using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class SkuClient : ClientBase<ISkuService>, ISkuService
    {
        public IEnumerable<Sku> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Sku Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Sku> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Sku GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateSku(Sku entity)
        {
            return Channel.UpdateSku(entity);
        }
        public long AddSku(Sku entity)
        {
            return Channel.AddSku(entity);
        }
		public bool DeleteSku(Sku entity)
        {
            return Channel.DeleteSku(entity);
        }
    }
}
