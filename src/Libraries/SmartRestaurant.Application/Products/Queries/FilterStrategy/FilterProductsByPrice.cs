﻿using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Extensions;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Domain.Entities;
using System.Linq;

namespace SmartRestaurant.Application.Products.Queries.FilterStrategy
{
    public class FilterProductsByPrice : IProductFilterStrategy
    {
        public PagedResultBase<Product> FetchData(DbSet<Product> products, GetProductListQuery reques)
        {
            var searchKey = ChecksHelper.IsFloatNumber(reques.SearchKey) ? float.Parse(reques.SearchKey) : -1;
            var comparisonOperator = string.IsNullOrWhiteSpace(reques.ComparisonOperator) ? "==" : reques.ComparisonOperator;
            var sortOrder = string.IsNullOrWhiteSpace(reques.SortOrder) ? "acs" : reques.SortOrder;

            switch (sortOrder)
            {
                case "acs":
                    return Acs(products, reques, searchKey, comparisonOperator);

                case "desc":
                    return Desc(products, reques, searchKey, comparisonOperator);

                default:
                    return Acs(products, reques, searchKey, comparisonOperator);
            }          
        }

        private static PagedResultBase<Product> Acs(DbSet<Product> products, GetProductListQuery reques, float searchKey, string comparisonOperator)
        {
            switch (comparisonOperator)
            {
                case "==":
                    return products
                       .Where(product => product.Price == searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "!=":
                    return products
                       .Where(product => product.Price != searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case ">":
                    return products
                   .Where(product => product.Price > searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);
                case ">=":
                    return products
                       .Where(product => product.Price >= searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "<":
                    return products
                       .Where(product => product.Price < searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "<=":
                    return products
                       .Where(product => product.Price <= searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                default:
                    return products
                       .Where(product => product.Price == searchKey)
                       .OrderBy(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);
            }
        }

        private static PagedResultBase<Product> Desc(DbSet<Product> products, GetProductListQuery reques, float searchKey, string comparisonOperator)
        {
            switch (comparisonOperator)
            {
                case "==":
                    return products
                       .Where(product => product.Price == searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "!=":
                    return products
                       .Where(product => product.Price != searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case ">":
                    return products
                       .Where(product => product.Price > searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);
                case ">=":
                    return products
                       .Where(product => product.Price >= searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "<":
                    return products
                       .Where(product => product.Price < searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                case "<=":
                    return products
                       .Where(product => product.Price <= searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);

                default:
                    return products
                       .Where(product => product.Price == searchKey)
                       .OrderByDescending(product => product.Price)
                       .GetPaged(reques.Page, reques.PageSize);
            }
        }
    }
}
