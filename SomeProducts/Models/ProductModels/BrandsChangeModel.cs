using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SomeProducts.DAL.Models;

namespace SomeProducts.Models.ProductModels
{
    public class BrandsChangeModel
    {
        public List<Brand> RemovedBrands { get; set; }
        public List<Brand> AddedBrands { get; set; }
    }
}