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
    public class BlogIndexModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public BlogIndexModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blog.ToListAsync();
        }
    }
}
