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
        [Route("")]
        public IActionResult Index()
        {
            return View();
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
    }
}