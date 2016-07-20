using Bj.Essentials.Entities;
using Bj.Essentials.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entiat.ProductsMenuItem.Services
{
    public class ProductsMenuItemService : IProductsMenuItemService
    {
        public IEnumerable<Category> GetAllCategories(int ChannelId)
        {
            using (CategoryClient proxy = new CategoryClient())
            {
                return proxy.GetAll(ChannelId);
            }
        }
    }
}