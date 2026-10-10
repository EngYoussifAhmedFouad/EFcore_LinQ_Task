using System;
using System.Collections.Generic;
using System.Text;

namespace EF_session.Models
{
    public class ProductFilter
    {
        
        public string? Text { get; set; }           
        public string? Category { get; set; }       
        public decimal? MinPrice { get; set; }      
        public decimal? MaxPrice { get; set; }      
        public bool InStockOnly { get; set; }       

        
        public string? SortBy { get; set; }         
        public bool IsDescending { get; set; }     

        
        public int PageNumber { get; set; } = 1;    
        public int PageSize { get; set; } = 10;     
    }

}
