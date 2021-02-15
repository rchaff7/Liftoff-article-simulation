using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using liftoff_storefront.Data;
using liftoff_storefront.Models;

namespace liftoff_storefront.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly StorefrontDbContext _context;

        public UserCommentsController(StorefrontDbContext context)
        {
            _context = context;
        }

        // GET: UserComments/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,ProductId,IdentityUserId")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", userComment.IdentityUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", userComment.ProductId);
            return View(userComment);
        }

        // GET: UserComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.FindAsync(id);
            if (userComment == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", userComment.IdentityUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", userComment.ProductId);
            return View(userComment);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,ProductId,IdentityUserId")] UserComment userComment)
        {
            if (id != userComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(userComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/home");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", userComment.IdentityUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", userComment.ProductId);
            return View(userComment);
        }

        // GET: UserComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments
                .Include(u => u.IdentityUser)
                .Include(u => u.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userComment = await _context.UserComments.FindAsync(id);
            _context.UserComments.Remove(userComment);
            await _context.SaveChangesAsync();
            return Redirect("/home");
        }

        private bool UserCommentExists(int id)
        {
            return _context.UserComments.Any(e => e.Id == id);
        }
    }
}
