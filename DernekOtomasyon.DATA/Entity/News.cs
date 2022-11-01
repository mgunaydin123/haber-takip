using DernekOtomasyon.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DernekOtomasyon.DATA.Entity
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Spot { get; set; }
        public string Content { get; set; }
        public int Statu { get; set; }
        public int MemberID { get; set; }
        public string Source { get; set; }
        public int CategoryID { get; set; }
        public virtual Member Member {get; set; }
        public virtual Category Category {get; set; }

    }
}
