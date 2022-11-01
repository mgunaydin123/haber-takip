using DernekOtomasyon.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DernekOtomasyon.MVC.Models
{
    public class NewsModel
    {
        public List<Category> CategoryList { get; set; }
        public int TotalCount { get; set; }
        public List<News> News { get; set; }
        public List<Comment> CommentList { get; set; }
        public News NewsDetail { get; set; }
    }

    public class Response
    {
        public string status { get; set; }

    }
}