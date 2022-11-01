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
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }
        public ActionResult CategoryList()
        {
            List<Category> adminList = CategoryRepo.GetList().ToList();
            return View(adminList);
        }
        public ActionResult DeleteCategory(int ID)
        {
            Category a = CategoryRepo.FirstOrDefault(t => t.ID == ID);
            CategoryRepo.Delete(a.ID);
            _uow.SaveChanges();

            return RedirectToAction("CategoryList");

        }
        public ActionResult InsertCategory()
        {
            Category a = new Category();
            a.CategoryName = Request["category"];
            CategoryRepo.Insert(a);
            _uow.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public ActionResult AddNews()
        {
            NewsModel nm = new NewsModel();
            nm.CategoryList = CategoryRepo.GetList().ToList();

            return View(nm);
        }

        [ValidateInput(false)]
        public ActionResult InsertNews(HttpPostedFileBase file, string content)
        {
            var categoryid = Convert.ToInt32(Request["category"]);
            var memberId = Convert.ToInt32(Session["MemberID"]);

            News e = new News();
            e.Title = Request["title"];
            e.Spot = Request["spot"];
            e.CategoryID = categoryid;
            e.MemberID = memberId;
            e.Statu = 0;
            e.Content = content;
            e.Source = Request["source"];
            NewsRepo.Insert(e);
            _uow.SaveChanges();
            return RedirectToAction("NewsList");
        }

        public ActionResult NewsList()
        {
            List<News> EventList = NewsRepo.GetList().ToList();

            return View(EventList);
        }


        public ActionResult DeleteNews(int ID)
        {
            NewsRepo.Delete(ID);
            _uow.SaveChanges();
            return RedirectToAction("NewsList");
        }

        public ActionResult Onayla(int ID)
        {
            News news = NewsRepo.FirstOrDefault(t => t.ID == ID);
            news.Statu = 1;
            _uow.SaveChanges();
            return RedirectToAction("NewsList");
        }

        public ActionResult Report()
        {
            NewsModel nm = new NewsModel();
            nm.TotalCount = NewsRepo.GetList().Count();
            return View();
        }
    }
}