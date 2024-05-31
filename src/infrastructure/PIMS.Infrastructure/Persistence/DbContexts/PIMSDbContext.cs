using Microsoft.EntityFrameworkCore;
using PIMS.Domain;
using PIMS.Infrastructure.Persistence.Interceptors;

namespace PIMS.Infrastructure.Persistence.DbContexts
{
    /// <summary>
    /// Контекст базы данных PIMS.
    /// </summary>
    public class PIMSDbContext:DbContext
    {
        /// <summary>
        /// Перехватчик событий домена публикации.
        /// </summary>
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="PIMSDbContext"/> .
        /// </summary>
        /// <param name="options">Варианты.</param>
        /// <param name="publishDomainEventsInterceptor">Перехватчик событий домена публикации.</param>
        public PIMSDbContext(DbContextOptions<PIMSDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)

         : base(options)

        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }
        /// <summary>
        /// Таблица документов.
        /// </summary>
        public DbSet<Document> Documents { get; set; } = null!;
        /// <summary>
        /// Таблица задач проектов.
        /// </summary>
        public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
        /// <summary>
        /// Создание модели.
        /// </summary>
        /// <param name="modelBuilder">Создатель моделей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                
                ApplyConfigurationsFromAssembly(typeof(PIMSDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
        /// <summary>
        /// По настройке.
        /// </summary>
        /// <param name="optionsBuilder">Конструктор опции.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=./PIMS.db");
            }

            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
