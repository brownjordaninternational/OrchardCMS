using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bj.Essentials.Entities;
using Entiat.CustomSettings.Models;

namespace Entiat.Products.ViewModels
{
    public class PoolsideCookieViewModel
    {
        public List<int> PoolsideProductIds { get; set; }
        public List<int> PoolsideCollectionIds { get; set; }
    }

    public class PoolsideProduct
    {
        public Product product { get; set; }
        public ChannelProduct channelProduct { get; set; }
        public string ImageUrl { get; set; }

    }
    public class PoolsideViewModel
    {
        public EntiatSiteSettingsPart CustomSettings { get; set; }
        public List<PoolsideProduct> PoolsideProducts { get; set; }
        public List<Collection> PoolsideCollections { get; set; }

        public PoolsideViewModel()
        {
            CustomSettings = new EntiatSiteSettingsPart();
            PoolsideCollections = new List<Collection>();
            PoolsideProducts = new List<PoolsideProduct>();
        }
    }
}