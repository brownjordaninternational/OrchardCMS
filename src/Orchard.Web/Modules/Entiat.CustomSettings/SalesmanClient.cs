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
    }
}
