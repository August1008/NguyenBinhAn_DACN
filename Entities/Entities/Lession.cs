using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Lession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LessionOrder { get; set; }
        [ForeignKey("Course_Class")]
        public int CourseClassOder { get; set; }
        public Course_Class Course_Class { get; set; }
        public DateTime LessionDate { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
