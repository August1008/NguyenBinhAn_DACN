using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Subject
    {
        [Key]
        [StringLength(7)]
        public string SubjectId { get; set; }
        [StringLength(128)]
        public string SubjectName { get; set; }

        public int NumberOfLesson { get; set; }
        public int Credit { get; set; }
    }
}
