using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class PriceListClient : ClientBase<IPriceListService>, IPriceListService
    {
        public IEnumerable<PriceList> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public PriceList Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<PriceList> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public PriceList GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdatePriceList(PriceList entity)
        {
            return Channel.UpdatePriceList(entity);
        }
        public long AddPriceList(PriceList entity)
        {
            return Channel.AddPriceList(entity);
        }
    }
}
