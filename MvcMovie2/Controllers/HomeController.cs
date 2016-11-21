using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMovie2.Datasets;
using MvcMovie2.Models;
using MvcMovie2.Repository;

namespace MvcMovie2.Controllers
{
    public class HomeController : Controller
    {
        private IPostRepository postRepository;

        public HomeController()
        {
            postRepository = new XmlPostRepository();
        }

        public HomeController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public ActionResult Index()
        {
            return View(postRepository.GetPosts());
        }

        public ActionResult Search(string search)
        {
            if (search != null)
            {
                ViewBag.Message = search;

                return View(postRepository.GetPosts(search).ToList());
            }
            return View();
        }

        public ActionResult Statistics(string usernameStat)
        {
            return View(postRepository.Statistics(usernameStat));
        }

        public ActionResult UserEmailList()
        {
            var emailList = GetUserEmailList();

            return PartialView("UserEmailList", emailList);
        }

        public List<UserViewModel> GetUserEmailList()
        {
            var numberOfUsers = postRepository.GetUserEmailList();

            return numberOfUsers;
        }

        public ActionResult BlogPost(string blogTitle)
        {
            BlogModelDataset.PostRow model = null;

            if (!String.IsNullOrEmpty(blogTitle))
            {
                model = postRepository.GetPostByTitle(blogTitle);
            }

            return PartialView(model);
        }

        public ActionResult GetBlogTitleList(string usernameStat)
        {
            return PartialView(postRepository.GetBlogTitleList(usernameStat));
        }

        [HttpGet]
        public ActionResult ViewComments(int BlogModelId)
        {
            return View("_CommentsForPost", postRepository.ViewComments(BlogModelId));
        }

        [HttpGet]
        public ActionResult PostComment(int BlogModelId)
        {
            CommentsModel model = new CommentsModel();
            model.BlogModelID = BlogModelId;

            return View("_LeaveCommentForPost");
        }

        [HttpPost]
        public ActionResult PostComment(CommentsModel model)
        {

            model.User = User.Identity.Name;

            if (ModelState.IsValid)
            {
                postRepository.AddComment(model);

                return RedirectToAction("ViewComments", new { model.BlogModelID });
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            }

            return View("_CommentsForPost");
        }

        public ActionResult GetColumnChart()
        {
            return PartialView("_ColumnChart", postRepository.GetColumnChartData());
        }

    }
}