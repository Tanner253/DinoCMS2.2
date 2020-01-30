﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DinoCMS.Controllers
{

    /// <summary>
    /// inject dependencies
    /// </summary>
    public class DinosaursController : Controller
    {
        private readonly DinoDbContext _context;


        public DinosaursController(DinoDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// serves index page with sorted data
        /// </summary>
        /// <param name="type">Diet type for sorting</param>
        /// <param name="searchString">Search string for search</param>
        /// <returns></returns>
        public ViewResult Index(Dinosaur.Food type, string searchString)
        {

            var dino = from s in _context.Dinosaur
                       select s;


            ViewData["HerbiSort"] = type == Dinosaur.Food.Herbivore ? "Herbi" : "Herbivore";
            ViewData["CarniSort"] = type == Dinosaur.Food.Carnivore ? "Carni" : "Carnivore";
            ViewData["OmniSort"] = type == Dinosaur.Food.Omnivore ? "Omni" : "Omnivore";

            switch (type)
            {
                case Dinosaur.Food.Herbivore:
                    dino = _context.Dinosaur.OrderByDescending(c => c.Diet == type);

                    break;
                case Dinosaur.Food.Carnivore:
                    dino = _context.Dinosaur.OrderByDescending(c => c.Diet == type);

                    break;
                case Dinosaur.Food.Omnivore:
                    dino = _context.Dinosaur.OrderByDescending(c => c.Diet == type);

                    break;
                default:
                    dino = _context.Dinosaur.OrderBy(s => s.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                dino = dino.Where(s => s.Name.Contains(searchString));
            }


            return View(dino.ToList());
        }
    


    
    // GET: Dinosaurs
    //public async Task<IActionResult> Index()
    //{

    //    return View(await _context.Dinosaur.ToListAsync());
    //}

    // GET: Dinosaurs/Details/5
    /// <summary>
    /// gets dinosaur by id and returns all of the data corrisponding to that obj
    /// </summary>
    /// <param name="id">object in question</param>
    /// <returns>data of object</returns>
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dinosaur = await _context.Dinosaur
            .FirstOrDefaultAsync(m => m.ID == id);
        if (dinosaur == null)
        {
            return NotFound();
        }

        return View(dinosaur);
    }
    [Authorize(Policy = "ADMIN")]
    // GET: Dinosaurs/Create
    
    public IActionResult Create()
    {
        return View();
    }

    // POST: Dinosaurs/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598
    [Authorize(Policy = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Name,Diet,NeedToKnow,Behavior,SocialInteraction,PackLimits,Image,Additionalinfo")] Dinosaur dinosaur)
    {
        if (ModelState.IsValid)
        {
            _context.Add(dinosaur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dinosaur);
    }
    [Authorize(Policy = "ADMIN")]
    // GET: Dinosaurs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dinosaur = await _context.Dinosaur.FindAsync(id);
        if (dinosaur == null)
        {
            return NotFound();
        }
        return View(dinosaur);
    }

    // POST: Dinosaurs/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize(Policy = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Diet,NeedToKnow,Behavior,SocialInteraction,PackLimits,Image,Additionalinfo")] Dinosaur dinosaur)
    {
        if (id != dinosaur.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(dinosaur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DinosaurExists(dinosaur.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(dinosaur);
    }
        /// <summary>
        /// Retrieves info abot obj
        /// </summary>
        /// <param name="id">obj in question</param>
        /// <returns>the object</returns>
    [Authorize(Policy = "ADMIN")]
    // GET: Dinosaurs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dinosaur = await _context.Dinosaur
            .FirstOrDefaultAsync(m => m.ID == id);
        if (dinosaur == null)
        {
            return NotFound();
        }

        return View(dinosaur);
    }
        /// <summary>
        /// deletes the object after confirmation of deletion
        /// </summary>
        /// <param name="id">obj being deleted</param>
        /// <returns>to list after deletion</returns>
    [Authorize(Policy = "ADMIN")]
    // POST: Dinosaurs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var dinosaur = await _context.Dinosaur.FindAsync(id);
        _context.Dinosaur.Remove(dinosaur);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
        /// <summary>
        /// is called before deletion to make sure that the obj being deleted even exists.
        /// </summary>
        /// <param name="id">obj being checked</param>
        /// <returns>grabs existant dinosaur, if none exist then... ? </returns>
    private bool DinosaurExists(int id)
    {
        return _context.Dinosaur.Any(e => e.ID == id);
    }
}
}
