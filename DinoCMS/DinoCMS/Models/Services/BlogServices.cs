using DinoCMS.Data;
using DinoCMS.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models.Services
{
    public class BlogServices : IBlogManager
    {
        private DinoDbContext _context;
        public BlogServices(DinoDbContext context)
        {
            _context = context;
        }

        public async Task CreateBlog(Blog blog)
        {
            _context.Add(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> DeleteBlog(int id)
        {
            Blog blog = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            return blog;
        }

        public async Task DeleteBlogFR(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> GetBlog(int id)
        {
            var blog = await _context.Blog.FirstOrDefaultAsync(m => m.Id == id);
            return blog;
        }

        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            var blogs = await _context.Blog.ToListAsync();
            return blogs;
        }

        public async Task UpdateBlog(int id, [Bind("ID,Title,BloggerName,Posts,User")]Blog blog)
        {
            _context.Update(blog);
            await _context.SaveChangesAsync();
        }
        public bool BlogExists(int id)
        {
            return _context.Dinosaur.Any(m => m.ID == id);
        }
    }

    public class PostServices : IPostManager
    {
        private DinoDbContext _context;

        public PostServices(DinoDbContext context)
        {
            _context = context;
        }
        public async Task CreatePost(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> DeletePost(int id)
        {
            Post post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            return post;
        }

        public async Task DeletePostFR(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
        }

        public Task<Post> GetPost(int id)
        {
            var post = _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            return post;

        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Post.ToListAsync();
            return posts;
        }

        public bool PostExists(int id)
        {
            return _context.Post.Any(m => m.Id == id);
        }

        public async Task UpdatePost(int id, [Bind("Id,Title,DateCreated,Content,BlogId,Blog")]Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
