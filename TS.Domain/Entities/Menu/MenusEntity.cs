using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TS.Domain.Entities.Menu
{
    public class MenusEntity
    {
        [Key]
        public int MenusId { get; set; }
        public string MenusDescription { get; set; }
        public bool IsEnabled { get; set; }
        public string Modal { get; set; }
        public string MenuIcon { get; set; }
        public string ModalSize { get; set; }
        public int MenuOrder { get; set; }
    }
}
