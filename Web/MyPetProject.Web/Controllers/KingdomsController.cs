using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPetProject.Data;
using MyPetProject.Data.Models;

namespace MyPetProject.Web.Controllers
{
    public class KingdomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KingdomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kingdoms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kingdoms.Include(k => k.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kingdoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdoms
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kingdom == null)
            {
                return NotFound();
            }

            return View(kingdom);
        }

        // GET: Kingdoms/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Kingdoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kingdom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", kingdom.UserId);
            return View(kingdom);
        }

        // GET: Kingdoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdoms.FindAsync(id);
            if (kingdom == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", kingdom.UserId);
            return View(kingdom);
        }

        // POST: Kingdoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PicUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
        {
            if (id != kingdom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kingdom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KingdomExists(kingdom.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", kingdom.UserId);
            return View(kingdom);
        }

        // GET: Kingdoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kingdom = await _context.Kingdoms
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kingdom == null)
            {
                return NotFound();
            }

            return View(kingdom);
        }

        // POST: Kingdoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kingdom = await _context.Kingdoms.FindAsync(id);
            _context.Kingdoms.Remove(kingdom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KingdomExists(int id)
        {
            return _context.Kingdoms.Any(e => e.Id == id);
        }
    }
}
