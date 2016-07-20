using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ChannelClient : ClientBase<IChannelService>, IChannelService
    {
        public IEnumerable<Channel> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Channel Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Channel> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Channel GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateChannel(Channel entity)
        {
            return Channel.UpdateChannel(entity);
        }
        public long AddChannel(Channel entity)
        {
            return Channel.AddChannel(entity);
        }
		public bool DeleteChannel(Channel entity)
        {
            return Channel.DeleteChannel(entity);
        }
    }
}
