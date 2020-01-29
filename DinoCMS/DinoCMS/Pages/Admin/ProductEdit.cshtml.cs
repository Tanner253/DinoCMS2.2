using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DinoCMS
{
    [Authorize(Policy = "Admin")]
    public class ProductEditModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public ProductEditModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dinosaur Dinosaur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dinosaur = await _context.Dinosaur.FirstOrDefaultAsync(m => m.ID == id);

            if (Dinosaur == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Dinosaur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DinosaurExists(Dinosaur.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./List");
        }

        private bool DinosaurExists(int id)
        {
            return _context.Dinosaur.Any(e => e.ID == id);
        }
    }
}
