using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models.Interfaces
{
    public interface IBlogManager
    {

        Task CreateBlog(Blog blog);

        Task UpdateBlog(int id, Blog blog);

        Task<Blog> DeleteBlog(int id);

        Task DeleteBlogFR(int id);


        Task<Blog> GetBlog(int id);

        Task<IEnumerable<Blog>> GetBlogs();

        bool BlogExists(int id);
    }
    public interface IPostManager
    {
        Task CreatePost(Post post);

        Task UpdatePost(int id,Post post);

        Task<Post> DeletePost(int id);

        Task DeletePostFR(int id);


        Task<Post> GetPost(int id);

        Task<IEnumerable<Post>> GetPosts();

        bool PostExists(int id);
    }
}
