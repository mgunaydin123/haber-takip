using DernekOtomasyon.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DernekOtomasyon.DATA.Entity
{
    public class Admin : BaseEntity
    {
        //Redaktör
        public string username { get; set; }
        public string pass { get; set; }
    }
}
