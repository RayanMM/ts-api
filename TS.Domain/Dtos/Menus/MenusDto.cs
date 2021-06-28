using TS.Domain.Entities.Menu;

namespace TS.Domain.Dtos.Menus
{
    public class MenusDto
    {
        public int MenusId { get; set; }
        public string MenusDescription { get; set; }
        public string Modal { get; set; }
        public string MenuIcon { get; set; }
        public string ModalSize { get; set; }
        public int MenuOrder { get; set; }

        public static MenusDto From(MenusEntity from)
        {
            return new MenusDto
            {
                MenusId = from.MenusId,
                MenusDescription = from.MenusDescription,
                Modal = from.Modal,
                MenuIcon = from.MenuIcon,
                ModalSize = from.ModalSize,
                MenuOrder = from.MenuOrder
            };
        }
    }
}
