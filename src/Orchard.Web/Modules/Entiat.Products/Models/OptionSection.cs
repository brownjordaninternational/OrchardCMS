using Bj.Essentials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entiat.Products.Models
{
    public class OptionSection
    {
        public Category Category { get; set; }
        public List<Sku> Skus { get; set; }
        public OptionSection()
        {
            Category = new Category();
            Skus = new List<Sku>();
        }

    }
}