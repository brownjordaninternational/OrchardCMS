using Bj.Essentials.Entities;
using Bjx.WCF.Contracts;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Bj.Essentials.Proxies
{
    public class ProductClient : ClientBase<IProductService>, IProductService
    {
        public IEnumerable<Product> GetAll(string company)
        {
            return Channel.GetAll(company);
        }
        public Product Get(string company, int id)
        {
            return Channel.Get(company, id);
        }
        public IEnumerable<Product> GetByNum(string company, int num)
        {
            return Channel.GetByNum(company, num);
        }
		public Product GetByCode(string company, string code) 
		{
			return Channel.GetByCode(company, code);
		}
		public bool UpdateProduct(Product entity)
        {
            return Channel.UpdateProduct(entity);
        }
        public long AddProduct(Product entity)
        {
            return Channel.AddProduct(entity);
        }
		public bool DeleteProduct(Product entity)
        {
            return Channel.DeleteProduct(entity);
        }
    }
}
