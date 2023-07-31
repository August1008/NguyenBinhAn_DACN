using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Student
    {
        [Key]
        [StringLength(10)]
        public string StudentId { get; set; }
        public string PersonId { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int InformationId { get; set; }
        public Information Information { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId    { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
