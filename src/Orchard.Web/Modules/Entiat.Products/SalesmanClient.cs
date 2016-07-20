using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class SalesmanClient : ClientBase<ISalesmanService>, ISalesmanService
    {
        public IEnumerable<Salesman> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Salesman Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Salesman> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Salesman GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateSalesman(Salesman entity)
        {
            return Channel.UpdateSalesman(entity);
        }
        public long AddSalesman(Salesman entity)
        {
            return Channel.AddSalesman(entity);
        }
    }
}
