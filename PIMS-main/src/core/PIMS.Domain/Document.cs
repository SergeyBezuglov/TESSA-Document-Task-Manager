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
        [Key]
        public string ID { get; set; }

        [Required]
        public string Status { get; set; }

        public string? ActiveTaskID { get; set; }
        public ProjectTask? ActiveTask { get; set; }

        public List<ProjectTask> Tasks { get; set; }

        public Document()
        {
            Tasks = new List<ProjectTask>();
        }
    }
}
