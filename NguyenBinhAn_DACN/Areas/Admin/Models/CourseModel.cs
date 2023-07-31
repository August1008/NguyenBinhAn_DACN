using System;

namespace NguyenBinhAn_DACN.Areas.Admin.Models
{
    public class CourseModel
    {
        public int CourseOrder { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int TimeId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Shift { get; set; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
    }
}
