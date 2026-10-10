using System;
using System.Collections.Generic;
using System.Text;

namespace EF_session.Models
{
    public class ProductSearchResult
    {
        public List<Product> Products = new List<Product>();
        public int TotalCount { get; set; }
        
    }
}
