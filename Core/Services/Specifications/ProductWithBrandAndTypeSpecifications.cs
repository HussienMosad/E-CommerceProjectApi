using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications :BaseSpecifications<Product , int>
    {
        // Get All Products 
        public ProductWithBrandAndTypeSpecifications(ProductSpacificationParameters parameters) 
            : base(p=> (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
            && (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
            && (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            switch (parameters.Sort)
            {
                case SortingSpecifications.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case SortingSpecifications.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case SortingSpecifications.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case SortingSpecifications.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }

            ApplyingPagination(parameters.PageSize, parameters.PageIndex);
        }

        public ProductWithBrandAndTypeSpecifications(int id) :base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}
