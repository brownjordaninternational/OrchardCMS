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
		public Category GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
		public IEnumerable<Category> GetByNum(int channel, int id)
        {
            return Channel.GetByNum(channel, id);
        }
		public bool UpdateCategory(Category entity)
        {
            return Channel.UpdateCategory(entity);
        }
        public long AddCategory(Category entity)
        {
            return Channel.AddCategory(entity);
        }
		public bool DeleteCategory(Category entity)
        {
            return Channel.DeleteCategory(entity);
        }
    }
}
