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
        [Key]
        public string ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string? PreviousTaskID { get; set; }
        public ProjectTask? PreviousTask { get; set; }

        public string DocumentID { get; set; }
        public Document Document { get; set; }

        public ProjectTask() { }
    }
}
