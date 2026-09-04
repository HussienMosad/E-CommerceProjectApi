using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class ProductSpacificationParameters
    {
        private const int DefultPageSize = 5;
        private const int MaxPageSize = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        public SortingSpecifications? Sort { get; set; }

        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        
        private int _pageSize = DefultPageSize;

        public int PageSize {
            get { return _pageSize; } 
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
