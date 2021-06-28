using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Menu
{
    public class SubMenusEntity
    {
        [Key]
        public int SubMenusId { get; set; }
        public string SubMenusDescription { get; set; }
        public bool IsEnabled { get; set; }
        public int MenusId { get; set; }
        public string Modal { get; set; }
        public string MenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public string Component { get; set; }
        public string SubMenusSize { get; set; }
    }
}
