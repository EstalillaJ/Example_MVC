using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example_MVC.Models;

namespace Example_MVC.Controllers
{
    public class PostController : Controller
    {

        public ActionResult New(string author, string message)
        {
            Database.SavePost(author, message);
            return RedirectToAction("ViewForum");
        }

        public ActionResult ViewForum()
        {   
            Forum forum = Database.LoadForum();
            return View(forum);
        }
    }
}