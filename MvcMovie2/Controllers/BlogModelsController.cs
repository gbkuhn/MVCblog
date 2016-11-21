using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcMovie2.Context;
using MvcMovie2.Models;
using MvcMovie2.Repository;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace MvcMovie2.Controllers
{
    public class BlogModelsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

         private IPostRepository postRepository;

        public BlogModelsController()
        {
            postRepository = new XmlPostRepository();
        }

        public BlogModelsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        // GET: BlogModels
        public ActionResult Index()
        {
            //return View(db.Posts.ToList());
            var debug = postRepository.GetPosts();

            return View(debug);
        }

        // GET: BlogModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.Posts.Find(id);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // GET: BlogModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User,Title,PostDate,Body")] BlogModel blogModel)
        {
           var xmlData = XmlPostRepository.ReadXmlDataset();

           xmlData.Post.AddPostRow(blogModel.ID, User.Identity.Name, blogModel.Title,blogModel.PostDate,blogModel.Body);

            xmlData.WriteXml("C:/Users/geoffrey.kuhn/Documents/Visual Studio 2013/Projects/MvcMovie2/MvcMovie2/XmlData/Posts.xml");

            return View(blogModel);
        }

        // GET: BlogModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.Posts.Find(id);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // POST: BlogModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,PostDate,Body")] BlogModel blogModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogModel);
        }

        // GET: BlogModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.Posts.Find(id);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // POST: BlogModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogModel blogModel = db.Posts.Find(id);
            db.Posts.Remove(blogModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
