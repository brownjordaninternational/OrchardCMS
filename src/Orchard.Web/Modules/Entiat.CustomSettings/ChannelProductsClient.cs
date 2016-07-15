using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ChannelProductsClient : ClientBase<IChannelProductsService>, IChannelProductsService
    {
        public IEnumerable<ChannelProducts> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public ChannelProducts Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
    }
}
