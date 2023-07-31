using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Entities;
using NguyenBinhAn_DACN.Data;

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Classes
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deletedClass = await _context.Classes
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (deletedClass == null)
            {
                return NotFound();
            }

            return View(deletedClass);
        }

        // GET: Admin/Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,Name")] Class deletedClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deletedClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deletedClass);
        }

        // GET: Admin/Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deletedClass = await _context.Classes.FindAsync(id);
            if (deletedClass == null)
            {
                return NotFound();
            }
            return View(deletedClass);
        }

        // POST: Admin/Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,Name")] Class deletedClass)
        {
            if (id != deletedClass.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deletedClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(deletedClass.ClassId))
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
            return View(deletedClass);
        }

        // GET: Admin/Classes/Delete/5
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedClass = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(deletedClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }

        #region API CALL
        [HttpGet]
        public IActionResult ClassList()
        {
            var list = _context.Classes.ToList();
            return Json(new { data = list });
        }
        #endregion
    }
}
