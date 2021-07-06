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
    public class SubbreedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubbreedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subbreeds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Subbreeds.Include(s => s.Breed).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Subbreeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subbreed = await _context.Subbreeds
                .Include(s => s.Breed)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subbreed == null)
            {
                return NotFound();
            }

            return View(subbreed);
        }

        // GET: Subbreeds/Create
        public IActionResult Create()
        {
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Subbreeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,IsPet,BreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subbreed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Description", subbreed.BreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", subbreed.UserId);
            return View(subbreed);
        }

        // GET: Subbreeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subbreed = await _context.Subbreeds.FindAsync(id);
            if (subbreed == null)
            {
                return NotFound();
            }
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Description", subbreed.BreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", subbreed.UserId);
            return View(subbreed);
        }

        // POST: Subbreeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PicUrl,Description,IsPet,BreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreed)
        {
            if (id != subbreed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subbreed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubbreedExists(subbreed.Id))
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
            ViewData["BreedId"] = new SelectList(_context.Breeds, "Id", "Description", subbreed.BreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", subbreed.UserId);
            return View(subbreed);
        }

        // GET: Subbreeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subbreed = await _context.Subbreeds
                .Include(s => s.Breed)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subbreed == null)
            {
                return NotFound();
            }

            return View(subbreed);
        }

        // POST: Subbreeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subbreed = await _context.Subbreeds.FindAsync(id);
            _context.Subbreeds.Remove(subbreed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubbreedExists(int id)
        {
            return _context.Subbreeds.Any(e => e.Id == id);
        }
    }
}
