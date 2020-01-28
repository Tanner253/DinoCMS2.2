using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DinoCMS
{
    [Authorize(Policy = "Admin")]
    public class CreateDinoModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public CreateDinoModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Dinosaur Dinosaur { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Dinosaur.Add(Dinosaur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./List");
        }
    }
}