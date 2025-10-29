using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myblogs.Data;
using myblogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myblogs.Controllers
{
    public class BlogTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogsTypes.ToListAsync());
        }

        // GET: BlogTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTypesModel = await _context.BlogsTypes
                .FirstOrDefaultAsync(m => m.BlogTypeId == id);
            if (blogTypesModel == null)
            {
                return NotFound();
            }

            return View(blogTypesModel);
        }

        // GET: BlogTypes/Create
        public IActionResult Create()
        {

            var _UserId = User.Identity.GetUserId();
            ViewBag.UserId = _UserId;
            ViewBag.datecreated = DateTime.Now;
            return View();
        }

        // POST: BlogTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogTypeId,UserId,BlogTypeName,BlogTypeDescription,DateCreated")] BlogTypesModel blogTypesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTypesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogTypesModel);
        }

        // GET: BlogTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTypesModel = await _context.BlogsTypes.FindAsync(id);
            if (blogTypesModel == null)
            {
                return NotFound();
            }
            return View(blogTypesModel);
        }

        // POST: BlogTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogTypeId,UserId,BlogTypeName,BlogTypeDescription,DateCreated")] BlogTypesModel blogTypesModel)
        {
            if (id != blogTypesModel.BlogTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTypesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTypesModelExists(blogTypesModel.BlogTypeId))
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
            return View(blogTypesModel);
        }

        // GET: BlogTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTypesModel = await _context.BlogsTypes
                .FirstOrDefaultAsync(m => m.BlogTypeId == id);
            if (blogTypesModel == null)
            {
                return NotFound();
            }

            return View(blogTypesModel);
        }

        // POST: BlogTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTypesModel = await _context.BlogsTypes.FindAsync(id);
            if (blogTypesModel != null)
            {
                _context.BlogsTypes.Remove(blogTypesModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogTypesModelExists(int id)
        {
            return _context.BlogsTypes.Any(e => e.BlogTypeId == id);
        }
    }
}
