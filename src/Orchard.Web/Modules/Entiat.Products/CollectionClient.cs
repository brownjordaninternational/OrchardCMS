using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class CollectionClient : ClientBase<ICollectionService>, ICollectionService
    {
        public IEnumerable<Collection> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public Collection Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
		public Collection GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
    }
}
