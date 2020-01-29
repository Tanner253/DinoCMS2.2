using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DinoCMS.Data;
using DinoCMS.Models;

namespace DinoCMS
{
    public class DetailsModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public DetailsModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

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
    }
}
