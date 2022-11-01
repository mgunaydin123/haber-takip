using DernekOtomasyon.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DernekOtomasyon.DATA.Entity
{
    public class Comment : BaseEntity
    {
        public int NewsID { get; set; }
        public string Name { get; set; }
        public string CommentText { get; set; }

        public virtual News News { get; set; }
    }
}
