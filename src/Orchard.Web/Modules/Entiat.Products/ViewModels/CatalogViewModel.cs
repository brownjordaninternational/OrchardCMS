using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entiat.Products.ViewModels
{
    public class CatalogViewModel
    {
        public int Id { get; set; }
        public int Channel { get; set; }
        public string CDNPath { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public dynamic Pager { get; set; }
        public bool FeatureEnabled { get; set; }
        public string Name { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public List<ProductDetail> ProductDetails { get; set;}
        
        public CatalogViewModel()
        {
            ProductDetails = new List<ProductDetail>();
        }
    }
}