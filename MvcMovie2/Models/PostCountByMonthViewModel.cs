using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie2.Models
{

    public class MonthsandPosts
    {
        public int Month { get; set; }
        public int PostCount { get; set; }
        public string MonthName
        {
            get
            {
                switch (Month)
                {
                    case 1:
                        return "January";
                        break;
                    case 2:
                        return "Febuary";
                        break;
                    case 3:
                        return "March";
                        break;
                    case 4:
                        return "April";
                        break;
                    case 5:
                        return "May";
                        break;
                    case 6:
                        return "June";
                        break;
                    case 7:
                        return "July";
                        break;
                    case 8:
                        return "August";
                        break;
                    case 9:
                        return "September";
                        break;
                    case 10:
                        return "October";
                        break;
                    case 11:
                        return "November";
                        break;
                    case 12:
                        return "December";
                        break;
                    default:
                        return "";
                }
            }
        }
    }
    public class PostCountByMonthViewModel
    {
        public List<MonthsandPosts> MonthandPosts;
        //public int MonthandPosts;
    }
}