using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain
{
    public class Document
    {
        /// <summary>
        ///  Первичный ключ для документа
        /// </summary>
        [Key]
        public string ID { get; set; }

        /// <summary>
        /// Статус документа, обязательно для заполнения
        /// </summary>
        [Required]
        public string Status { get; set; }

        /// <summary>
        ///  Идентификатор активной задачи, может быть null
        /// </summary>
        public string? ActiveTaskID { get; set; }

        /// <summary>
        /// Ссылка на активную задачу, может быть null
        /// </summary>
        public ProjectTask? ActiveTask { get; set; }

        /// <summary>
        /// Список задач, связанных с документом
        /// </summary>
        public List<ProjectTask> Tasks { get; set; }

        /// <summary>
        /// Конструктор инициализирует список задач
        /// </summary>
        public Document()
        {
            Tasks = new List<ProjectTask>();
        }
    }
}
