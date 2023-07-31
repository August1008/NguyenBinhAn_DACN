using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using NguyenBinhAn_DACN.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Entities.Entities;

namespace NguyenBinhAn_DACN.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        private FaceClient client { set; get; }
        public StudentImageController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
            _context = context;
            client = new FaceClient(new ApiKeyServiceClientCredentials("3ebbc489fe194c8a9a263d24acefd334"))
            {
                Endpoint = "https://nguyenbinhan.cognitiveservices.azure.com/"
            };
        }
        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadStudentImages(IFormCollection dataForm)
        {
            var studentId = HttpContext.Request.Form["studentId"];
            var images = HttpContext.Request.Form.Files;

            //luu hinh
            foreach (var image in images)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\StudentImages\\", image.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    image.CopyTo(stream);
                }
                StudentImage newImage = new StudentImage();
                newImage.Path = image.FileName;
                newImage.StudentId = studentId;
                _context.StudentImages.Add(newImage);
            }
            _context.SaveChanges();

            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
            {
                // sinh vien chua ton tai
                return Ok(new { status = false, message = "Student does not exist" });
            }
            // kiem tra sinh vien da co personId chua
            string studentName = _context.Informations.
                FirstOrDefault(i => i.InformationId == student.InformationId).Name;
            if(student.PersonId == null)
            {
                await Task.Delay(250);
                Person person = await client.PersonGroupPerson.CreateAsync("binhan", studentName, student.StudentId);
                student.PersonId = person.PersonId.ToString();
                _context.SaveChanges();
            }

            Stream tempStream = null;
            // nhan dang face trong images
            foreach (var image in images)
            {
                tempStream = image.OpenReadStream();
                await client.PersonGroupPerson.AddFaceFromStreamAsync("binhan", Guid.Parse(student.PersonId), tempStream);
            }
            // training du lieu
            await client.PersonGroup.TrainAsync("binhan");
            while(true)
            {
                await Task.Delay(1000);
                var trainingStatus = await client.PersonGroup.GetTrainingStatusAsync("binhan");
                if(trainingStatus.Status == TrainingStatusType.Succeeded)
                {
                    return Ok(new { status = true, message = "successfully" });
                }
            }
        }
    }
}
