using DernekOtomasyon.DATA.Entity;
using DernekOtomasyon.MVC.Controllers.Base;
using DernekOtomasyon.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DernekOtomasyon.MVC.Controllers
{
    public class SiteController : BaseController
    {
        // GET: Site
        public ActionResult Index()
        {

            NewsModel nm = new NewsModel();
            nm.CategoryList = CategoryRepo.GetList().ToList();
            
            return View(nm);
        }

        public ActionResult CategoryNews(int CategoryID)
        {
            NewsModel nm = new NewsModel();

            nm.CategoryList = CategoryRepo.GetList().ToList();
            nm.News = NewsRepo.GetList().Where(t => t.CategoryID == CategoryID).ToList();

            return View(nm);
        }

        public ActionResult NewsDetail(int ID)
        {
            NewsModel nm = new NewsModel();
            nm.CategoryList = CategoryRepo.GetList().ToList();
            nm.NewsDetail = NewsRepo.FirstOrDefault(t => t.ID == ID);
            nm.CommentList = CommentRepo.GetList().Where(t => t.NewsID == ID).ToList();
            return View(nm);
        }

        public ActionResult YorumYap()
        {
            var newsid = Convert.ToInt32(Request["id"]);
            string text = Request["commenttext"].ToString();
            string name = Request["name"].ToString();

            Comment c = new Comment();
            c.Name = name;
            c.CommentText = text;
            c.NewsID = newsid;
            CommentRepo.Insert(c);
            _uow.SaveChanges();

            return RedirectToAction("NewsDetail", "Site", new { ID = newsid });
        }
    }
}