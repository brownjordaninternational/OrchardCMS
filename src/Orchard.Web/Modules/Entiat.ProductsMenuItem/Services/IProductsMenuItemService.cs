using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bj.Essentials.Entities;
using Orchard;

namespace Entiat.ProductsMenuItem.Services
{
    public interface IProductsMenuItemService : IDependency
    {
        IEnumerable<Category> GetAllCategories(int ChannelId);
    }
}