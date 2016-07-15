using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ShipViaClient : ClientBase<IShipViaService>, IShipViaService
    {
        public IEnumerable<ShipVia> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public ShipVia Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<ShipVia> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
