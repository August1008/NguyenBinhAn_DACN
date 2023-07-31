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

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Subjects
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Subjects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Admin/Subjects/Create
        public IActionResult Create()
        {
            ViewBag.Creditlist = new List<SelectListItem>
            {
                new SelectListItem{Text ="1" ,Value = "1"},
                new SelectListItem{Text ="2" ,Value = "2"},
                new SelectListItem{Text ="3" ,Value = "3"}
            };
            return View();
        }

        // POST: Admin/Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectModel subjectModel)
        {
            if (ModelState.IsValid)
            {
                var newSubject = new Subject
                {
                    SubjectId = subjectModel.SubjectId,
                    SubjectName = subjectModel.SubjectName,
                    NumberOfLesson = subjectModel.setLessonByCredit(),
                    Credit = subjectModel.Credit,
                };
                _context.Add(newSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Creditlist = new List<SelectListItem>
            {
                new SelectListItem{Text ="1" ,Value = "1"},
                new SelectListItem{Text ="2" ,Value = "2"},
                new SelectListItem{Text ="3" ,Value = "3"}
            };
            return View(subjectModel);
        }

        // GET: Admin/Subjects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Admin/Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Subject subject)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedSubject = await _context.Subjects.
                        FirstOrDefaultAsync(s => s.SubjectId == subject.SubjectId);
                    updatedSubject.SubjectName = subject.SubjectName;
                    updatedSubject.Credit = subject.Credit;
                    switch (updatedSubject.Credit)
                    {
                        case 1: updatedSubject.NumberOfLesson = 6; break;
                        case 2: updatedSubject.NumberOfLesson = 7; break;
                        case 3: updatedSubject.NumberOfLesson = 9; break;
                    }
                    _context.Subjects.Update(updatedSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            return View(subject);
        }

        // GET: Admin/Subjects/Delete/5
        
        // POST: Admin/Subjects/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok(new { message = "delete sucessfully!" });
        }

        private bool SubjectExists(string id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }

        #region API CALL
        [HttpGet]
        public async Task<IActionResult> SubjectList()
        {
            var list =await (from s in _context.Subjects
                        select new SubjectModel
                        {
                            SubjectId = s.SubjectId,
                            SubjectName = s.SubjectName,
                            NumberOfLesson = s.NumberOfLesson,
                            Credit = s.Credit,
                        }
                        ).ToListAsync();
            return Json(new { data = list });
        }
        #endregion
    }
}
