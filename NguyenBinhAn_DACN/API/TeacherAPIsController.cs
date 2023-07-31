using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NguyenBinhAn_DACN.Data;
using System.Threading.Tasks;
using System.Linq;
using NguyenBinhAn_DACN.API.Models;
using NguyenBinhAn_DACN.Utility;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace NguyenBinhAn_DACN.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherAPIsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-class")]
        public async Task<ActionResult> GetMyClasses(string teacherId)
        {
            var courseList = await (from c in _context.Course_Class
                                    where c.TeacherId == teacherId
                                    join cl in _context.Classes on c.ClassId equals cl.ClassId
                                    join s in _context.Subjects on c.SubjectId equals s.SubjectId
                                    join t in _context.Times on c.TimeId equals t.Id
                                    select new CourseModel
                                    {
                                        SubjectName = s.SubjectName,
                                        Shift = t.Shift,
                                        StartDate = c.StartDate.ToString("dd/MM/yyyy"),
                                        EndDate = c.EndDate.ToString("dd/MM/yyyy"),
                                        ClassName = cl.Name,
                                        CourseOrder = c.CourseClassOrder,
                                        Hour = t.Shift.Equals(SD.Time_Morning) ? 7 : 12
                                    }).ToListAsync();
            return Ok(new { status = true, data = courseList });
        }

        [HttpGet("export-excel-file")]
        public async Task<ActionResult> ExportExcel(int id)
        {
            //int courseOrder = int.Parse(getcourseOrder);
            var course = await _context.Course_Class.FirstOrDefaultAsync(c => c.CourseClassOrder == id);
            var curClass = await _context.Classes.FirstOrDefaultAsync(cl => cl.ClassId == course.ClassId);
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == course.SubjectId);
            // danh sach tiet hoc cua khoa
            var lessonList = _context.Lessions.Where(l => l.CourseClassOder == id).ToList();
            //danh sach sinh vien 
            var studentList = (from s in _context.Students
                               where s.ClassId == course.ClassId
                               select s).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //tao excel va worksheet
            var package = new ExcelPackage();
            
            var workSheet = package.Workbook.Worksheets.Add(curClass.Name);

            // o dau tien
            workSheet.Cells["A1:K1"].Merge = true;
            workSheet.Cells["A1"].Value = curClass.Name + " - " + subject.SubjectName;
            workSheet.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1"].Style.Font.Bold = true;

            for(int j = 1; j <= 11; j++)
            {
                workSheet.Column(j).Width = 20;
            }
            //header
            List<string> lis = new List<string>()
            {
                "A2","B2","C2","D2","E2","F2","G2","H2","I2","J2","K2"
            };
            lis.ForEach(x =>
            {
                workSheet.Cells[x].Style.Font.Bold = true;
                workSheet.Cells[x].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[x].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[x].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[x].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[x].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            });
            workSheet.Cells[lis[0]].Value = "Student Code";
            workSheet.Cells[lis[1]].Value = "Full Name";
            int lessonIndex = 2;
            foreach(var lesson in lessonList)
            {
                workSheet.Cells[lis[lessonIndex]].Value = lesson.LessionDate.ToString("dd/MM/yyyy");
                lessonIndex++;
            }

            // noi dung
            int studentIndex = 3;
            foreach(var student in studentList)
            {
                workSheet.Cells["A" + studentIndex].Value = student.StudentId;
                workSheet.Cells["B" + studentIndex].Value = _context.Informations.FirstOrDefault(i => i.InformationId == student.InformationId).Name;
                int colunmIndex = 67;
                string celli = ((char)colunmIndex).ToString() + studentIndex;
                foreach (var lesson in lessonList)
                {
                    var attendance = _context.Attendances.Where(a => a.StudentId == student.StudentId && a.LessionOder == lesson.LessionOrder).FirstOrDefault();
                    if(attendance.Status)
                    {
                        workSheet.Cells[((char)colunmIndex).ToString() + studentIndex].Value = "X";
                    }
                    colunmIndex++;
                }
                studentIndex++;
            }

            byte[] data = await package.GetAsByteArrayAsync();
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "Exports", "export.xlsx");
            if (!System.IO.File.Exists(filepath))
            {
                System.IO.File.WriteAllBytes(filepath, data);
            }
            else
            {
                filepath = Path.Combine(Directory.GetCurrentDirectory(), "Exports", "export");
                filepath += "_" + DateTime.Now.ToString("yyyy-MM-dd-H-m") + ".xlsx";
                System.IO.File.WriteAllBytes(filepath, data);

            }
            return Ok(filepath);
        }
    }
}
