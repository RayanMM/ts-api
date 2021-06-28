using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities.Menu;

namespace TS.Domain.Dtos.Menus
{
    public class SubMenusDto
    {
        public int SubMenusId { get; set; }
        public string SubMenusDescription { get; set; }
        public bool IsEnabled { get; set; }
        public int MenusId { get; set; }
        public string Modal { get; set; }
        public string MenuIcon { get; set; }
        public int SubMenuOrder { get; set; }
        public string Component { get; set; }
        public string SubMenusSize { get; set; }

        public static SubMenusDto From(SubMenusEntity from)
        {
            return new SubMenusDto
            {
                SubMenusId = from.SubMenusId,
                SubMenusDescription = from.SubMenusDescription,
                IsEnabled = from.IsEnabled,
                MenusId = from.MenusId,
                Modal = from.Modal,
                MenuIcon = from.MenuIcon,
                SubMenuOrder = from.SubMenuOrder,
                Component = from.Component,
                SubMenusSize = from.SubMenusSize
            };
        }
    }
}
