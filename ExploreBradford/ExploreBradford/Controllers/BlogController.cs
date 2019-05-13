using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreBradford.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ExploreBradford.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }
        [Route("")]
        public IActionResult Index()
        {

            var posts = _db.Posts.OrderByDescending(x=>x.Posted).Take(5).ToArray();

            return View(posts);
        }
        
        

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year,int month,string key)
        {
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Anwar ul Haq",
                Body = "This is a great post for blog"
            };

            return View(post);
        }

        [HttpGet,Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,Route("create")]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;
            _db.Posts.Add(post);
            _db.SaveChanges();

            return View(post);
            
        }

        
    }
}