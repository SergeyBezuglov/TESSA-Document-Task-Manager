using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.DbContexts
{
    /// <summary>
    /// Настройки провайдера БД.
    /// </summary>
    public class DBProviderSettings
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public const string SectionName = "DBProviderSettings";
        /// <summary>
        /// Имя поставщика является аргументом командной строки.
        /// </summary>
        public const string ProviderNameArgsForCommandLine = "ProviderName";
       
        /// <summary>
        /// Сервер SQLLite
        /// </summary>
        public const string SQLite= nameof(SQLite);
        /// <summary>
        /// Имя поставщика.
        /// </summary>
        /// <value>Строка.</value>
        public string ProviderName { get; set; } = string.Empty;
    }
}
