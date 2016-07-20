using Bj.Essentials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entiat.Products.Models
{
    public class FilterSection
    {
       public string FilterLabel { get; set; }
       public int OptionId { get; set; }
       public List<Sku> Skus { get; set; }
        public FilterSection()
        {
            Skus = new List<Sku>();
        }
    }
}