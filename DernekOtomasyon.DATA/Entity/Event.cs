using DernekOtomasyon.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DernekOtomasyon.DATA.Entity
{
    public class Event: BaseEntity
    {
         public string name { get; set; }
         public DateTime? startDate { get; set; }
         public DateTime? endDate { get; set; }
         public int limit { get; set; }
         public string desc { get; set; }
    }
}
