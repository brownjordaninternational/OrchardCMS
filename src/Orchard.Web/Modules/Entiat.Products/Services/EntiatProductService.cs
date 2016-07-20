using System;
using Orchard.Mvc;
using Orchard.Environment.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bj.Essentials.Entities;
using Bj.Essentials.Proxies;

namespace Entiat.Products.Services
{
    public class EntiatProductService : IEntiatProductService
    {
        public EntiatProductService() { }

        public IEnumerable<Collection> GetAllCollections(int channel)
        {
            IEnumerable<Collection> channels = new List<Collection>();

            using (CollectionClient proxy = new CollectionClient())
            {
                return proxy.GetAll(channel);
            }
        }
        public Collection GetCollection(int channel, int collectionId)
        {
            using(CollectionClient proxy = new CollectionClient())
            {
                return proxy.Get(channel, collectionId);
            }
        }
        public Category GetCategory(int channel, int categoryId)
        {
            using(CategoryClient proxy = new CategoryClient())
            {
                return proxy.Get(channel, categoryId);
            }
        }
        public IEnumerable<ChannelProduct> GetChannelProducts(int channel)
        {
            using(ChannelProductClient proxy = new ChannelProductClient())
            {
                return proxy.GetAll(channel);
            }
        }
        public IEnumerable<ChannelGroup> GetChannelGroup(int channel)
        {
            using(ChannelGroupClient proxy = new ChannelGroupClient())
            {
                return proxy.GetAll(channel);
            }
        }
        public IEnumerable<Product> GetProducts(string company)
        {
            using(ProductClient proxy = new ProductClient())
            {
                return proxy.GetAll(company);
            }
        }
        public IEnumerable<Image> GetProductImages(int channel,int productId)
        {
            using(ImageClient proxy = new ImageClient())
            {
                return proxy.GetByProduct(channel, productId);
            }
        }
        public IEnumerable<ProductDocument> GetProductDocuments(int channel, int productId)
        {
            using(ProductDocumentClient proxy = new ProductDocumentClient())
            {
                return proxy.GetByNum(channel, productId);
            }
        }
        public IEnumerable<ProductOption> LookupOptions(string company, int productId)
        {
            using(ProductOptionClient proxy = new ProductOptionClient())
            {
                return proxy.GetByProduct(company, productId);
            }
        }
        public IEnumerable<Sku> GetSkusInGroup(string company, int id)
        {
            using(SkuClient proxy = new SkuClient())
            {
                return proxy.GetByNum(company, id);
            }
        }
        public ChannelProduct GetChannelProduct(int channelProductId)
        {
            using(ChannelProductClient proxy = new ChannelProductClient())
            {
                return proxy.GetById(channelProductId);
            }
        }
        public Product GetProduct(string company, int productId)
        {
            using (ProductClient proxy = new ProductClient())
            {
                return proxy.Get(company, productId);
            }
        }
        public OptionGroup GetOptionGroup(string company, int id)
        {
            using(OptionGroupClient proxy = new OptionGroupClient())
            {
                return proxy.Get(company, id);
            }
        }
        public Image GetSwatchImage(int channel, int id)
        {
            using(ImageClient proxy = new ImageClient())
            {
                return proxy.Get(channel, id);
            }
        }
        public Image GetMainImage(int channel, int id)
        {
            using (ImageClient proxy = new ImageClient())
            {
                return proxy.Get(channel, id);
            }
        }
        public Sku GetSku(string company, int skuId)
        {
            using (SkuClient proxy = new SkuClient())
            {
                return proxy.Get(company, skuId);
            }
        }
        public IEnumerable<ProductCategory> GetProductsInCategory(int id)
        {
            using(ProductCategoryClient proxy = new ProductCategoryClient())
            {
                return proxy.GetByCategory(id);
            }
        }
        public IEnumerable<ProductCategory> GetProductCategories(int product)
        {
            using (ProductCategoryClient proxy = new ProductCategoryClient())
            {
                return proxy.GetByProduct(product);
            }
        }
        public IEnumerable<ChannelSku> GetSkusInChannel(int channel)
        {
            using (ChannelSkuClient proxy = new ChannelSkuClient())
            {
                return proxy.GetAll(channel);
            }
        }
        public IEnumerable<SkuCategory> GetSkuCategories(int sku)
        {
            using(SkuCategoryClient proxy = new SkuCategoryClient())
            {
                return proxy.GetBySku(sku);
            }
        }
    }
}