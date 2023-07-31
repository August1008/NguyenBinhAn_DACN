using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Teacher
    {
        [Key]
        [StringLength(8)]
        public string TeacherId { get; set; }
        [ForeignKey("EducationLevel")]
        public int LevelId { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public int InformationId { get; set; }
        public Information Information { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
