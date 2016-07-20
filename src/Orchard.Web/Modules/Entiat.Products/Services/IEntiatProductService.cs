using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bj.Essentials.Entities;
using Orchard;

namespace Entiat.Products.Services
{
    public interface IEntiatProductService :IDependency
    {
        IEnumerable<Collection> GetAllCollections(int channel);
        Collection GetCollection(int channel, int collectionId);
        Category GetCategory(int channel, int categoryId);
        IEnumerable<ChannelProduct> GetChannelProducts(int channel);
        IEnumerable<ChannelGroup> GetChannelGroup(int channel);
        IEnumerable<Product> GetProducts(string company);
        IEnumerable<Image> GetProductImages(int channel,int productId);
        IEnumerable<ProductDocument> GetProductDocuments(int channel, int productId);
        IEnumerable<ProductOption> LookupOptions(string company, int productId);
        IEnumerable<Sku> GetSkusInGroup(string company, int id);
        ChannelProduct GetChannelProduct(int channelproductId);
        Product GetProduct(string company, int productId);
        OptionGroup GetOptionGroup(string company, int id);
        Image GetSwatchImage(int channel, int id);
        Image GetMainImage(int channel, int id);
        Sku GetSku(string Company, int skuId);
        IEnumerable<ProductCategory> GetProductsInCategory(int id);
        IEnumerable<ProductCategory> GetProductCategories(int product);
        IEnumerable<ChannelSku> GetSkusInChannel(int channel);
        IEnumerable<SkuCategory> GetSkuCategories(int sku);
    }
}