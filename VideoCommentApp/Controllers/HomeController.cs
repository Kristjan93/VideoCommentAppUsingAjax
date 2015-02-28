using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoBlogApplication.Models;

namespace VideoCommentApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var result = CommentRepository.Instance.GetComments();    

               return View(result);
        }

         [HttpGet]
        public ActionResult getIt()
         {
             var result = CommentRepository.Instance.GetComments();
             var newResult = from c in result
                             select new
                             {
                                 CommentDate = c.CommentDate.ToString(),
                                 ID = c.ID,
                                 CommentText = c.CommentText,
                                 Username = c.Username
                             };
             return Json(newResult, JsonRequestBehavior.AllowGet);
         }
         [HttpGet]
         public ActionResult getItLikes()
         {
             var result = CommentRepository.Instance.GetLikes();
             var newResult = from a in result
                             select new
                             {
                                 LikeDate = a.LikeDate.ToString(),
                                 ID = a.ID,
                                 LikeUsername = a.LikeUsername
                             };
             return Json(newResult, JsonRequestBehavior.AllowGet);
         }

        [HttpPost]
        //fallid tekur inn tilvik af comment sem er fengid ut script'unni sem json
        public ActionResult Index(Comment formData)
        {
            //ef input field'id er ekki tomt ta er buid til nytt tilvik af comment og gildi sett inn
            if (!String.IsNullOrEmpty(formData.CommentText))
            {
                Comment c = new Comment();

                c.CommentText = formData.CommentText;

                string strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
               
                if (!String.IsNullOrEmpty(strUser))
                {
                    int slashPos = strUser.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUser = strUser.Substring(slashPos + 1);
                    }
                    c.Username = strUser;

                    CommentRepository.Instance.AddComment(c);
                }
                else
                {
                    c.Username = "Unknown user";
                }

                //nae i oll comment'in
                var result = CommentRepository.Instance.GetComments();

                //labba i gegnum oll comment'in
                //set likedata = string
                //allt annad helst eins
                var newResult = from a in result
                                select new
                                {
                                    CommentDate = a.CommentDate.ToString(),
                                    ID = a.ID,
                                    CommentText = a.CommentText,
                                    Username = a.Username
                                };
                return Json(newResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("CommentText", "Comment text cannot be empty!");
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LikeIt(Likes that)
        {  
            //checking if like is already there
            var checkLike = CommentRepository.Instance.GetLikes();

            String strUserCheck = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                if (!String.IsNullOrEmpty(strUserCheck))
                {
                    int slashPos = strUserCheck.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUserCheck = strUserCheck.Substring(slashPos + 1);
                    }
                }

            foreach (var element in checkLike)
            {
                if(element.LikeUsername == strUserCheck){
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            //---------------------- check ends here----------------------
            Likes l = new Likes();

            String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (!String.IsNullOrEmpty(strUser))
            {
                int slashPos = strUser.IndexOf("\\");
                if (slashPos != -1)
                {
                    strUser = strUser.Substring(slashPos + 1);
                }
                l.LikeUsername = strUser;

                CommentRepository.Instance.AddLike(l);
            }
            else
            {
                l.LikeUsername = "Unknown user";
            }

            //saeki oll like'in
            var result = CommentRepository.Instance.GetLikes();

            //labba i gegnum oll like'in
            //set likedata = string
            //allt annad helst eins
            var newResult = from a in result
                            select new
                            {
                                LikeDate = a.LikeDate.ToString(),
                                ID = a.ID,
                                LikeUsername = a.LikeUsername
                            };
            return Json(newResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}