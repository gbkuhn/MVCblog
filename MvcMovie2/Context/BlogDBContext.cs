using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcMovie2.Controllers;
using MvcMovie2.Models;

namespace MvcMovie2.Context
{
    public class BlogDBContext:DbContext
    {
        public DbSet<BlogModel> Posts { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
    }
}