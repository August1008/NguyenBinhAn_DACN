using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NguyenBinhAn_DACN.Data;
using System.Threading.Tasks;
using System.Linq;
using NguyenBinhAn_DACN.API.Models;
using NguyenBinhAn_DACN.Utility;
using Microsoft.EntityFrameworkCore;

namespace NguyenBinhAn_DACN.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentAPIsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("get-class")]
        public async Task<ActionResult> GetStudentClasses(string studentId)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            var studentCourseList = await (from cl in _context.Classes
                                           where cl.ClassId == student.ClassId
                                           join c in _context.Course_Class on cl.ClassId equals c.ClassId
                                           join t in _context.Teachers on c.TeacherId equals t.TeacherId
                                           join i in _context.Informations on t.InformationId equals i.InformationId
                                           join s in _context.Subjects on c.SubjectId equals s.SubjectId
                                           join ti in _context.Times on c.TimeId equals ti.Id
                                           select new CourseModel
                                           {
                                               SubjectName = s.SubjectName,
                                               Shift = ti.Shift,
                                               StartDate = c.StartDate.ToString("dd/MM/yyyy"),
                                               EndDate = c.EndDate.ToString("dd/MM/yyyy"),
                                               ClassName = cl.Name,
                                               CourseOrder = c.CourseClassOrder,
                                               Hour = ti.Shift.Equals(SD.Time_Morning) ? 7 : 12,
                                               TeacherName = i.Name
                                           }).ToListAsync();
            return Ok(new { status = "true", data = studentCourseList });
        }
    }
}
