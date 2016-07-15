using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ProductDocumentClient : ClientBase<IProductDocumentService>, IProductDocumentService
    {
        public IEnumerable<ProductDocument> GetAll(int channel)
        {
            return Channel.GetAll(channel);
        }
        public ProductDocument Get(int channel, int id)
        {
            return Channel.Get(channel, id);
        }
		public ProductDocument GetByCode(int channel, string code)
		{
			return Channel.GetByCode(channel, code);
		}
		public IEnumerable<ProductDocument> GetByNum(int channel, int id)
        {
            return Channel.GetByNum(channel, id);
        }
		public bool UpdateProductDocument(ProductDocument entity)
        {
            return Channel.UpdateProductDocument(entity);
        }
        public long AddProductDocument(ProductDocument entity)
        {
            return Channel.AddProductDocument(entity);
        }
		public bool DeleteProductDocument(ProductDocument entity)
        {
            return Channel.DeleteProductDocument(entity);
        }
    }
}
