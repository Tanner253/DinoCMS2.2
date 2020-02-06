using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DinoCMS.Data;
using DinoCMS.Models;

namespace DinoCMS
{
    public class BlogCreateModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public BlogCreateModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Blog Blog { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Blog.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}