using System.Collections.Generic;
using System.Linq;
using Orchard.UI.Navigation;
using Entiat.ProductsMenuItem.Services;
using Orchard;
using Entiat.CustomSettings.Models;
using Orchard.ContentManagement;
using Bj.Essentials.Entities;
using Orchard.Localization;

namespace Entiat.ProductsMenuItem.Navigation
{
    public class NavigationProductsProvider : INavigationFilter
    {
        private readonly IProductsMenuItemService _service;
        private readonly IOrchardServices _oservice;

        public NavigationProductsProvider(IProductsMenuItemService service, IOrchardServices oservice)
        {
            _service = service;
            _oservice = oservice;
        }

        public IEnumerable<MenuItem> Filter(IEnumerable<MenuItem> items)
        {
            foreach(MenuItem item in items)
            {
                if(item.Content != null && item.Content.ContentItem.ContentType == "NavigationProductsMenuItem")
                {
                    var settings = _oservice.WorkContext.CurrentSite.As<EntiatSiteSettingsPart>();
                    IEnumerable<Category> categories = _service.GetAllCategories(settings.ChannelId).Where(x => x.Active == true && x.IsMenuItem == true).ToList().OrderBy(x=>x.Sequence);
                    List<Category> menuList = new List<Category>();
                    List<Category> subcategories = new List<Category>();
                    foreach (Category c in categories)
                    {
                        if(c.ParentId != null && c.ParentId != 0)
                        {
                            subcategories.Add(c);
                        }
                        else 
                        {
                            menuList.Add(c);
                        }
                    }
                    //order sub categories alphabetically
                    subcategories.OrderBy(i => i.Sequence);

                    int index = 0;
                    var url = "";
                    foreach(Category c in menuList)
                    {
                       
                        if(c.HasCLP != null && c.HasCLP == true)
                        {
                            url = string.Format("~/{0}", c.Name.ToLower().Replace(" ", "-"));
                        }
                        else
                        {
                            url = string.Format("~/category/{0}", c.Id);
                        }
                        List<Category> tempList = subcategories.Where(x => x.ParentId == c.Id).ToList();
                        List<MenuItem> subCatList = new List<MenuItem>();
                        if (tempList.Count > 0)
                        {
                            int subcatIndex = 0;
                            string suburl = "";
                            
                            foreach (Category sub in tempList) {
                                if(sub.HasCLP != null && sub.HasCLP == true)
                                {
                                    suburl = string.Format("~/{0}", sub.Name.ToLower().Replace(" ", "-"));
                                }
                                else
                                {
                                    suburl = string.Format("~/category/{0}", sub.Id);
                                }
                                MenuItem t = new MenuItem();
                                t.Text = new LocalizedString(sub.Name);
                                t.Url = suburl;
                                t.Items = new MenuItem[0];
                                t.Position = item.Position + ":" + subcatIndex++;
                                subCatList.Add(t);
                            }
                            
                        }
                        var inserted = new MenuItem
                        {
                            Text = new LocalizedString(c.Name),
                            Url = url,
                            Items = subCatList,
                            Position = item.Position + ":" + index++,
                        };

                        yield return inserted;
                    }
                }
                else
                {
                    yield return item;
                }
            }
        }
    }
}