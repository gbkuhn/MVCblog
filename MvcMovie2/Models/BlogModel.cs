using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcMovie2.Models
{
    public class BlogModel : IEnumerable
    {
        
        [DisplayName("Author:")]
        public string User { get; set; }
        [Key]
        public int ID { get; set; }

        //[Required]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Title { get; set; }
       

        [DisplayName("Post Date:")]
        public DateTime PostDate { get; set; }
         
        
        [DisplayName("Post")]
        public string Body { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}