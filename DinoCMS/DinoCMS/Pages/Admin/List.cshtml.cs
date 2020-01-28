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
    public class ListModel : PageModel
    {
        private readonly DinoCMS.Data.DinoDbContext _context;

        public ListModel(DinoCMS.Data.DinoDbContext context)
        {
            _context = context;
        }

        public IList<Dinosaur> Dinosaur { get;set; }

        public async Task OnGetAsync()
        {
            Dinosaur = await _context.Dinosaur.ToListAsync();
        }
    }
}
