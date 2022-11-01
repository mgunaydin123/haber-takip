using DernekOtomasyon.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DernekOtomasyon.DATA.Entity
{
    public class EventUser : BaseEntity
    {
        public int EventID { get; set; }
        public int MemberID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Member Member { get; set; }
    }
}
