using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using PIMS.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PIMS.Infrastructure.Persistence.Interceptors;
using System.Data;
using PIMS.Infrastructure.Persistence.Repositories.Common;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using Serilog.Exceptions;
using System.Reflection;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using PIMS.Logger.Services;

namespace PIMS.Infrastructure
{
    /// <summary>
    /// Внедрение зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Имя таблицы журнала событий.
        /// </summary>
        private const string EventLogTableName = "EventLog";
        /// <summary>
        /// Добавили инфраструктуру.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="builder">Строитель.</param>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddPersistence(builder.Configuration, out string SelectedProvider,out string SelectedConnectionString);
            services.AddLogging(builder,SelectedProvider, SelectedConnectionString);

            return services;
        }
        /// <summary>
        /// Добавили упорства
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="SelectedProvider">Выбранный провайдер.</param>
        /// <param name="SelectedConnectionString">Выбранная строка подключения.</param>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        private static IServiceCollection AddPersistence(this IServiceCollection services,
            ConfigurationManager configuration,out string SelectedProvider,out string SelectedConnectionString)
        {  
            var dbProviderSettings = new DBProviderSettings();
            configuration.Bind(DBProviderSettings.SectionName, dbProviderSettings);
            configuration.GetSection(DBProviderSettings.SectionName);
            
            services.AddSingleton(Options.Create(dbProviderSettings));
            var InjectedCommandLineArgumentProviderName = configuration.GetValue(DBProviderSettings.ProviderNameArgsForCommandLine, "");
         
            var dbProvider =  dbProviderSettings.ProviderName;
            if (!string.IsNullOrEmpty(InjectedCommandLineArgumentProviderName))
            {
                dbProvider = InjectedCommandLineArgumentProviderName;
            }
            SelectedProvider = dbProvider;
            var connectionString = SelectedConnectionString = configuration.GetConnectionString(dbProvider)!;

            services.AddDbContext<PIMSDbContext>(options => {
                options.EnableDetailedErrors().EnableSensitiveDataLogging();
                options.DatabaseProviderConfiguration(dbProvider!,connectionString!);                
            });         


            services.AddScoped<PublishDomainEventsInterceptor>();
            #region Репозитории
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            #endregion Репозитории
            #region Кэш репозиториев
            //  services.Decorate<IUserRepository, CachedUserRepository>();
            #endregion Кэш репозиториев


            services.AddMemoryCache();
            return services;
        }
        /// <summary>
        /// Базы данных конфигурации провайдера.
        /// </summary>
        /// <param name="options">Варианты.</param>
        /// <param name="provider">Провайдер.</param>
        /// <param name="connectionString">Строка подключения.</param>
        private static void DatabaseProviderConfiguration(this DbContextOptionsBuilder options,string provider,string connectionString)
        {
            if (provider == DBProviderSettings.MSSQLServer)
            {
                options.UseSqlServer(connectionString!,
                           x => x.MigrationsAssembly("PIMS.Migrations.MSQL")
                       );
            }
            if (provider == DBProviderSettings.SQLite)
            {
                options.UseSqlServer(connectionString!,
                           x => x.MigrationsAssembly("PIMS.Migrations.SQLite")
                       );
            }
        }
        /// <summary>
        /// Добавили логирование.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="builder">Строитель.</param>
        /// <param name="dbProvider">Провайдер базы данных.</param>
        /// <param name="connectionString">Строка подключения.</param>
        private static void AddLogging(this IServiceCollection services, WebApplicationBuilder builder,string dbProvider,string connectionString)
        {
            
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var LoggerConfiguration= new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Assembly", $"{assembly.Version}");


            if (dbProvider == DBProviderSettings.MSSQLServer)
            {
                var ColumnOptions = new Serilog.Sinks.MSSqlServer.ColumnOptions
                {

                    AdditionalColumns = new Collection<SqlColumn>
                  {
                     new SqlColumn {ColumnName =CustomLoggerExtensions.UserInfoCustomColumnName, PropertyName =CustomLoggerExtensions.UserInfoCustomColumnName, DataType = SqlDbType.NVarChar, DataLength = 500}
                  }
                };


                LoggerConfiguration.WriteTo.MSSqlServer(
              connectionString: connectionString,
              sinkOptions: new MSSqlServerSinkOptions
              {

                  TableName = EventLogTableName,
              },

              columnOptions: ColumnOptions);
            }
            if (dbProvider == DBProviderSettings.SQLite)
            {
                LoggerConfiguration.WriteTo.SQLite(
                    connectionString,
                    tableName: "LogEvents",
                    storeTimestampInUtc: true,
                    restrictedToMinimumLevel: LogEventLevel.Information);
            }




            Log.Logger = LoggerConfiguration.CreateBootstrapLogger();
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
        }
        
        
      

    }
}