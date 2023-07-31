using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using NguyenBinhAn_DACN.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System;
using NguyenBinhAn_DACN.API.Models;
using Entities.Entities;
using System.Globalization;

namespace NguyenBinhAn_DACN.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TakeAttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private FaceClient client { get; set; }
        public TakeAttendanceController(ApplicationDbContext context)
        {
            _context = context;
            client = new FaceClient(new ApiKeyServiceClientCredentials("3ebbc489fe194c8a9a263d24acefd334"))
            {
                Endpoint = "https://nguyenbinhan.cognitiveservices.azure.com/"
            };
        }
        [HttpPost("take-attendance")]
        public async Task<IActionResult> TakeAttendance(IFormCollection dataForm)
        {
            var images = HttpContext.Request.Form.Files;
            var courseOrder = int.Parse(HttpContext.Request.Form["courseOrder"]);
            var lessonDate = HttpContext.Request.Form["date"];
            // lay ra khoa hoc
            var course = _context.Course_Class.FirstOrDefault(c => c.CourseClassOrder == courseOrder);
            if (course == null)
            {
                return Ok(new { status = false, message = "course not found" });
            }
            // nhan dien khuon mat co trong hinh lop
            Stream classImage = images[0].OpenReadStream();
            IList<DetectedFace> detectedFaces = await client.Face.DetectWithStreamAsync(classImage
                ,recognitionModel:RecognitionModel.Recognition04
                ,detectionModel:DetectionModel.Detection03);
            //lay faceid cua cac khuon mat co trong hinh lop
            IList<Guid> sourseFaceIds = new List<Guid>();
            foreach (DetectedFace face in detectedFaces)
            {
                sourseFaceIds.Add(face.FaceId.Value);
            }

            IList<IdentifyResult> identifyResults = null;
            identifyResults = await client.Face.IdentifyAsync(sourseFaceIds, "binhan");

            List<AttendStudentModel> studentList = new List<AttendStudentModel>();
            Person person = null;
            foreach (var result in identifyResults)
            {
                if (result.Candidates.Count != 0)
                {
                    person = client.PersonGroupPerson.GetAsync("binhan", result.Candidates[0].PersonId).Result;
                    var student = _context.Students.FirstOrDefault(c => c.StudentId == person.UserData);

                    if (student != null)
                    {
                        var information = _context.Informations.FirstOrDefault(i => i.InformationId == student.InformationId);
                        studentList.Add(new AttendStudentModel
                        {
                            StudentId = student.StudentId,
                            StudentName = information.Name
                        });
                    }
                }
            }

            // tao lesson moi de luu thong tin diem danh
            Lession newLesson = new Lession();
            newLesson.CourseClassOder = courseOrder;
            newLesson.LessionDate = DateTime.ParseExact(lessonDate,"dd/MM/yyyy",CultureInfo.InvariantCulture);
            _context.Lessions.Add(newLesson);
            _context.SaveChanges();
            //lay ra danh sach toan bo sinh vien trong lop
            var allstudentList = (from s in _context.Students
                                  where s.ClassId == course.ClassId select s).ToList();
            foreach(var student in allstudentList)
            {
                Attendance attendance = new Attendance();
                int flag = 0;
                foreach(var attendstudent in studentList)
                {
                    if(student.StudentId == attendstudent.StudentId)
                    {
                        attendance.Status = true;
                        attendance.CourseOrder = courseOrder;
                        attendance.StudentId = student.StudentId;
                        attendance.LessionOder = newLesson.LessionOrder;
                        attendance.RollCallDate = DateTime.ParseExact(lessonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        _context.Add(attendance);
                        flag = 1;
                        break;
                    }
                }
                if(flag == 0)
                {
                    attendance.Status = false;
                    attendance.CourseOrder = courseOrder;
                    attendance.StudentId = student.StudentId;
                    attendance.LessionOder = newLesson.LessionOrder;
                    attendance.RollCallDate = DateTime.ParseExact(lessonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _context.Add(attendance);
                }

            }
            _context.SaveChanges();
            return Ok(new { status = true, message = "successfully" });
        }
    }
}
