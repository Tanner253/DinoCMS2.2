﻿using DinoCMS.Data;
using DinoCMS.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DinoCMS.Models.Services
{
    public class DinoService : IDinoManager
    {
      
            private DinoDbContext _context;

            public DinoService(DinoDbContext context)
            {
                _context = context;
            }

            public async Task CreateDinosaur(Dinosaur dinosaur)
            {
                _context.Add(dinosaur);
                await _context.SaveChangesAsync();

            }
            public async Task<Dinosaur> DeleteDinosaur(int id)
            {
                Dinosaur dino = await _context.Dinosaur.FirstOrDefaultAsync(m => m.ID == id);
                return dino;
            }

            public async Task DeleteDinosaurFR(int id)
            {
                var dino = await _context.Dinosaur.FindAsync(id);
                _context.Dinosaur.Remove(dino);
                await _context.SaveChangesAsync();
            }

            public async Task<Dinosaur> GetDinosaur(int id)
            {
                var dinosaur = await _context.Dinosaur.FirstOrDefaultAsync(m => m.ID == id);
                return dinosaur;
            }

            public async Task<IEnumerable<Dinosaur>> GetDinosaurs()
            {
                var dinosaurs = await _context.Dinosaur.ToListAsync();
                return dinosaurs;
            }

            public async Task UpdateDinosaur(int id, [Bind("ID,Name,Diet")]Dinosaur dinosaur)
            {
                _context.Update(dinosaur);
                await _context.SaveChangesAsync();
            }
        
            public bool DinosaurExists(int id)
            {
                return _context.Dinosaur.Any(m => m.ID == id);
            }

    }
}
