using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ShipToClient : ClientBase<IShipToService>, IShipToService
    {
        public IEnumerable<ShipTo> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public ShipTo Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<ShipTo> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
    }
}
