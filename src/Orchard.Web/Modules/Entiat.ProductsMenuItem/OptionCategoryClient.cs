using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class OptionCategoryClient : ClientBase<IOptionCategoryService>, IOptionCategoryService
    {
        public IEnumerable<OptionCategory> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public OptionCategory Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<OptionCategory> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public OptionCategory GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateOptionCategory(OptionCategory entity)
        {
            return Channel.UpdateOptionCategory(entity);
        }
        public long AddOptionCategory(OptionCategory entity)
        {
            return Channel.AddOptionCategory(entity);
        }
    }
}
