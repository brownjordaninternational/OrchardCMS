using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Mvc;
using Entiat.CustomSettings.Models;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entiat.Products.Services;
using Orchard;
using Bj.Essentials.Entities;
using Entiat.Products.Models;
using Newtonsoft.Json;

namespace Entiat.Products.Controllers
{
    [Themed]
    public class ProductController : Controller
    {
        private readonly IEntiatProductService _service;
        private readonly IOrchardServices _oservice;
        private readonly dynamic _factory;
        private readonly EntiatSiteSettingsPart _settings;
        private readonly IWorkContextAccessor _workContextAccessor;

        public ProductController(IShapeFactory factory, IEntiatProductService service, IOrchardServices oservice, IWorkContextAccessor workcontext)
        {
            _service = service;
            _factory = factory;
            _oservice = oservice;
            _settings = _oservice.WorkContext.CurrentSite.As<EntiatSiteSettingsPart>();
            _workContextAccessor = workcontext;
        }
        //Catalog Page By Collection
        [HttpGet]
        public ActionResult GetProductsInCollection(int collectionid)
        {
            Collection collection = _service.GetCollection(_settings.ChannelId, collectionid);
            IEnumerable<ChannelProducts> cp = _service.GetChannelProducts(_settings.ChannelId).Where(x=>x.Collection== collectionid).ToList();
            List<Image> ProductImages = new List<Image>();

            foreach(ChannelProducts p in cp)
            {
                Image image = new Image();
                image = _service.GetProductImages(_settings.ChannelId, Convert.ToInt32(p.Product)).Where(x => x.ParentFolder == "main").FirstOrDefault();

                if(image != null)
                {
                    ProductImages.Add(image);
                }
            }

            if (collection.FeatureEnabled)
            {
                var topshape = _factory.Parts_FeatureCollection(Collection: collection, CustomSettings: _settings);
                _workContextAccessor.GetContext().Layout.Zones["TopContent"].Add(topshape);
            }
            if(true /*replace with collection.isFilterOn*/)
            {
                List<FilterSection> filters = GetFilters(cp);
                filters = filters.OrderBy(x => x.FilterLabel).ToList();
                var filtershape = _factory.Parts_Filters(FilterSection: filters, CustomSettings: _settings, Collection:collection);
                _workContextAccessor.GetContext().Layout.Zones["AsideLeft"].Add(filtershape);
            }

            var shape = _factory.Parts_ProductCatalogByCollection(Products: cp, Collection: collection, CustomSettings: _settings, ProductImages: ProductImages);
            return new ShapeResult(this, shape);
        }

        //Product Detail Page
        public ActionResult GetProductDetail(int channelproductid)
        {
            ChannelProducts cp = _service.GetChannelProduct(channelproductid);
            int prodId = Convert.ToInt32(cp.Product);
            Product product = _service.GetProduct(_settings.CompanyCode,prodId);
            Collection collection = new Collection();
            if(cp.Collection != null)
            {
                collection = _service.GetCollection(_settings.ChannelId, Convert.ToInt32(cp.Collection));
            }
            IEnumerable<Image> images = _service.GetProductImages(_settings.ChannelId,prodId);

            IEnumerable<Option> productOptions = GetOptions(cp);
            List<OptionGroup> optionGroups = GetOptionGroups(productOptions);
            List<Sku> optionSkus = GetSkus(optionGroups);
            List<string> optionCats = optionGroups.Select(x => x.CategoryName).Distinct().ToList();
            List<OptionSelection> optionList = new List<OptionSelection>();
            foreach(string c in optionCats)
            {
                OptionSelection op = new OptionSelection();
                op.Label = c;
                foreach (OptionGroup g in optionGroups)
                {
                    if (g.CategoryName == c)
                    {
                        foreach(Sku s in optionSkus)
                        {
                            if(s.OptionGroup == g.Id)
                            {
                                if(!op.Skus.Select(x=>x.Id).Contains(s.Id))
                                {
                                    if (s.ImageId != null)
                                    {
                                        op.SwatchImages.Add(_service.GetSwatchImage(_settings.ChannelId, Convert.ToInt32(s.ImageId)));
                                    }
                                    op.Skus.Add(s);
                                }
                            }
                        }
                    }
                }
                optionList.Add(op);
            }
            var shape = _factory.Parts_ProductDetail(Product:product, Images:images,ChannelProduct:cp, CustomSettings:_settings, Collection:collection, Option:optionList);
            return new ShapeResult(this, shape);
        }

        public ActionResult FilterProducts(string filters, int collectionid)
        {
            Collection collection = _service.GetCollection(_settings.ChannelId, collectionid);
            List<ChannelProducts> cp = _service.GetChannelProducts(_settings.ChannelId).Where(x => x.Collection == collectionid).ToList();
            List<ChannelProducts> tempList = new List<ChannelProducts>();
            List<Image> ProductImages = new List<Image>();
            List<FilterSelection> filterList = new List<FilterSelection>();
            filterList = JsonConvert.DeserializeObject<List<FilterSelection>>(filters);
            tempList = cp;
            if (filterList != null && filterList.Count > 0)
            {
                foreach (ChannelProducts prod in tempList.ToList())
                {
                    IEnumerable<Option> options = GetOptions(prod);
                    foreach (int category in filterList.Select(x => x.OptionCategoryId).Distinct())
                    {
                        bool isValid = false;
                        List<int> groups = filterList.Where(x => x.OptionCategoryId == category).Select(x => x.OptionGroupId).ToList();
                        foreach (Option o in options)
                        {
                            if (groups.Contains(Convert.ToInt32(o.Group)))
                            {
                                isValid = true;
                                break;
                            }
                        }
                        if (isValid == false)
                        {
                            cp.Remove(prod);
                        }
                    }
                }
            }
            var shape = _factory.Parts_ProductGrid(Products: cp, CustomSettings: _settings, ProductImages: ProductImages);
            return new ShapePartialResult(this, shape);
        }
        private List<FilterSection> GetFilters(IEnumerable<ChannelProducts> channelproducts)
        {
            IEnumerable<Option> optionList = Enumerable.Empty<Option>();
            List<OptionGroup> optionGroupList = new List<OptionGroup>();
            List<Sku> skuList = new List<Sku>();
            List<FilterSection> filters = new List<FilterSection>();
            foreach(ChannelProducts prod in channelproducts)
            {
                IEnumerable<Option> tempoptionList = Enumerable.Empty<Option>();
                List<OptionGroup> tempoptionGroupList = new List<OptionGroup>();
                List<Sku> tempskuList = new List<Sku>();
                tempoptionList = _service.LookupOptions(_settings.CompanyCode,Convert.ToInt32(prod.Product));
                tempoptionGroupList = GetOptionGroups(tempoptionList);
                tempskuList = GetSkus(tempoptionGroupList);
                foreach (OptionGroup g in tempoptionGroupList)
                {
                    if (!optionGroupList.Select(x=>x.Id).ToList().Contains(g.Id))
                    {
                        optionGroupList.Add(g);
                    }
                }
                foreach(Sku s in tempskuList)
                {
                    if(!skuList.Select(x=>x.Id).ToList().Contains(s.Id))
                    {
                        skuList.Add(s);
                    }
                }


            }
            skuList = skuList.OrderBy(x => x.Name).ToList();
            foreach(int category in optionGroupList.Select(x=>x.Category).ToList())
            {
                FilterSection section = new FilterSection();
                
                foreach(OptionGroup group in optionGroupList)
                {
                    if(group.Category == category)
                    {
                        section.FilterLabel = group.CategoryName;
                        section.OptionCategory = category;
                        foreach (Sku s in skuList)
                        {
                            if(group.Id == s.OptionGroup)
                            {
                                if (!section.Skus.Select(x=>x.Id).ToList().Contains(s.Id))
                                {
                                    section.Skus.Add(s);
                                }

                            }
                        }
                    }
                }
                filters.Add(section);
            }
            return filters;
        }
        private List<OptionGroup> GetOptionGroups(IEnumerable<Option> options)
        {
            List<OptionGroup> optionGroups = new List<OptionGroup>();
            List<int?> optionGroupIds = options.Select(x => x.Group).Distinct().ToList();
            optionGroupIds.Remove(null);
            foreach(int id in optionGroupIds)
            {
                optionGroups.Add(_service.GetOptionGroup(_settings.CompanyCode, id));
            }
            return optionGroups;
        }
        private List<Sku> GetSkus(List<OptionGroup> optionGroups)
        {
            List<Sku> skus = new List<Sku>();
            foreach(OptionGroup group in optionGroups)
            {
                IEnumerable<Sku> skusInGroup = _service.GetSkusInGroup(_settings.CompanyCode, group.Id);
                foreach(Sku s in skusInGroup)
                {
                    skus.Add(s);
                }
            }
            return skus;
        }
        private IEnumerable<Option> GetOptions(ChannelProducts product)
        {
            IEnumerable<Option> optionList = Enumerable.Empty<Option>();
            optionList = _service.LookupOptions(_settings.CompanyCode, Convert.ToInt32(product.Product));
            return optionList;
        }
    }
}