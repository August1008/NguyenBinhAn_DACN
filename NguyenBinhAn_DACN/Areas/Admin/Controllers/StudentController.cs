using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenBinhAn_DACN.Areas.Admin.Models;
using NguyenBinhAn_DACN.Data;
using System.Collections.Generic;
using Entities.Entities;
using NguyenBinhAn_DACN.Utility;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public StudentController(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentImage(string studentId)
        {
            ViewBag.ImageList = _context.StudentImages.Where(i => i.StudentId == studentId).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(string filepath)
        {
            string deletedImage = Path.Combine(_hostingEnvironment.WebRootPath, "images\\StudentImages\\", filepath);
            if (System.IO.File.Exists(deletedImage))
            {
                System.IO.File.Delete(deletedImage);
                StudentImage image = _context.StudentImages.FirstOrDefault(i => i.Path == filepath);
                _context.StudentImages.Remove(image);
                _context.SaveChanges();
            }
            else
            {

            }
            return RedirectToAction("StudentImage");
        }




        // GET: Admin/Teachers/Create
        public IActionResult Create()
        {
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
            ViewBag.ClassList = new SelectList(_context.Classes, "ClassId", "Name");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem{Text="Male",Value="Male" },
                new SelectListItem{Text="Female",Value="Female" },
                new SelectListItem{Text="Orther",Value="Orther" }
            };

            return View();
        }

        

        // POST: Admin/Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel student)
        {
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
            ViewBag.ClassList = new SelectList(_context.Classes, "ClassId", "Name");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem{Text="Male",Value="Male" },
                new SelectListItem{Text="Female",Value="Female" },
                new SelectListItem{Text="Orther",Value="Orther" }
            };
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            // create new address for student
            var Address = new Address
            {
                WardId = student.WardId,
                StreetAddress = student.Address,
            };
            _context.Address.Add(Address);
            await _context.SaveChangesAsync();

            // create new information for student
            var Information = new Information
            {
                AddressId = Address.AddressId,
                BirthDay = student.BirthDay,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IdentityCardNumber = student.IdentityCardNumber,
                Name = student.Name,
                Gender = student.Gender
            };
            _context.Informations.Add(Information);
            //create new user for student
            ApplicationUser newUser = new ApplicationUser
            {
                UserName = student.StudentId,
            };
            var password = student.getPasswordFromBirthDay();
            await _userManager.CreateAsync(newUser, password);
            await _context.SaveChangesAsync();

            if (!await _roleManager.RoleExistsAsync(SD.Role_Student))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Student));
            }

            await _userManager.AddToRoleAsync(newUser, SD.Role_Student);
            // create new teacher
            var newStudent = new Student
            {
                StudentId = student.StudentId,
                UserId = newUser.Id,
                InformationId = Information.InformationId,
                DepartmentId = student.DepartmentId,
                ClassId = student.ClassId,   
            };
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(t => t.StudentId == id);
            IdentityUser user = await _userManager.FindByNameAsync(id);

            if(user != null)
                await _userManager.DeleteAsync(user);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Delete Successfully!" });
        }

        // GET: Admin/Teachers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await (from t in _context.Students
                                 join i in _context.Informations on t.InformationId equals i.InformationId
                                 join a in _context.Address on i.AddressId equals a.AddressId
                                 join w in _context.Wards on a.WardId equals w.WardId
                                 join d in _context.Districts on w.DistrictId equals d.DistrictId
                                 where t.StudentId == id
                                 select new StudentModel
                                 {
                                     StudentId = t.StudentId,
                                     BirthDay = i.BirthDay,
                                     Name = i.Name,
                                     Gender = i.Gender,
                                     DepartmentId = t.DepartmentId,
                                     ClassId = t.ClassId,
                                     PhoneNumber = i.PhoneNumber,
                                     IdentityCardNumber = i.IdentityCardNumber,
                                     Email = i.Email,
                                     Address = a.StreetAddress,
                                     WardId = a.WardId,
                                     DistrictId = w.DistrictId,
                                     ProvinceId = d.ProvinceId,
                                     BirthDayString = i.BirthDay.ToString("yyyy-MM-dd")
                                 }).FirstOrDefaultAsync(x => x.StudentId == id);
            ;
            if (student == null)
            {
                return NotFound();
            }
            // change to class and department
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
            ViewBag.ClassList = new SelectList(_context.Classes, "ClassId", "Name");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
            ViewBag.DistrictList = new SelectList(_context.Districts, "DistrictId", "Name");
            ViewBag.WardList = new SelectList(_context.Wards, "WardId", "Name");
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem{Text="Male",Value="Male" },
                new SelectListItem{Text="Female",Value="Female" },
                new SelectListItem{Text="Orther",Value="Orther" }
            };
            return View(student);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentModel student)
        {
            try
            {
                var updateStudent = await _context.Students.FirstOrDefaultAsync(t => t.StudentId == student.StudentId);
                if (updateStudent == null)
                {
                    return NotFound();
                }
                //update lại thông tin
                var upInformation = await _context.Informations
                    .FirstOrDefaultAsync(i => i.InformationId == updateStudent.InformationId);
                upInformation.BirthDay = student.BirthDay;
                upInformation.Gender = student.Gender;
                upInformation.Name = student.Name;
                upInformation.PhoneNumber = student.PhoneNumber;
                upInformation.Email = student.Email;
                upInformation.IdentityCardNumber = student.IdentityCardNumber;
                _context.Informations.Update(upInformation);


                // update lại địa chỉ
                var updateAddress = await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == upInformation.AddressId);
                updateAddress.StreetAddress = student.Address;
                updateAddress.WardId = student.WardId;
                _context.Address.Update(updateAddress);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
        #region API
        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var list = await (from s in _context.Students
                        join i in _context.Informations on s.InformationId equals i.InformationId
                        join c in _context.Classes on s.ClassId equals c.ClassId
                        join d in _context.Departments on s.DepartmentId equals d.DepartmentId
                        select new StudentModel
                        {
                            StudentId = s.StudentId,
                            Name = i.Name,
                            Department = d.Name,
                            Class = c.Name,
                            Email = i.Email
                        }).ToListAsync();
            foreach(var student in list)
            {
                student.ImagesCount = _context.StudentImages.Where(i => i.StudentId == student.StudentId).Count();
            }
            return Ok(new {data=list});
        }
        #endregion
    }
}
