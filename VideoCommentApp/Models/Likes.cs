using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoBlogApplication.Models
{
  
       public class Likes
       {
           public int ID { get; set; }
           public String LikeUsername { get; set; }
           public DateTime LikeDate { get; set; }

           public int CommmentID { get; set; }
       }
   
}