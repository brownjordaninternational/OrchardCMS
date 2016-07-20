using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ChannelGroupClient : ClientBase<IChannelGroupService>, IChannelGroupService
    {
        public IEnumerable<ChannelGroup> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public ChannelGroup Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
		public ChannelGroup GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
    }
}
