using System;

namespace NguyenBinhAn_DACN.Areas.Teachera.Models
{
    public class StudentAttendanceModel
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public double AttendanceRate { get; set; }

        public double calAttendanceRate(int x,int y)
        {
            return Math.Round((double)x / y,4);
        }
    }
}
