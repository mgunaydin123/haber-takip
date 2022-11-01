using DernekOtomasyon.Business.DataServices;
using DernekOtomasyon.Business.UnitOfWork;
using DernekOtomasyon.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DernekOtomasyon.MVC.Controllers.Base
{
    public class BaseController : Controller
    {
        // GET: Base


        private ApplicationEntityDbContext _dbContext;

        public MemberRepository MemberRepo;
        public AdminRepository AdminRepo;
        public EventRepository EventRepo;
        public EventUserRepository EventUserRepo;
        public CommentRepository CommentRepo;
        public CategoryRepository CategoryRepo;
        public NewsRepository NewsRepo;



        public IUnitOfWork _uow;

        public BaseController()
        {

            ViewBag.Path = "/content";
            _dbContext = new ApplicationEntityDbContext();
            MemberRepo = new MemberRepository(_dbContext);
            AdminRepo = new AdminRepository(_dbContext);
            EventRepo = new EventRepository(_dbContext);
            EventUserRepo = new EventUserRepository(_dbContext);
            CommentRepo = new CommentRepository(_dbContext);
            CategoryRepo = new CategoryRepository(_dbContext);
            NewsRepo = new NewsRepository(_dbContext);


            _uow = new EFUnitOfWork(_dbContext);
        }
    }
}