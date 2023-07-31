using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenBinhAn_DACN.Areas.Admin.Models;
using NguyenBinhAn_DACN.Data;
using NguyenBinhAn_DACN.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            ViewBag.SubjectList = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewBag.TimeList = new SelectList(_context.Times, "Id", "Shift");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.TeacherList = new SelectList(_context.Teachers.Include(t => t.Information), "TeacherId", "Information.Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseModel course)
        {
            Course_Class newCourse = new Course_Class
            {
                ClassId = course.ClassId,
                TeacherId = course.TeacherId,
                SubjectId = course.SubjectId,
                StartDate = DateTime.Parse(course.StartDate),
                EndDate = DateTime.Parse(course.EndDate),
                TimeId = course.TimeId
            };
            _context.Add(newCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        #region API CALL
        [HttpGet]
        public async Task<IActionResult> CourseList()
        {
            var list = await (from c in _context.Course_Class
                              join ti in _context.Times on c.TimeId equals ti.Id
                              join cl in _context.Classes on c.ClassId equals cl.ClassId
                              join s in _context.Subjects on c.SubjectId equals s.SubjectId
                              join t in _context.Teachers on c.TeacherId equals t.TeacherId
                              
                              select new CourseModel
                              {
                                  CourseOrder = c.CourseClassOrder,
                                  ClassName = cl.Name,
                                  SubjectName = s.SubjectName,
                                  StartDate = c.StartDate.ToString("dd/MM/yyyy"),
                                  EndDate = c.EndDate.ToString("dd/MM/yyyy"),
                                  TeacherId = c.TeacherId,
                                  StartTime = ti.Shift.Equals(SD.Time_Morning) ? "7:30" : "12:30",
                                  EndTime = ti.Shift.Equals(SD.Time_Morning) ? "11:30" : "16:30"
                              }).ToListAsync();
            return Ok(new {data = list});
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           var course = await _context.Course_Class.FindAsync(id);
            if (course == null)
            {
                return BadRequest(new { message = "Delete failed!" });
            }
            _context.Course_Class.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Delete sucessfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetClassList(int DepartmentId)
        {
            var list = await _context.Classes.Where(c => c.DepartmentId == DepartmentId).ToListAsync();
            var rList = new SelectList(list, "ClassId", "Name");
            return Ok( rList );
        }
        #endregion
    }
}
