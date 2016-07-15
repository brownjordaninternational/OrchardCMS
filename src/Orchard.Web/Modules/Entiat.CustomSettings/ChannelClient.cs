using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ChannelClient : ClientBase<IChannelService>, IChannelService
    {
        public IEnumerable<Channel> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public Channel Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
    }
}
