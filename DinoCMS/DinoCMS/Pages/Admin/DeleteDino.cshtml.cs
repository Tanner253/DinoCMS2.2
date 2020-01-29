using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DinoCMS
{
    [Authorize(Policy = "Admin")]
    public class DeleteDinoModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public DeleteDinoModel(DinoCMS.Data.DinoDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Dinosaur = await _context.Dinosaur.FindAsync(id);

            if (Dinosaur != null)
            {
                _context.Dinosaur.Remove(Dinosaur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./List");
        }
    }
}
