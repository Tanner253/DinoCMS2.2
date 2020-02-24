using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Controllers
{
    public class BlogsController : Controller
    {
        private DinoDbContext _context;

        public BlogsController(DinoDbContext context)
        {
            _context = context;
        }
        // GET: Blog
        public ViewResult Index()
        {
            var blogs = _context.Blog.ToList();
            return View(blogs);
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }
            var blogs = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            if (blogs == null) { return NotFound(); }
            return View(blogs);
        }


        //THIS FORSURE DOESN'T WORK || LEGACY CODE vvv
        /// <summary>
        /// I have no idea if his is going to work the diea is that icrewate an enpoint that takes in an id, and selects all the posts that are liked to that id, (the blog id)
        /// </summary>
        /// <param name="id">The blog id</param>
        /// <returns>a list of posts that are related to the blog id</returns>
        //public async Task<IActionResult> BlogPosts(int? id)
        //{
        //    if(id == null) { return NotFound(); }
        //    var blogPosts = await _context.Post.ToListAsync();
        //    if(blogPosts == null) { return NotFound(); }
        //    return View(blogPosts);
        //}





        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,BloggerName,Posts,User")] Models.Blog blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(blog);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null) { return NotFound(); }
            return View(blog);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("ID,Title,BloggerName,Posts,User")] Models.Blog blog)
        {

            if (id != blog.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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

            return View(blog);

        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var blog = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            if(blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var blog = await _context.Blog.FindAsync(id);
                _context.Blog.Remove(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}