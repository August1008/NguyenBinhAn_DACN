using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentOrder { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Course_Class")]
        public int CourseClassOrder { get; set; }
        public Course_Class Course_Class { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}
