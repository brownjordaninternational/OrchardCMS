using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Mvc;
using Entiat.CustomSettings.Models;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Entiat.Products.Services;
using Orchard;
using Bj.Essentials.Entities;
using Bj.Essentials.Proxies;
using Entiat.Products.Models;
using Orchard.Settings;
using Entiat.Products.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.Web;

namespace Entiat.Products.Controllers
{
    [Themed]
    public class ProductDetailController : Controller
    {

        private readonly IEntiatProductService _service;
        private readonly IOrchardServices _oservice;
        private readonly dynamic _factory;
        private readonly EntiatSiteSettingsPart _settings;
        private readonly IWorkContextAccessor _workContextAccessor;
        private IEnumerable<ChannelProduct> _ChannelProduct = Enumerable.Empty<ChannelProduct>();
        private Collection _collection = new Collection();
        private Category _category = new Category();
        private Product _product = new Product();
        private List<Image> _altImages = new List<Image>();
        private List<Image> _collectionImages = new List<Image>();
        private List<ProductDocument> _documents = new List<ProductDocument>();
        private List<OptionSection> _productOptions = new List<OptionSection>();
        private readonly ISiteService _siteService;
        CloudStorageAccount _account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["BlobStorage"]);

        public ProductDetailController(IShapeFactory factory, IEntiatProductService service,
            IOrchardServices oservice, IWorkContextAccessor workcontext, ISiteService siteService)
        {
            _service = service;
            _factory = factory;
            _oservice = oservice;
            _siteService = siteService;
            _workContextAccessor = workcontext;
            _settings = _oservice.WorkContext.CurrentSite.As<EntiatSiteSettingsPart>();

        }

        private void GetCollection(int collectionId)
        {
            _collection = _service.GetCollection(_settings.ChannelId, collectionId);
        }
        //Product Detail Page
        public ActionResult GetProductDetail(int channelproductid)
        {
            //get channelproduct and product info
            ChannelProduct cp = _service.GetChannelProduct(channelproductid);
            if (cp.Product != null)
            {
                _product = _service.GetProduct(_settings.CompanyCode, Convert.ToInt32(cp.Product));
            }


            if (cp.Collection != null)
            {
                GetCollection(Convert.ToInt32(cp.Collection));
            }

            if(!_settings.EnableAmplience)
            {
                if (_collection.Id > 0)
                {
                    _collectionImages = GetImagesFromBlob(_collection.Id);
                }
                if( cp.Product != null)
                {
                    _altImages = _service.GetProductImages(_settings.ChannelId, Convert.ToInt32(cp.Product)).Where(x => x.ParentFolder == "alt").ToList();
                }
            }
            if (cp.Product != null)
            {
                _documents = _service.GetProductDocuments(_settings.ChannelId, Convert.ToInt32(cp.Product)).ToList();
            }

            ProductDetail detail = new ProductDetail();
            if (cp.ImageId != null && cp.ImageId > 0)
            {
                detail.ImageUrl = _service.GetMainImage(_settings.ChannelId, Convert.ToInt32(cp.ImageId)).Url;
            }
            detail.cp = cp;

            //get options that belong to a product
            IEnumerable<ProductOption> options = GetOptions(cp);
            IEnumerable<ProductCategory> categories = Enumerable.Empty<ProductCategory>();
            List<SkuCategory> skucategories = new List<SkuCategory>();
            //filter options to whats available in a channel
            if (options.Count() > 0)
            {
                List<ChannelSku> skusInChannel = GetSkusInChannel();
                if(skusInChannel.Count() > 0)
                {
                    options = options.Where(x => skusInChannel.Select(y => y.Sku).ToList().Contains(x.Sku)).ToList();
                }
                if (options.Count() > 0)
                {
                    //get categories product belongs to
                    categories = GetProductCategories(cp);
                    //get categories sku belongs to
                    skucategories = GetSkuCategories(options).ToList();
                    //remove from sku categories, categories that do not belong to a product.
                    List<int> skuCatList = skucategories.Select(x => x.Category).Distinct().ToList();
                    foreach(int c in skuCatList)
                    {
                        if(!categories.Select(x=>x.Category).ToList().Contains(c))
                        {
                            skucategories.RemoveAll(x => x.Category == c);
                        }
                    }
                    //group skus

                    foreach(int cat in skucategories.Select(x=>x.Category).ToList())
                    {
                        OptionSection os = new OptionSection();
                        os.Category = _service.GetCategory(_settings.ChannelId, cat);
                        foreach(int s in skucategories.Where(x=>x.Category == cat).Select(x=>x.Sku).ToList())
                        {
                            Sku sku = _service.GetSku(_settings.CompanyCode, s);
                            os.Skus.Add(sku);
                        }
                        _productOptions.Add(os);
                    }

                }
            }

            dynamic shape = null;
            if (_settings.IsMobile)
            {
                shape = _factory.Parts_ProductDetailBootstrap(Product: _product, ProductDetail: detail,Options:_productOptions, AltImages: _altImages, CollectionImages: _collectionImages, CustomSettings: _settings, Collection: _collection, Documents: _documents);
            }
            else
            {
                shape = _factory.Parts_ProductDetail(Product: _product, ProductDetail: detail, Options: _productOptions, AltImages: _altImages, CollectionImages: _collectionImages, CustomSettings: _settings, Collection: _collection, Documents: _documents);
            }

            return new ShapeResult(this, shape);
        }
        public ActionResult GetFullSizeSwatchImage(int swatchid)
        {
            Sku selectedSku = _service.GetSku(_settings.CompanyCode, swatchid);
            var shape = _factory.Parts_FullSizeImage(Sku: selectedSku, CustomSettings: _settings);
            return new ShapeResult(this, shape);
        }

        /// <summary>
        /// Gets available options for a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private IEnumerable<ProductOption> GetOptions(ChannelProduct product)
        {
            IEnumerable<ProductOption> optionList = Enumerable.Empty<ProductOption>();
            optionList = _service.LookupOptions(_settings.CompanyCode, Convert.ToInt32(product.Product));
            return optionList;
        }
        private IEnumerable<ProductCategory> GetProductCategories(ChannelProduct product)
        {
            return _service.GetProductCategories(product.ID);
        }
        private List<ChannelSku> GetSkusInChannel()
        {
            return _service.GetSkusInChannel(_settings.ChannelId).ToList();
        }
        private IEnumerable<SkuCategory> GetSkuCategories(IEnumerable<ProductOption> options)
        {
            List<SkuCategory> categories = new List<SkuCategory>();
            foreach(ProductOption po in options)
            {
                List<SkuCategory> skucategories = _service.GetSkuCategories(po.Sku).ToList();
                categories.Concat(skucategories);
            }
            return categories;
        }
        /// <summary>
        /// gets option skus for a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private List<Sku> GetSkus(OptionGroup group)
        {
            IEnumerable<Sku> skus = Enumerable.Empty<Sku>();
            skus = _service.GetSkusInGroup(_settings.CompanyCode, group.Id);
            return skus.ToList();
        }

        private List<Image> GetImagesFromBlob(int collection)
        {
            List<Image> CollectionImages = new List<Image>();
            List<string> ImageUrlList = new List<string>();
            CloudBlobClient blobClient = _account.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["BlobContainer"]);
            var blobs = container.ListBlobs("channel/" + _settings.ChannelId + "/collection/" + collection + "/MED/");
            foreach (var blob in blobs.OfType<CloudBlockBlob>().Select(x => x.Name).ToList())
            {
                Image img = new Image();
                img.Channel = _settings.ChannelId;
                img.Url = blob;
                CollectionImages.Add(img);

            }
            return CollectionImages;
        }

        public ActionResult AddPoolsideProduct(int channelproductid)
        {
            HttpCookie poolsideCookie = new HttpCookie("MyPoolside");
            if (Request.Cookies["MyPoolside"] != null)
            {
                poolsideCookie = Request.Cookies["MyPoolside"];
            }
            //add the id to the cookie section for collection or products if it does not already exist
            if (poolsideCookie.Values["Products"] != null && !poolsideCookie.Values["Products"].Contains(channelproductid.ToString()))
            {
                if (poolsideCookie.Values["Products"].Length < 1)
                {
                    poolsideCookie.Values["Products"] += channelproductid;
                }
                else
                {
                    poolsideCookie.Values["Products"] += "," + channelproductid;
                }
            }
            else if(poolsideCookie.Values["Products"] == null)
            {
                poolsideCookie.Values["Products"] += channelproductid;
            }
            poolsideCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(poolsideCookie);
            //return the poolside view with the cookie info
            return RedirectToAction("ViewMyPoolside", "ProductDetail");
        }
        public ActionResult AddPoolsideCollection(int channelproductid)
        {
            HttpCookie poolsideCookie = new HttpCookie("MyPoolside");
            if (Request.Cookies["MyPoolside"] != null)
            {
                poolsideCookie = Request.Cookies["MyPoolside"];
            }
            //add the id to the cookie section for collection or products

            if (!poolsideCookie.Values["Collections"].Contains(channelproductid.ToString()))
            {
                if (poolsideCookie.Values["Collections"].Length < 1)
                {
                    poolsideCookie.Values["Collections"] += channelproductid;
                }
                else
                {
                    poolsideCookie.Values["Collections"] += "," + channelproductid;
                }
            }
            poolsideCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(poolsideCookie);
            //return the poolside view with the cookie info
            return RedirectToAction("ViewMyPoolside", "ProductDetail");
        }
        public ActionResult RemoveMyPoolsideProduct(int channelproductid)
        {
            HttpCookie poolsideCookie = new HttpCookie("MyPoolside");
            if (Request.Cookies["MyPoolside"] != null)
            {
                poolsideCookie = Request.Cookies["MyPoolside"];
            }
            string[] ProductIds = poolsideCookie.Values["Products"].Split(',');
            List<string> newProductsCookie = new List<string>();
            foreach (var product in ProductIds)
            {
                //call service to get collection data by id
                int value = 0;
                int.TryParse(product, out value);
                if (value != 0 && value == channelproductid)
                {
                    //do nothing -- skip
                    // TODO ' HACK Update this procedure - add failure message or javascript warning that it is already in the wishlist
                }
                else
                {
                    newProductsCookie.Add(value.ToString());
                }

            }
            string delimiter = ",";
            string products = newProductsCookie.ToString();
            if (newProductsCookie.Count > 1)
            {
                products = newProductsCookie.Aggregate((i, j) => i + delimiter + j);
            }
            poolsideCookie.Values["Products"] = products;
            // TODO Update this cookie expiration to be an app setting?
            poolsideCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(poolsideCookie);
            return RedirectToAction("ViewMyPoolside", "ProductDetail");
        }
        public ActionResult RemoveMyPoolsideCollection(int channelproductid)
        {
            HttpCookie poolsideCookie = new HttpCookie("MyPoolside");
            if (Request.Cookies["MyPoolside"] != null)
            {
                poolsideCookie = Request.Cookies["MyPoolside"];
            }
            string[] CollectionIds = poolsideCookie.Values["Collections"].Split(',');
            List<string> newCollectionCookie = new List<string>();
            foreach (var collection in CollectionIds)
            {
                //call service to get collection data by id
                int value = 0;
                int.TryParse(collection, out value);
                if (value != 0 && value == channelproductid)
                {
                    //do nothing -- skip
                    // TODO ' HACK Update this procedure - add failure message or javascript warning that it is already in the wishlist
                }
                else
                {
                    newCollectionCookie.Add(value.ToString());
                }

            }
            string delimiter = ",";
            string collections = newCollectionCookie.Aggregate((i, j) => i + delimiter + j);
            poolsideCookie.Values["Collections"] = collections;
            // TODO Update this cookie expiration to be an app setting?
            poolsideCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(poolsideCookie);
            return RedirectToAction("ViewMyPoolside", "ProductDetail");
        }
        public ActionResult ViewMyPoolside(PoolsideCookieViewModel cookie)
        {
            var collectionService = new CollectionClient();
            var productService = new ProductClient();
            var ChannelProductervice = new ChannelProductClient();
            PoolsideViewModel poolsideItems = new PoolsideViewModel();
            HttpCookie poolsideCookie = new HttpCookie("MyPoolside");
            if (Request.Cookies["MyPoolside"] != null)
            {

                poolsideCookie = Request.Cookies["MyPoolside"];
                if (poolsideCookie.Values["Collections"] != null && poolsideCookie.Values["Collections"] != "")
                {
                    string[] CollectionIds = poolsideCookie.Values["Collections"].Split(',');
                    foreach (var id in CollectionIds)
                    {
                        //call service to get collection data by id
                        int value = 0;
                        int.TryParse(id, out value);
                        if (value != 0)
                        {
                            var collection = collectionService.Get(_settings.ChannelId, value);
                            if (collection != null)
                            {
                                poolsideItems.PoolsideCollections.Add(collection);
                            }
                        }
                    }
                }
                if (poolsideCookie.Values["Products"] != null && poolsideCookie.Values["Products"] != "")
                {
                    string[] ProductIds = poolsideCookie.Values["Products"].Split(',');
                    foreach (var id in ProductIds)
                    {
                        var poolsideProduct = new Products.ViewModels.PoolsideProduct();

                        //call service to get collection data by id
                        int value = 0;
                        int.TryParse(id, out value);
                        if (value != 0)
                        {
                            var channelProduct = ChannelProductervice.GetById(value);
                            {
                                if (channelProduct != null)
                                {
                                    if (channelProduct.Product != null)
                                    {
                                        var product = productService.Get(_settings.CompanyCode, channelProduct.Product.Value);
                                        if (product != null)
                                        {
                                            poolsideProduct.product = product;
                                            poolsideProduct.channelProduct = channelProduct;
                                            IEnumerable<Image> images = _service.GetProductImages(_settings.ChannelId, product.Id);

                                            if (channelProduct.ImageId != null && channelProduct.ImageId > 0)
                                            {
                                                poolsideProduct.ImageUrl = _service.GetMainImage(_settings.ChannelId, Convert.ToInt32(channelProduct.ImageId)).Url;
                                            }
                                            else
                                            {
                                                poolsideProduct.ImageUrl = images.Where(x => x.Product == channelProduct.Product && x.ParentFolder == "main").Select(x => x.Url).FirstOrDefault();
                                            }
                                            poolsideItems.PoolsideProducts.Add(poolsideProduct);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // return view with full product/collection set

            var shape = _factory.Parts_WishList(poolsideVM: poolsideItems, CustomSettings: _settings);


            return new ShapeResult(this, shape);
        }


    }
}