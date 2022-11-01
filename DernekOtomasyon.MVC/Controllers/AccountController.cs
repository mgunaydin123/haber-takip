﻿using DernekOtomasyon.DATA.Entity;
using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DernekOtomasyon.MVC.Controllers
{
    public class AccountController : Base.BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            string ok = Session["OK"].ToString();

            return RedirectToAction("Index", "Home");
            if (ok =="ok")
            {

                return RedirectToAction("Welcome", "Home");
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
        }

        //kayıt işlemleri
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase image)
        {
            var link = System.Configuration.ConfigurationManager.AppSettings["link"];
            string name = Request["username"];
            string pass = Request["pass"];
            string email = Request["email"];

            var isControl = MemberRepo.FirstOrDefault(t => t.Username == name || t.Showname == email);
            if (isControl != null)
            {
                Session["Error"] = "Email veya Kullanıcı adı kullanımda";
                return RedirectToAction("Error", "Home");
            }
            else
            {

                Member m = new Member();
                m.Password = pass;
                m.Showname = email;
                m.UniqueKey = Guid.NewGuid().ToString();
                m.Username = name;
                m.Status = 0;

                //var kimden = new MailAddress("gnydn.mine.123@gmail.com", "Dernek Otomasyon Mine");
                //var kime = new MailAddress(email, name);
                //const string gMailParolaniz = "mine123321+";
                //const string konu = "Üyelik Onaylama";
                //string mailIcerik = "üyelik oynamak için <a href=\"" + link + "/Account/MemberSuccess?unique=" + m.UniqueKey + "\">buraya</a> tıklayınız";

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(kimden.Address, gMailParolaniz)
                //};
                //using (var mesaj = new MailMessage(kimden, kime)
                //{
                //    Subject = konu,
                //    Body = mailIcerik,
                //    IsBodyHtml = true
                //})
                //{
                //    smtp.Send(mesaj);
                //}




                MemberRepo.Insert(m);
                _uow.SaveChanges();
                return RedirectToAction("Login");
            }

        }



        //kayıt işlemleri
        public ActionResult RedaktorOlustur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedaktorOlustur(HttpPostedFileBase image)
        {
            var link = System.Configuration.ConfigurationManager.AppSettings["link"];
            string name = Request["username"];
            string pass = Request["pass"];
            string email = Request["email"];

            var isControl = MemberRepo.FirstOrDefault(t => t.Username == name || t.Showname == email);
            if (isControl != null)
            {
                Session["Error"] = "Email veya Kullanıcı adı kullanımda";
                return RedirectToAction("Error", "Home");
            }
            else
            {

                Member m = new Member();
                m.Password = pass;
                m.Showname = email;
                m.UniqueKey = Guid.NewGuid().ToString();
                m.Username = name;
                m.Status = 0;

                //var kimden = new MailAddress("gnydn.mine.123@gmail.com", "Dernek Otomasyon Mine");
                //var kime = new MailAddress(email, name);
                //const string gMailParolaniz = "mine123321+";
                //const string konu = "Üyelik Onaylama";
                //string mailIcerik = "üyelik oynamak için <a href=\"" + link + "/Account/MemberSuccess?unique=" + m.UniqueKey + "\">buraya</a> tıklayınız";

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(kimden.Address, gMailParolaniz)
                //};
                //using (var mesaj = new MailMessage(kimden, kime)
                //{
                //    Subject = konu,
                //    Body = mailIcerik,
                //    IsBodyHtml = true
                //})
                //{
                //    smtp.Send(mesaj);
                //}




                MemberRepo.Insert(m);
                _uow.SaveChanges();
                return RedirectToAction("Login");
            }

        }

        //giriş işlemleri
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(HttpPostedFileBase image)
        {

            string name = Request["username"];
            string pass = Request["pass"];

            var isControl = MemberRepo.FirstOrDefault(t => t.Username == name && t.Password == pass);
            if (isControl != null)
            {
                Session["OK"] = "OK";
                Session["MemberID"] = isControl.ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var isAdminControl = AdminRepo.FirstOrDefault(t => t.username == name && t.pass == pass);
                if (isAdminControl != null)
                {
                    Session["Admin"] = "OK";
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                }
                Session["Error"] = "Kullanıcı adı veya şifre hatalı";

                return View();
            }

        }

        public ActionResult KayitOl()
        {
            Member m = new Member();
            m.Password = Request["pass"];
            m.Username = Request["username"];
            m.Showname = Request["showname"];
            m.Status = 0;
            MemberRepo.Insert(m);
            _uow.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult RedaktorEkle()
        {
            Admin m = new Admin();
            m.pass = Request["pass"];
            m.username = Request["username"];

            AdminRepo.Insert(m);
            _uow.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult FacebookLogin()
        {

            string AppID = "3066183300078444";
            string AppSecret = "9e739d8e1f7e4d7246bf9ce29f5cee0e";
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = AppID,
                client_secret = AppSecret,
                redirect_uri = Url.Action("FacebookOk","Account"),
                response_type = "code",
                scope = "user_about_me, email" // Add other permissions as needed
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookOk()
        {
            return View();
        }

        public ActionResult MemberSuccess(string unique)
        {

            var isMember = MemberRepo.FirstOrDefault(t => t.UniqueKey == unique);
            if (isMember != null)
            {
                isMember.Status = 1;
                MemberRepo.Update(isMember);
                _uow.SaveChanges();

            }
            else
            {
                Session["Error"] = "Hatalı bir onaylama süreci";
                return RedirectToAction("Error", "Home");
            }
            return View();
        }


        public ActionResult AdminList()
        {
            List<Admin> adminList = AdminRepo.GetList().ToList();
            return View(adminList);
        }

        public ActionResult DeleteAdmin(int ID)
        {
            Admin a = AdminRepo.FirstOrDefault(t => t.ID == ID);
            AdminRepo.Delete(a.ID);
            _uow.SaveChanges();

            return RedirectToAction("AdminList");

        }

        public ActionResult AddAdmin()
        {

            return View();
        }

        public ActionResult InsertAdmin()
        {
            Admin a = new Admin();
            a.username = Request["Username"];
            a.pass = Request["Password"];
            AdminRepo.Insert(a);
            _uow.SaveChanges();
            return RedirectToAction("AdminList");
        }
    }
}