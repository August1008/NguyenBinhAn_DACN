namespace NguyenBinhAn_DACN.API.Models
{
    public class CourseModel
    {
        public int CourseOrder { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Shift { get; set; }
        public int Hour { get; set; }
    }
}
