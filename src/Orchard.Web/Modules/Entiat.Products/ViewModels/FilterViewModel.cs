using Entiat.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entiat.Products.ViewModels
{
    public class FilterViewModel
    {
        public int PageTypeId { get; set; }
        public string PageType { get; set; }
        public int Channel { get; set; }
        public string CDNPath { get; set; }
        public List<FilterSection> Filters { get; set; }

        public FilterViewModel()
        {
            Filters = new List<FilterSection>();
        }
    }
}