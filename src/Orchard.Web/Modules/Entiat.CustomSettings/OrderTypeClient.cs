using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class OrderTypeClient : ClientBase<IOrderTypeService>, IOrderTypeService
    {
        public IEnumerable<OrderType> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public OrderType Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<OrderType> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
