using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class CategoryClient : ClientBase<ICategoryService>, ICategoryService
    {
        public IEnumerable<Category> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public Category Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
    }
}
