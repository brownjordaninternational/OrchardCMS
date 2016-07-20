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
		public ShipVia GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateShipVia(ShipVia entity)
        {
            return Channel.UpdateShipVia(entity);
        }
        public long AddShipVia(ShipVia entity)
        {
            return Channel.AddShipVia(entity);
        }
		public bool DeleteShipVia(ShipVia entity)
        {
            return Channel.DeleteShipVia(entity);
        }
    }
}
