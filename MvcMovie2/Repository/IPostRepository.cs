using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMovie2.Datasets;
using MvcMovie2.Models;

namespace MvcMovie2.Repository
{
    public interface IPostRepository
    {

        BlogModelDataset.PostRow GetPostByTitle(string blogTitle);

        List<UserViewModel> GetUserEmailList();

        StatisticsViewModel Statistics(string usernameStat);

        List<BlogModel> GetPosts(string search);

        List<BlogModel> GetPosts();

        List<string> GetBlogTitleList(string usernameStat);

        List<BlogModelDataset.CommentRow> ViewComments(int blogModelId);

        void AddComment(CommentsModel model);

        List<MonthsandPosts> GetColumnChartData();
    }
}
