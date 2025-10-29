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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsModel = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentsId == id);
            if (commentsModel == null)
            {
                return NotFound();
            }

            return View(commentsModel);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            var _UserId = User.Identity.GetUserId();
            ViewBag.UserId = _UserId;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentsId,BlogId,Comment,UserId,NickName,DateCreated")] CommentsModel commentsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commentsModel);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsModel = await _context.Comments.FindAsync(id);
            if (commentsModel == null)
            {
                return NotFound();
            }
            return View(commentsModel);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentsId,BlogId,Comment,UserId,NickName,DateCreated")] CommentsModel commentsModel)
        {
            if (id != commentsModel.CommentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsModelExists(commentsModel.CommentsId))
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
            return View(commentsModel);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsModel = await _context.Comments
                .FirstOrDefaultAsync(m => m.CommentsId == id);
            if (commentsModel == null)
            {
                return NotFound();
            }

            return View(commentsModel);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentsModel = await _context.Comments.FindAsync(id);
            if (commentsModel != null)
            {
                _context.Comments.Remove(commentsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsModelExists(int id)
        {
            return _context.Comments.Any(e => e.CommentsId == id);
        }
    }
}
