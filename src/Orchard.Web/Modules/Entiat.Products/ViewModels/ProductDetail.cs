using Bj.Essentials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entiat.Products.ViewModels
{
    public class ProductDetail
    {
        public string ImageUrl { get; set; }
        public ChannelProduct cp { get; set; }
        public string ProductGroup { get; set; }
        public List<ProductDetail> DetailList { get; set; }
        public ProductDetail()
        {
            cp = new ChannelProduct();
            DetailList = new List<ProductDetail>();
        }
    }
}