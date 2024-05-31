using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain
{
    public class ProjectTask
    {
        /// <summary>
        /// Первичный ключ для задачи
        /// </summary>
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// Имя задачи, обязательно для заполнения
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор предыдущей задачи, может быть null
        /// </summary>
        public string? PreviousTaskID { get; set; }
        /// <summary>
        /// Ссылка на предыдущую задачу, может быть null
        /// </summary>
        public ProjectTask? PreviousTask { get; set; }
        /// <summary>
        /// Идентификатор документа, к которому относится задача
        /// </summary>
        public string DocumentID { get; set; }
        /// <summary>
        /// Ссылка на документ, к которому относится задача
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ProjectTask() { }
    }
}
