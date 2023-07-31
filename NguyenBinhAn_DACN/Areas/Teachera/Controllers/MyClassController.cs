using Microsoft.AspNetCore.Mvc;
using NguyenBinhAn_DACN.Areas.Teachera.Models;
using NguyenBinhAn_DACN.Data;
using NguyenBinhAn_DACN.Utility;
using System.Linq;

namespace NguyenBinhAn_DACN.Areas.Teachera.Controllers
{
    [Area("Teachera")]
    public class MyClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index(string id,int pg=1)
        {
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int categoryCount = _context.Classes.Count();

            var pager = new Pager(categoryCount, pg, pageSize);
            int recskip = (pg - 1) * pageSize;

            var data = (from cl in _context.Classes
                        join c in _context.Course_Class on cl.ClassId equals c.ClassId
                        where c.TeacherId == id
                        join s in _context.Subjects on c.SubjectId equals s.SubjectId
                        join t in _context.Times on c.TimeId equals t.Id
                        select new MyClassModel
                        {
                            CourseOrder = c.CourseClassOrder,
                            Shift = t.Shift,
                            StartDate = c.StartDate.ToString("dd/MM/yyyy"),
                            EndDate = c.EndDate.ToString("dd/MM/yyyy"),
                            SubjectId = c.SubjectId,
                            ClassId = c.ClassId,
                            ClassName = cl.Name,
                            Hour = t.Shift.Equals(SD.Time_Morning) ? 7 : 12
                        }) 
                .Skip(recskip).Take(pager.PageSize)
                .ToList();

            this.ViewBag.Pager = pager;
            return View(data);

        }

        public IActionResult Detail(int id)
        {
            var course = _context.Course_Class.FirstOrDefault(c => c.CourseClassOrder == id);
            if (course == null) return NotFound();
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == course.TeacherId);
            var level = _context.EducationLevels.FirstOrDefault(e => e.LevelId == teacher.LevelId);
            var currentClass = _context.Classes.FirstOrDefault(c => c.ClassId == course.ClassId);
            var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == course.SubjectId);
            var time = _context.Times.FirstOrDefault(t => t.Id == course.TimeId);

            ViewBag.Course = course;
            ViewBag.StartDate = course.StartDate.ToShortDateString();
            ViewBag.EndDate = course.EndDate.ToShortDateString();

            ViewBag.Teacher = _context.Informations.FirstOrDefault(t => t.InformationId == teacher.InformationId).Name;
            ViewBag.Level = level.Name;
            ViewBag.Class = currentClass.Name;
            ViewBag.Subject = subject.SubjectName;
            ViewBag.Time = time.Shift;

            var studentList = (from s in _context.Students
                               where s.ClassId == course.ClassId
                               join i in _context.Informations on s.InformationId equals i.InformationId
                               select new StudentAttendanceModel
                               {
                                   StudentId = s.StudentId,
                                   StudentName = i.Name,
                               }).ToList();
            //calculate absent rate
            //count lession in all course
            int lessonCount = _context.Lessions.Where(l => l.CourseClassOder == course.CourseClassOrder).Count();
            // 
            int attendanceNum = 0;
            foreach (var student in studentList)
            {
                //count attendance(status = true)
                attendanceNum = _context.Attendances.
                    Where(a => a.StudentId == student.StudentId)
                    .Where(a => a.CourseOrder == course.CourseClassOrder)
                    .Where(a=> a.Status == true)
                    .Count();
                student.AttendanceRate = student.calAttendanceRate(attendanceNum, lessonCount) * 100;
            }

            // get all lesson of course
            var lessonList = (from l in _context.Lessions
                              where l.CourseClassOder == course.CourseClassOrder
                              select new LessonModel
                              {
                                  LessonOrder = l.LessionOrder,
                                  LessonDate = l.LessionDate.ToString("dd/MM/yyyy")
                              }).ToList();
            // count attendance in class at lesson
            foreach(var lesson in lessonList)
            {
                lesson.AttendanceStudent = _context.Attendances.Where(a => a.LessionOder == lesson.LessonOrder)
                    .Where(a=> a.Status == true).Count();
            }
            ViewBag.LessonList = lessonList;
            ViewBag.StudentCount = studentList.Count();
            ViewBag.CourseOrder = course.CourseClassOrder;
            return View(studentList);
        }
    }
}
