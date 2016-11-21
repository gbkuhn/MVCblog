using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Microsoft.Owin.Security;

namespace MvcMovie2.Models
{
    public class StatisticsViewModel 
    {

        public List<UserViewModel> UserEmails = new List<UserViewModel>();

        public List<string> PostTitles = new List<string>();


    }
}