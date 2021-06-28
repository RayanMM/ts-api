using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.Domain.Entities.Menu;
using TS.Infra.Base;
using TS.Infra.Context;

namespace TS.Infra.Repositories.Menu
{
    public class MenusRepository : IRepository
    {
        private readonly DbContext context;

        public MenusRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MenusEntity>> GetMenus(int menuGroup, int userId)
        {
            return await context.Connection.QueryAsync<MenusEntity>(@"
                    SELECT M.MenusId, 
                            MenusDescription, 
                            M.IsEnabled,
                            MenuIcon, 
                            Modal, 
                            ModalSize, 
                            MenuOrder
                            FROM Menus M JOIN menusUser MU on M.MenusId = MU.MenusId JOIN [User] U on U.UserId = MU.UserId 
                            WHERE M.MenuGroupId = @menuGroup and mu.userId = @userId and M.IsEnabled = 1
                            ORDER BY MenuOrder
            ", new { menuGroup, userId});
        }

        public async Task<IEnumerable<SubMenusEntity>> GetSubMenus(int menuId)
        {
            return await context.Connection.QueryAsync<SubMenusEntity>(@"
                select 
                    MenusId, 
                    SubMenusid, 
                    subMenusDescription, 
                    Modal, 
                    Menuicon, 
                    subMenuOrder, 
                    Component, 
                    SubMenusSize 
                from SubMenus
                where MenusId = @menuId AND IsEnabled = 1
                ORDER BY subMenuOrder
                ", new { menuId });
        }
    }
}
