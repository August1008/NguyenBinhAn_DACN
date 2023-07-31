namespace NguyenBinhAn_DACN.Areas.Teachera.Models
{
    public class LessonModel
    {
        public int LessonOrder { get; set; }
        public string LessonDate { get; set; }
        public int AttendanceStudent { set; get; }
        public int AbsentStudent { set; get; }
    }
}
