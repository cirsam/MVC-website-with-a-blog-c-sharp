using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myblogs.Data;
using myblogs.Models;
using myblogs.Models.buttonTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myblogs.Controllers
{
    public class blogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public blogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: blog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModelClass = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogModelClass == null)
            {
                return NotFound();
            }
            var _UserId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(_UserId))
            {
                blogModelClass.UserId = _UserId;
            }
            return View(blogModelClass);
        }

        // GET: blog/Create
        public IActionResult Create()
        {
            var _UserId = User.Identity.GetUserId();
            ViewBag.UserId = _UserId;
            ViewBag.datecreated = DateTime.Now;
            return View();
        }

        // POST: blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,UserId,BlogTitle,SelectedblogTypeId,SelectedBlogPublishStatusOptionsId,BlogText,Datecreated")] BlogModelClass blogModelClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogModelClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(blogModelClass);
        }

        // GET: blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModelClass = await _context.Blogs.FindAsync(id);
            if (blogModelClass == null)
            {
                return NotFound();
            }
            var _UserId = User.Identity.GetUserId();
        ViewBag.UserId = _UserId;
            if (!string.IsNullOrEmpty(_UserId))
            {
                blogModelClass.UserId = _UserId;
            }
            return View(blogModelClass);
        }

        // POST: blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,UserId,BlogTitle,SelectedblogTypeId,SelectedBlogPublishStatusOptionsId,BlogText,Datecreated")] BlogModelClass blogModelClass)
        {
            if (id != blogModelClass.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogModelClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogModelClassExists(blogModelClass.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var _UserId = User.Identity.GetUserId();
                if (!string.IsNullOrEmpty(_UserId))
                {
                    blogModelClass.UserId = _UserId;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogModelClass);
        }

        // GET: blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModelClass = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogModelClass == null)
            {
                return NotFound();
            }
            var _UserId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(_UserId))
            {
                blogModelClass.UserId = _UserId;
            }

            return View(blogModelClass);
        }

        // POST: blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogModelClass = await _context.Blogs.FindAsync(id);
            if (blogModelClass != null)
            {
                var _UserId = User.Identity.GetUserId();
                if (!string.IsNullOrEmpty(_UserId))
                {
                    blogModelClass.UserId = _UserId;
                }

                _context.Blogs.Remove(blogModelClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogModelClassExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
