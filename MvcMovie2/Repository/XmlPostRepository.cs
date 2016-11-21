using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using MvcMovie2.Datasets;
using MvcMovie2.Models;


namespace MvcMovie2.Repository
{
    public class XmlPostRepository : IPostRepository 
    {

        public BlogModelDataset.PostRow GetPostByTitle(string blogTitle)
        {
            var xmlData = ReadXmlDataset();

            return xmlData.Post.SingleOrDefault(v => v.Title.Equals(blogTitle));
        }
        /*
        public BlogModel GetPostByTitle(string blogTitle)
        {
            var xmlData = ReadXmlDataset();

            //return db.Posts.SingleOrDefault(v => v.Title.Equals(blogTitle));

            return xmlData.Post.SingleOrDefault(v => v.Title.Equals(blogTitle));
        }
        */
        
        public List<UserViewModel> GetUserEmailList()
        {
            var xmlData = ReadXmlDataset();

            var numberOfUsers = xmlData
                .Post
                .GroupBy(v => v.User)
                .Select(v => new UserViewModel() { EmailAddress = v.Key, PostCount = v.Count() })
                .OrderByDescending(m => m.EmailAddress)
                .ToList();
            //assigns list of users to emailList and is passed into partialview

            return numberOfUsers;
        }

        public StatisticsViewModel Statistics(string usernameStat)
        {
            var xmlData = ReadXmlDataset();

            StatisticsViewModel model = new StatisticsViewModel();

            if (usernameStat == null)
            {
                model.PostTitles = xmlData.Post.Select(v => v.Title).OrderByDescending(m => m).ToList();
                model.UserEmails = GetUserEmailList();
            }
            else
            {
                var x = (from p in xmlData.Post where p.User == usernameStat group p by p.User into g select g.Key).ToList();

                model.PostTitles = xmlData.Post.Where(v => v.User == usernameStat).Select(v => v.Title).ToList();
                //LINQ
            }

            return model;
        }

        public List<BlogModel> GetPosts(string search)
        {
            var xmlData = ReadXmlDataset();
            var filteredPosts = xmlData.Post.Where(v => v.User.Equals(search) || v.Title.Contains(search))
                .OrderByDescending(v => v.PostDate)
                .ToList();

            return MapToBlogModelList(filteredPosts);
        }

        public List<Models.BlogModel> GetPosts()
        {
            var xmlData = ReadXmlDataset();

            return MapToBlogModelList(xmlData.Post.ToList());
        }

        private List<Models.BlogModel> MapToBlogModelList(List<BlogModelDataset.PostRow> searchTerm)
        {
            List<BlogModel> post = (from row in searchTerm
                                    select new Models.BlogModel
                                    {
                                        ID = row.ID, //Convert row to int  
                                        User = row.User,
                                        Title = row.Title,
                                        PostDate = row.PostDate,
                                        Body = row.Body
                                    }).ToList();
            return post;
        }

        public static BlogModelDataset ReadXmlDataset()
        {
            string xmlData = HttpContext.Current.Server.MapPath("~/XmlData/Posts.xml");
            var ds = new BlogModelDataset();

            ds.ReadXml(xmlData);

            return ds;
        }

        public static BlogModelDataset ReadXmlDatasetForComments()
        {

            string xmlData = HttpContext.Current.Server.MapPath("~/XmlData/Comments.xml");
            var ds = new BlogModelDataset();

            ds.ReadXml(xmlData);

            return ds;
        }

        public List<string> GetBlogTitleList(string usernameStat)
        {
            var xmlData = ReadXmlDataset();
            List<string> filteredTitleList = null;

            if (!String.IsNullOrEmpty(usernameStat))
            {
                filteredTitleList = xmlData.Post.Where(v => v.User == usernameStat).Select(v => v.Title).ToList();
            }

            return filteredTitleList;
        }

        public List<BlogModelDataset.CommentRow> ViewComments(int blogModelId)
        {

            List<BlogModelDataset.CommentRow> model = null;

            var xmlData = ReadXmlDatasetForComments();

            try
            {
                
                model = xmlData.Comment.Where(v => v.BlogModelID.Equals(blogModelId)).ToList();
                 
            }
            catch (Exception e)
            {
                var z = e;
            }

            return model;
        }

        public void AddComment(Models.CommentsModel model)
        {
            var xmlData = ReadXmlDatasetForComments();

            xmlData.Comment.AddCommentRow(model.Title, model.Body, model.BlogModelID);

            xmlData.WriteXml("~/XmlData/Comments.xml");
        }

        public List<Models.MonthsandPosts> GetColumnChartData()
        {
            var xmlData = ReadXmlDataset();

            return xmlData.Post.GroupBy(v => v.PostDate.Month)
                  .Select(x => new MonthsandPosts() { Month = x.Key, PostCount = x.Count() }).ToList();
        }
    }
}