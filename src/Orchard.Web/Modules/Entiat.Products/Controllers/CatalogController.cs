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
using Entiat.Products.Models;
using Newtonsoft.Json;
using Orchard.UI.Navigation;
using Orchard.Settings;
using Entiat.Products.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.Xml.Linq;
using Orchard.Logging;

namespace Entiat.Products.Controllers
{
    [Themed]
    public class CatalogController : Controller
    {
        private readonly IEntiatProductService _service;
        private readonly IOrchardServices _oservice;
        private readonly dynamic _factory;
        private readonly EntiatSiteSettingsPart _settings;
        private readonly IWorkContextAccessor _workContextAccessor;
        private IEnumerable<ChannelProduct> _channelProducts = Enumerable.Empty<ChannelProduct>();
        private Collection _collection = new Collection();
        private Category _category = new Category();
        private List<Image> _productImages = new List<Image>();
        private readonly ISiteService _siteService;
        private CatalogViewModel _collectionModel = new CatalogViewModel();
        private CatalogViewModel _categoryModel = new CatalogViewModel();

        public ILogger Logger {get;set;}
        public CatalogController(IShapeFactory factory, IEntiatProductService service, 
            IOrchardServices oservice, IWorkContextAccessor workcontext, ISiteService siteService)
        {
            _service = service;
            _factory = factory;
            _oservice = oservice;
            _siteService = siteService;
            _workContextAccessor = workcontext;
            _settings = _oservice.WorkContext.CurrentSite.As<EntiatSiteSettingsPart>();
            Logger = NullLogger.Instance;
            Logger.Log(Orchard.Logging.LogLevel.Information, null, "catalog controller reached");
        }

        /// <summary>
        /// passes a collection id and returns the collection data
        /// </summary>
        /// <param name="id"></param>
        private void GetCollection(int id)
        {
            _collection = _service.GetCollection(_settings.ChannelId, id);
        }
        /// <summary>
        /// gets category information by id
        /// </summary>
        /// <param name="id"></param>
        private void GetCategory(int id)
        {
            _category = _service.GetCategory(_settings.ChannelId, id);
        }

        private IEnumerable<ProductCategory> GetProductsInCategory(int id)
        {
            return _service.GetProductsInCategory(id);
        }
        /// <summary>
        /// gets channel products list by 
        /// </summary>
        private void GetChannelProducts()
        {
            _channelProducts = _service.GetChannelProducts(_settings.ChannelId);
        }
        /// <summary>
        /// sets order of products based on setting from custom settings modules
        /// </summary>
        private void SetProductOrder()
        {
            switch (_settings.ProductOrder)
            {
                case "NAME":
                    _channelProducts.OrderBy(x => x.Name);
                    break;
                case "PARTNUMBER":
                    _channelProducts.OrderBy(x => x.PartNumber);
                    break;
                case "SEQUENCE":
                    _channelProducts.OrderBy(x => x.Sequence);
                    break;
            }
        }

        //gets main product images
        private void GetMainProductImages()
        {
            foreach (ChannelProduct p in _channelProducts)
            {
                Image image = new Image();
                if (p.ImageId != null)
                {
                    image = _service.GetMainImage(_settings.ChannelId, Convert.ToInt32(p.ImageId));
                    _productImages.Add(image);
                }
                else if (p.Product != null)
                {
                    image = _service.GetProductImages(_settings.ChannelId, Convert.ToInt32(p.Product)).Where(x => x.ParentFolder == "main").FirstOrDefault();
                    _productImages.Add(image);
                }
            }

        }

        //Catalog Page By Collection
        [HttpGet]
        public ActionResult GetProductsInCollection(int collectionid, PagerParameters pagerParameters)
        {
            //Get collection data
            GetCollection(collectionid);

            //GetChannelProducts
            GetChannelProducts();
            if(_channelProducts.Count() > 0)
            {
                //filter set to just products in collection
                _channelProducts = _channelProducts.Where(x => x.Collection == collectionid).ToList();
                if (_channelProducts.Count() > 0)
                {
                    //set product order
                    SetProductOrder();
                    //get product images if images are no in amplience
                    if (!_settings.EnableAmplience)
                    {
                        GetMainProductImages();
                    }
                }
            }

            PopulateCollectionModel(collectionid);

            //store collection id in model
            _collectionModel.Id = collectionid;

            //currently only available for non bootstrap sites
            #region top content feature section
            if (_collection.FeatureEnabled && _settings.IsMobile == false)
            {
                var topshape = _factory.Parts_FeatureCollection(Collection: _collection, CustomSettings: _settings);
                _workContextAccessor.GetContext().Layout.Zones["TopContent"].Add(topshape);
            }
            #endregion

            #region Pager
            if (_settings.ShowPager)
            {
                
                Pager pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
                dynamic pagerShape = _factory.Pager(pager).TotalItemCount(_collectionModel.TotalNumberOfProducts);
                _collectionModel.Pager = pagerShape;
                _collectionModel.StartPosition = pager.GetStartIndex(pager.Page) + 1;
                _collectionModel.EndPosition = pager.Page * pager.PageSize;
                if (_collectionModel.EndPosition > _collectionModel.TotalNumberOfProducts)
                {
                    _collectionModel.EndPosition = _collectionModel.TotalNumberOfProducts;
                }
                _collectionModel.ProductDetails = _collectionModel.ProductDetails.Skip(pager.GetStartIndex(pager.Page)).Take(pager.PageSize).ToList();
            }
            #endregion


            dynamic shape = null;
            
            if (_settings.IsMobile)
            {
                shape = _factory.Parts_ProductCatalogBootstrap(CustomSettings: _settings, CatalogViewModel: _collectionModel);
            }
            else
            {
                shape = _factory.Parts_ProductCatalog(CustomSettings: _settings, CatalogViewModel: _collectionModel);
            }

            return new ShapeResult(this, shape);
        }

        // Catalog Page By Category
        [HttpGet]
        public ActionResult GetProductsInCategory(int categoryId, PagerParameters pagerParameters)
        {

            //Get collection data
            GetCategory(categoryId);

            //GetChannelProducts
            GetChannelProducts();
            if (_channelProducts.Count() > 0)
            {
                //filter set to just products in category
                IEnumerable<ProductCategory> productsInCategory = GetProductsInCategory(categoryId);
                if (productsInCategory.Count() > 0) {
                    List<int> channelProductIds = productsInCategory.Select(x => x.ChannelProduct).ToList();
                    _channelProducts = _channelProducts.Where(x => channelProductIds.Contains(x.ID)).ToList();
                    if (_channelProducts.Count() > 0)
                    {
                        //set product order
                        SetProductOrder();
                        //get product images if images are no in amplience
                        if (!_settings.EnableAmplience)
                        {
                            GetMainProductImages();
                        }
                    }
                }
                else
                {
                    _channelProducts = Enumerable.Empty<ChannelProduct>();
                }
            }
            PopulateCategoryModel(categoryId);
            _categoryModel.Id = categoryId;
            #region top content feature section
            if (_category.FeatureEnabled && _settings.IsMobile == false)
            {
                var topshape = _factory.Parts_FeatureCategory(Category: _category, CustomSettings: _settings);
                _workContextAccessor.GetContext().Layout.Zones["TopContent"].Add(topshape);
            }
            #endregion
          
            #region Pager
            if (_settings.ShowPager)
            {
                Pager pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);
                dynamic pagerShape = _factory.Pager(pager).TotalItemCount(_categoryModel.TotalNumberOfProducts);
                _categoryModel.Pager = pagerShape;
                _categoryModel.StartPosition = pager.GetStartIndex(pager.Page) + 1;
                _categoryModel.EndPosition = pager.Page * pager.PageSize;
                if (_categoryModel.EndPosition > _categoryModel.TotalNumberOfProducts)
                {
                    _categoryModel.EndPosition = _categoryModel.TotalNumberOfProducts;
                }
                _categoryModel.ProductDetails = _categoryModel.ProductDetails.Skip(pager.GetStartIndex(pager.Page)).Take(pager.PageSize).ToList();
            }
            #endregion
           
            dynamic shape = null;

            if (_settings.IsMobile)
            {
                shape = _factory.Parts_ProductCatalogBootstrap(CustomSettings: _settings, CatalogViewModel: _categoryModel);
            }
            else
            {
                shape = _factory.Parts_ProductCatalog(CustomSettings: _settings, CatalogViewModel: _categoryModel);
            }
            return new ShapeResult(this, shape);
        }
       
        /// <summary>
        /// Setup view model for collection page.
        /// </summary>
        /// <param name="collectionid"></param>
        private void PopulateCollectionModel(int collectionid)
        {
            _collectionModel.Channel = _settings.ChannelId;
            _collectionModel.CDNPath = _settings.CDNImagesUrl;
            _collectionModel.FeatureEnabled = _collection.FeatureEnabled;
            _collectionModel.Name = _collection.Name;
            _collectionModel.TotalNumberOfProducts = _channelProducts.Count();
            foreach (ChannelProduct product in _channelProducts)
            {
                ProductDetail detail = new ProductDetail();
                if (product.ImageId != null)
                {
                    detail.ImageUrl = _productImages.Where(x => x.Id == product.ImageId).Select(x => x.Url).FirstOrDefault();
                }
                detail.cp = product;
                detail.ProductGroup = product.ProductGroup;
                _collectionModel.ProductDetails.Add(detail);
            }
        }

        /// <summary>
        /// Setup view model for category page
        /// </summary>
        /// <param name="categoryid"></param>
        private void PopulateCategoryModel(int categoryid)
        {
            _categoryModel.Channel = _settings.ChannelId;
            _categoryModel.CDNPath = _settings.CDNImagesUrl;
            _categoryModel.FeatureEnabled = _category.FeatureEnabled;
            _categoryModel.Name = _category.Name;
            _categoryModel.TotalNumberOfProducts = _channelProducts.Count();
            foreach (ChannelProduct product in _channelProducts)
            {
                ProductDetail detail = new ProductDetail();
                if (product.ImageId != null)
                {
                    detail.ImageUrl = _productImages.Where(x => x.Id == product.ImageId).Select(x => x.Url).FirstOrDefault();
                }
                detail.cp = product;
                detail.ProductGroup = product.ProductGroup;
                _categoryModel.ProductDetails.Add(detail);
            }
        }

    }
}