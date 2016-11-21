using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMovie2.Context;
using MvcMovie2.Datasets;
using MvcMovie2.Models;

namespace MvcMovie2.Repository
{
    public class PostRepository : IPostRepository
    {
        private BlogDBContext db = new BlogDBContext();

        /* will have to be incldued if the repository is supposed ot be used again, method below is placeholder so it will compile
        public BlogModelDataset.PostRow GetPostByTitle(string blogTitle)
        {
            return db.Posts.SingleOrDefault(v => v.Title.Equals(blogTitle));
        }*/

        public BlogModelDataset.PostRow GetPostByTitle(string blogTitle)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetUserEmailList()
        {
            var NumberOfUsers = db
                .Posts
                .GroupBy(v => v.User)
                .Select(v => new UserViewModel() { EmailAddress = v.Key, PostCount = v.Count() })
                .OrderByDescending(m => m.EmailAddress)
                .ToList();
            //assigns list of users to emailList and is passed into partialview

            return NumberOfUsers;
        }

        public StatisticsViewModel Statistics(string usernameStat)
        {
            StatisticsViewModel model = new StatisticsViewModel();

            if (usernameStat == null)
            {
                model.PostTitles = db.Posts.Select(v => v.Title).OrderByDescending(m => m).ToList();
                model.UserEmails = GetUserEmailList();
            }
            else
            {
                var x = (from p in db.Posts where p.User == usernameStat group p by p.User into g select g.Key).ToList();

                model.PostTitles = db.Posts.Where(v => v.User == usernameStat).Select(v => v.Title).ToList();
                //LINQ
            }

            return model;
            //value = value.Where(v => v.User.Contains(search) || v.Title.Contains(search));
        }

        public List<BlogModel> GetPosts(string search)
        {
            return db.Posts.Where(v => v.User.Equals(search) || v.Title.Contains(search)).OrderByDescending(v => v.PostDate).ToList();
        }

        public List<BlogModel> GetPosts()
        {
            IQueryable<BlogModel> value = db.Posts;

            return value.OrderByDescending(v => v.PostDate).ToList();
        }

        public List<string> GetBlogTitleList(string usernameStat)
        {
            List<string> model = null;

            if (!String.IsNullOrEmpty(usernameStat))
            {
                model = db.Posts.Where(v => v.User == usernameStat).Select(v => v.Title).ToList();
            }

            return model;
        }

        /*
        [HttpGet]
        public List<BlogModelDataset.CommentRow> ViewComments(int blogModelId)
        {
            List<CommentsModel> model = null;

            try
            {
                model = db.Comments.Where(v => v.BlogModelID.Equals(blogModelId)).ToList();
            }
            catch (Exception e)
            {
                var z = e;
            }

            return model;
        }
        */
        public List<BlogModelDataset.CommentRow> ViewComments(int blogModelId)
        {
            throw new NotImplementedException();
        }

        public void AddComment(CommentsModel model)
        {
            db.Comments.Add(model);
            db.SaveChanges();
        }

        public List<MonthsandPosts> GetColumnChartData()
        {
            return db.Posts.GroupBy(v => v.PostDate.Month)
                    .Select(x => new MonthsandPosts() { Month = x.Key, PostCount = x.Count() }).ToList();
        }
    }
}