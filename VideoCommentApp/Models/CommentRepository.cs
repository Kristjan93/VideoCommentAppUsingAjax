using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoBlogApplication.Models
{
    public class CommentRepository
    {
        private static CommentRepository _instance;

        public static CommentRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommentRepository();
                return _instance;
            }
        }

        private List<Comment> m_comments = null;

        private List<Likes> m_likes = null;

        private CommentRepository()
        {
            this.m_comments = new List<Comment>();
            

            this.m_likes = new List<Likes>();
            //Likes like1 = new Likes { ID = 1, LikeDate = new DateTime(2014, 3, 1, 12, 30, 00), LikeUsername = "Siggi Hall" };
            //this.m_likes.Add(like1);
        }
        
       public IEnumerable<Likes> GetLikes()
        {
            var result = from l in m_likes
                         orderby l.LikeDate ascending
                         select l;
            return result;
        }
        
        public IEnumerable<Comment> GetComments()
        {
            var result = from c in m_comments
                         orderby c.CommentDate ascending
                         select c;

            return result;
        }

        public void AddComment(Comment c)
        {
            int newID = 1;
            if (m_comments.Count() > 0)
            {
                newID = m_comments.Max(x => x.ID) + 1;
            }
            c.ID = newID;
            c.CommentDate = DateTime.Now;
            m_comments.Add(c);
        }
        public void AddLike(Likes l)
        {
            int newID = 1;
            if (m_likes.Count() > 0)
            {
                newID = m_likes.Max(x => x.ID) + 1;
            }
            l.ID = newID;
            l.LikeDate = DateTime.Now;
            m_likes.Add(l);
        }
    }
}