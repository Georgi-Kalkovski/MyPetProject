using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPetProject.Data;
using MyPetProject.Data.Common.Repositories;
using MyPetProject.Data.Models;

namespace MyPetProject.Web.Controllers
{
    public class BreedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BreedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Breeds
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            if (name == "Create")
            {
                return this.View();
            }

            var applicationDbContext = _context.Breeds.Include(b => b.User)
                .Where(x => x.KingdomName == name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breed = await _context.Breeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breed == null)
            {
                return NotFound();
            }

            return View(breed);
        }

        // GET: Breeds/Create
        [HttpGet("/Breeds/Create/")]
        public IActionResult Create()
        {
            ViewData["KingdomName"] = new SelectList(_context.Kingdoms, "Name", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Breeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Breeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breed);
                await _context.SaveChangesAsync();
                return RedirectToAction(breed.KingdomName);
            }
            ViewData["KingdomName"] = new SelectList(_context.Kingdoms, "Name", "Name", breed.KingdomName);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", breed.UserId);
            return View(breed);
        }

        // GET: Breeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breed = await _context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }
            ViewData["KingdomName"] = new SelectList(_context.Kingdoms, "Name", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", breed.UserId);
            return View(breed);
        }

        // POST: Breeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (id != breed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreedExists(breed.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(breed.KingdomName);
            }
            ViewData["KingdomName"] = new SelectList(_context.Kingdoms, "Name", "Name", breed.KingdomName);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", breed.UserId);
            return View(breed);
        }

        // GET: Breeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breed = await _context.Breeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breed == null)
            {
                return NotFound();
            }

            return View(breed);
        }

        // POST: Breeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);
            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();
            return RedirectToAction(breed.KingdomName);
        }

        private bool BreedExists(int id)
        {
            return _context.Breeds.Any(e => e.Id == id);
        }
    }
}
