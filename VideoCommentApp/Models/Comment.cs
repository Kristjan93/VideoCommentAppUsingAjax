using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoBlogApplication.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public String CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public int counter { get; set; }
    }
}