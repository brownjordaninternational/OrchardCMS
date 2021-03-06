using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ImageClient : ClientBase<IImageService>, IImageService
    {
        public IEnumerable<Image> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public Image Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
		public Image GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
		public bool UpdateImage(Image entity)
        {
            return Channel.UpdateImage(entity);
        }
        public long AddImage(Image entity)
        {
            return Channel.AddImage(entity);
        }
    }
}
