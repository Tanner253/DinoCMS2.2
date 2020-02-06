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
    public class PostCreateModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public PostCreateModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BlogId"] = new SelectList(_context.Blog, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Post.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}