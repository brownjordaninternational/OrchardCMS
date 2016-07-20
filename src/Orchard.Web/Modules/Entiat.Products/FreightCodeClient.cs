using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class FreightCodeClient : ClientBase<IFreightCodeService>, IFreightCodeService
    {
        public IEnumerable<FreightCode> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public FreightCode Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<FreightCode> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public FreightCode GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateFreightCode(FreightCode entity)
        {
            return Channel.UpdateFreightCode(entity);
        }
        public long AddFreightCode(FreightCode entity)
        {
            return Channel.AddFreightCode(entity);
        }
    }
}
