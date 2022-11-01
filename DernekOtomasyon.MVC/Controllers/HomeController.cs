using DernekOtomasyon.DATA.Entity;
using DernekOtomasyon.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DernekOtomasyon.MVC.Controllers
{
    public class HomeController : Base.BaseController
    {
        public ActionResult Index()
        {
            //return View();
            if (Session["OK"] != null)
            {

                return View();
            }
            else if (Session["Admin"] != null)
            {
                NewsModel nm = new NewsModel();

                nm.TotalCount = NewsRepo.GetList().Count();
                ViewBag.TotalCount = nm.TotalCount;
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
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

        public ActionResult AddEvent()
        {
            return View();
        }
        public ActionResult EventList()
        {
            List<Event> EventList = EventRepo.GetList().ToList();

            return View(EventList);
        }

        public ActionResult InsertEvent()
        {
            Event e = new Event();
            e.name = Request["name"];
            e.desc = Request["desc"];
            e.startDate = Convert.ToDateTime(Request["StartAt"]);
            e.endDate = Convert.ToDateTime(Request["EndAt"]);
            EventRepo.Insert(e);
            _uow.SaveChanges();
            return RedirectToAction("EventList");
        }

        public ActionResult DeleteEvent(int ID)
        {
            EventRepo.Delete(ID);
            _uow.SaveChanges();
            return RedirectToAction("EventList");
        }

        public ActionResult Katil(int ID)
        {
            int MemberID = Convert.ToInt32(Session["MemberID"]);
            if (EventUserRepo.Count(t => t.EventID == ID && t.MemberID == MemberID) > 0)
            {
                Session["Message"] = "Zaten kayıt olmuşsunuz.";
            }
            else
            {

                Session["Message"] = "Kayıt Başarılı";
                EventUser eu = new EventUser();
                eu.EventID = ID;
                eu.MemberID = Convert.ToInt32(Session["MemberID"]);
                EventUserRepo.Insert(eu);
                _uow.SaveChanges();

            }
            return RedirectToAction("EventList");
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login","Account");
        }


    }
}