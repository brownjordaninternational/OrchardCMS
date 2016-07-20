using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class DiscountCodeClient : ClientBase<IDiscountCodeService>, IDiscountCodeService
    {
        public IEnumerable<DiscountCode> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public DiscountCode Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<DiscountCode> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public DiscountCode GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateDiscountCode(DiscountCode entity)
        {
            return Channel.UpdateDiscountCode(entity);
        }
        public long AddDiscountCode(DiscountCode entity)
        {
            return Channel.AddDiscountCode(entity);
        }
    }
}
