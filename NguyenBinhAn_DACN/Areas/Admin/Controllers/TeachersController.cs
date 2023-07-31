using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Entities;
using NguyenBinhAn_DACN.Data;
using NguyenBinhAn_DACN.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using NguyenBinhAn_DACN.Utility;

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeachersController(ApplicationDbContext context,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Admin/Teachers
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Teachers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.EducationLevel)
                .Include(t => t.Information)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Admin/Teachers/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.EducationLevels, "LevelId", "Name");
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
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
        public async Task<IActionResult> Create( TeacherModel teacher)
        {
            
            if (!ModelState.IsValid)
            {
                ViewData["LevelId"] = new SelectList(_context.EducationLevels, "LevelId", "Name");
                ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
                ViewBag.GenderList = new List<SelectListItem>
                {
                    new SelectListItem{Text="Male",Value="Male" },
                    new SelectListItem{Text="Female",Value="Female" },
                    new SelectListItem{Text="Orther",Value="Orther" }
                };
                return View(teacher);
            }
            // create new address for teacher
            var Address = new Address
            {
                WardId = teacher.WardId,
                StreetAddress = teacher.Address,
            };
            _context.Address.Add(Address);
            await _context.SaveChangesAsync();

            // create new information for teacher
            var Information = new Information
            {
                AddressId = Address.AddressId,
                BirthDay = teacher.BirthDay,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                IdentityCardNumber = teacher.IdentityCardNumber,
                Name = teacher.Name,
                Gender = teacher.Gender
            };
            _context.Informations.Add(Information);
            await _context.SaveChangesAsync();
            //create new user for teacher
            ApplicationUser newUser = new ApplicationUser
            {
                UserName = teacher.TeacherId,
            };
            var password = teacher.getPasswordFromBirthDay();
            await _userManager.CreateAsync(newUser,password);
            await _userManager.AddToRoleAsync(newUser, SD.Role_Teacher);
            // create new teacher
            var newTeacher = new Teacher
            {
                TeacherId = teacher.TeacherId,
                ApplicationUserId = newUser.Id,
                InformationId = Information.InformationId,
                LevelId = teacher.LevelId
            };
            _context.Teachers.Add(newTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));

        }

        // GET: Admin/Teachers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await (from t in _context.Teachers
                                 join i in _context.Informations on t.InformationId equals i.InformationId
                                 join a in _context.Address on i.AddressId equals a.AddressId
                                 join w in _context.Wards on a.WardId equals w.WardId
                                 join d in _context.Districts on w.DistrictId equals d.DistrictId
                                 where t.TeacherId == id
                                 select new TeacherModel
                                 {
                                     TeacherId=t.TeacherId,
                                     BirthDay = i.BirthDay,
                                     BirthDayString = i.BirthDay.ToString("yyyy-MM-dd"),
                                     Name = i.Name,
                                     Gender = i.Gender,
                                     LevelId = t.LevelId,
                                     PhoneNumber = i.PhoneNumber,
                                     IdentityCardNumber = i.IdentityCardNumber,
                                     Email = i.Email,
                                     Address = a.StreetAddress,
                                     WardId = a.WardId,
                                     DistrictId = w.DistrictId,
                                     ProvinceId = d.ProvinceId

                                 }).FirstOrDefaultAsync(x => x.TeacherId == id);
                ;
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["LevelId"] = new SelectList(_context.EducationLevels, "LevelId", "Name", teacher.LevelId);
            ViewBag.ProvinceList = new SelectList(_context.Provinces, "ProvinceId", "Name");
            ViewBag.DistrictList = new SelectList(_context.Districts, "DistrictId", "Name");
            ViewBag.WardList = new SelectList(_context.Wards, "WardId", "Name");
            ViewBag.GenderList = new List<SelectListItem>
            {
                new SelectListItem{Text="Male",Value="Male" },
                new SelectListItem{Text="Female",Value="Female" },
                new SelectListItem{Text="Orther",Value="Orther" }
            };
            return View(teacher);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( TeacherModel teacher)
        {
            
            try
            {
                var updateTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacher.TeacherId);
                if (updateTeacher == null)
                {
                    return NotFound();
                }
                //update lại thông tin
                var upInformation = await _context.Informations
                    .FirstOrDefaultAsync(i => i.InformationId == updateTeacher.InformationId);
                upInformation.BirthDay = teacher.BirthDay;
                upInformation.Gender = teacher.Gender;
                upInformation.Name = teacher.Name;
                upInformation.PhoneNumber = teacher.PhoneNumber;
                upInformation.Email = teacher.Email;
                upInformation.IdentityCardNumber = teacher.IdentityCardNumber;
                _context.Informations.Update(upInformation);


                // update lại địa chỉ
                var updateAddress = await _context.Address
                    .FirstOrDefaultAsync(a => a.AddressId == upInformation.AddressId);
                updateAddress.StreetAddress = teacher.Address;
                updateAddress.WardId = teacher.WardId;
                _context.Address.Update(updateAddress);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(teacher.TeacherId))
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

        

        // POST: Admin/Teachers/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);

            IdentityUser user = await _userManager.FindByNameAsync(id);

            var courseList = _context.Course_Class.Where(c => c.TeacherId == teacher.TeacherId).ToList();
            foreach (var course in courseList)
            {
                _context.Course_Class.Remove(course);
            }
            await _userManager.DeleteAsync(user);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Delete Successfully!" });
        }

        private bool TeacherExists(string id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }

        #region API CALL
        public async Task<IActionResult> TeacherList()
        {
            var list = await (from t in _context.Teachers
                              join i in _context.Informations on t.InformationId equals i.InformationId
                              join e in _context.EducationLevels on t.LevelId equals e.LevelId
                              select new TeacherModel
                              {
                                  TeacherId = t.TeacherId,
                                  Name = i.Name,
                                  BirthDay = i.BirthDay,
                                  EducationLevel = e.Name,
                                  Email = i.Email,
                                  PhoneNumber = i.PhoneNumber,
                              }
                              ).ToListAsync();
            return Json(new { data = list });
        }
        #endregion
    }
}
