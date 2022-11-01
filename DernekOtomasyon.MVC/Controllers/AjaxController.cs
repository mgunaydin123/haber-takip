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
    public class AjaxController : BaseController
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetText()
        {

            var News = NewsRepo.GetList().ToList();

            return Json(News, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCategory(string categoryName)
        {
            Response r = new Response();
            try
            {
                Category a = new Category();
                a.CategoryName = categoryName;
                CategoryRepo.Insert(a);
                _uow.SaveChanges();
                r.status = "Kategori Eklendi";


                return Json(r, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                r.status = ex.Message;

                return Json(r, JsonRequestBehavior.AllowGet);

            }

        }

        [AllowAnonymous]
        [HttpDelete]
        public JsonResult DeleteCategory(int ID)
        {

            Category a = CategoryRepo.FirstOrDefault(t => t.ID == ID);
            CategoryRepo.Delete(a.ID);
            _uow.SaveChanges();
            Response r = new Response();
            r.status = "Silindi";
            return Json(r,JsonRequestBehavior.AllowGet);

        }



        [HttpPut]
        public JsonResult UpdateCategory(string oldName, string newName)
        {
            Response r = new Response();
            try
            {
                Category c = CategoryRepo.FirstOrDefault(t => t.CategoryName == oldName);
                c.CategoryName = newName;
                _uow.SaveChanges();
                r.status = oldName + " kategorisinin ismi " + newName + " adıyla değiştirildi";


                return Json(r, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                r.status = ex.Message;

                return Json(r, JsonRequestBehavior.AllowGet);

            }

        }
    }
}