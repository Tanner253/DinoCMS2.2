using DinoCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Data
{
    public class DinoDbContext : DbContext
    {
        public DinoDbContext(DbContextOptions<DinoDbContext> options) : base(options)
        {


        }
        public DbSet<Dinosaur> Dinosaur { get; set; }

    }


}
