using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class RepresentativeClient : ClientBase<IRepresentativeService>, IRepresentativeService
    {
        public IEnumerable<Representative> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public Representative Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
		public Representative GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
		public IEnumerable<Representative> GetByNum(int channel, int id)
        {
            return Channel.GetByNum(channel, id);
        }
		public bool UpdateRepresentative(Representative entity)
        {
            return Channel.UpdateRepresentative(entity);
        }
        public long AddRepresentative(Representative entity)
        {
            return Channel.AddRepresentative(entity);
        }
		public bool DeleteRepresentative(Representative entity)
        {
            return Channel.DeleteRepresentative(entity);
        }
    }
}
