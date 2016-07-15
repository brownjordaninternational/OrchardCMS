using Bj.Essentials.Entities;
using Orchard.ContentManagement;
using System.Collections.Generic;

namespace Entiat.CustomSettings.Models
{
    public class EntiatSiteSettingsPart : ContentPart
    {
        public string CDNImagesUrl
        {
            get { return this.Retrieve(x => x.CDNImagesUrl); }
            set { this.Store(x => x.CDNImagesUrl, value); }
        }
        public int ChannelId
        {
            get { return this.Retrieve(x => x.ChannelId); }
            set { this.Store(x => x.ChannelId, value); }
        }
        public string CompanyCode
        {
            get { return this.Retrieve(x => x.CompanyCode); }
            set { this.Store(x => x.CompanyCode, value); }
        }
        public bool IsMobile
        {
            get { return this.Retrieve(x => x.IsMobile); }
            set { this.Store(x => x.IsMobile, value); }
        }
        public bool EnableAmplience
        {
            get { return this.Retrieve(x => x.EnableAmplience); }
            set { this.Store(x => x.EnableAmplience, value); }
        }
        public bool EnableWishlist
        {
            get { return this.Retrieve(x => x.EnableWishlist); }
            set { this.Store(x => x.EnableWishlist, value); }
        }
        public string Facebook
        {
            get { return this.Retrieve(x => x.Facebook); }
            set { this.Store(x => x.Facebook, value); }
        }
        public string Pinterest
        {
            get { return this.Retrieve(x => x.Pinterest); }
            set { this.Store(x => x.Pinterest, value); }
        }
        public string Instagram
        {
            get { return this.Retrieve(x => x.Instagram); }
            set { this.Store(x => x.Instagram, value); }
        }
        public string Twitter
        {
            get { return this.Retrieve(x => x.Twitter); }
            set { this.Store(x => x.Twitter, value); }
        }
        public bool ShowPager
        {
            get { return this.Retrieve(x => x.ShowPager); }
            set { this.Store(x => x.ShowPager, value); }
        }
        public int ProductsPerRow
        {
            get { return this.Retrieve(x => x.ProductsPerRow); }
            set { this.Store(x => x.ProductsPerRow, value); }
        }
        public bool GroupByProductGroup
        {
            get { return this.Retrieve(x => x.GroupByProductGroup); }
            set { this.Store(x => x.GroupByProductGroup, value); }
        }
        public string ProductOrder
        {
            get { return this.Retrieve(x => x.ProductOrder); }
            set { this.Store(x => x.ProductOrder, value); }
        }
        public bool HideProductName
        {
            get { return this.Retrieve(x => x.HideProductName); }
            set { this.Store(x => x.HideProductName, value); }
        }
        public bool ShowModelNum
        {
            get { return this.Retrieve(x => x.ShowModelNum); }
            set { this.Store(x => x.ShowModelNum, value); }
        }
        public bool ShowOptionDropdowns
        {
            get { return this.Retrieve(x => x.ShowOptionDropdowns); }
            set { this.Store(x => x.ShowOptionDropdowns, value); }
        }
        public bool ShowMSRP
        {
            get { return this.Retrieve(x => x.ShowMSRP); }
            set { this.Store(x => x.ShowMSRP, value); }
        }
        public IEnumerable<Channel> channelList
        {
            get; set;
        }
        public IEnumerable<Company> companyList
        {
            get; set;
        }
    }


}