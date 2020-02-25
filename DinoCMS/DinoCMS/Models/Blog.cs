using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Created By")]
        public string BloggerName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }


        //nav
        public ApplicationUser User { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedOn = DateTime.Now;
        public string Content { get; set; }

        
        // public ICollection<Comment> Comments { get; set; }

        //nav ???
        public int BlogId { get; set; } 
        public Blog Blog { get; set; }
       
    }
    public class Comment
    {
        public string Content { get; set; }
    }
}
