using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class CustomerClient : ClientBase<ICustomerService>, ICustomerService
    {
        public IEnumerable<Customer> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Customer Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Customer> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Customer GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateCustomer(Customer entity)
        {
            return Channel.UpdateCustomer(entity);
        }
        public long AddCustomer(Customer entity)
        {
            return Channel.AddCustomer(entity);
        }
    }
}
