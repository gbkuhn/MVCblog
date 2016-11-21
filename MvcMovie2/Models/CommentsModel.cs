using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie2.Models
{
    public class CommentsModel
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Key]
        public int ID { get; set; }
        public int BlogModelID { get; set; }
        //public virtual ICollection<BlogModel> BlogModel;
    }
}