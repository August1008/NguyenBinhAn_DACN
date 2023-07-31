using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Course_Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseClassOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string TeacherId     { get; set; }
        public Teacher  Teacher { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int ClassId  { get; set; }
        public Class Class { get; set; }
        public int TimeId   { get; set; }
        public Time Time { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Lession> Lessions { get; set; }
    }
}
