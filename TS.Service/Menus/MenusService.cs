using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TS.Domain.Dtos.Menus;
using TS.Domain.Helper;
using TS.Infra.Repositories.Menu;
using TS.Service.Base;
using TS_Security;

namespace TS.Service.Menus
{
    public class MenusService : IService
    {
        private readonly CacheHelper cacheHelper;
        private readonly MenusRepository repository;
        private readonly AuthenticationTokenProvider authenticationToken;
        public MenusService(CacheHelper cacheHelper, MenusRepository repository, AuthenticationTokenProvider authenticationToken)
        {
            this.cacheHelper = cacheHelper;
            this.repository = repository;
            this.authenticationToken = authenticationToken;
        }

        public async Task<IEnumerable<MenusDto>> GetMenus(int menuGroup, string token)
        {
            var userid = authenticationToken.DecriptToken(token);

            var cacheKey = $"cache-menus-{menuGroup}-{userid}";

            var cacheItem = cacheHelper.Get(cacheKey);

            if (string.IsNullOrEmpty(cacheItem) == true)
            {
                var list = await repository.GetMenus(menuGroup, Convert.ToInt32(userid));

                var selectedList = list.Select(MenusDto.From);

                var cacheItemSerialized = JsonConvert.SerializeObject(selectedList);

                cacheHelper.Set(cacheKey, cacheItemSerialized, TimeSpan.FromMinutes(10));

                return selectedList;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<MenusDto>>(cacheItem);
            }
        }

        public async Task<IEnumerable<SubMenusDto>> GetSubMenus(int menuId)
        {
            var cacheKey = $"cache-submenus-{menuId}";

            var cacheItem = cacheHelper.Get(cacheKey);

            if (string.IsNullOrEmpty(cacheItem) == true)
            {
                var list = await repository.GetSubMenus(menuId);

                var selectedList = list.Select(SubMenusDto.From);

                var cacheItemSerialized = JsonConvert.SerializeObject(selectedList);

                cacheHelper.Set(cacheKey, cacheItemSerialized);

                return selectedList;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<SubMenusDto>>(cacheItem);
            }
        }
    }
}
