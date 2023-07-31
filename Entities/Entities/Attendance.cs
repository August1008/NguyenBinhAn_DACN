using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceOrder { get; set; }
        public bool Status { get; set; }
        public DateTime RollCallDate { get; set; }
        [ForeignKey("Lession")]
        public int? LessionOder { get; set; }
        public Lession Lession { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Course_Class")]
        public int CourseOrder { get; set; }
        public Course_Class Course { get; set; }
        
    }
}
