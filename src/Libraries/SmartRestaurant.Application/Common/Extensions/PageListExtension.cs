﻿using System;
using System.Linq;

namespace SmartRestaurant.Application.Common.Extensions
{
    public class PagedResultBase<TEntity> where TEntity : class
    {
        public int CurrentPage { get; protected internal set; }
        public int PageCount { get; protected internal set; }
        public int PageSize { get; protected internal set; }
        public int RowCount { get; protected internal set; }
        public IQueryable<TEntity> Data { get; protected internal set; }
    }

    public static class PagedListExtension
    {
        public static PagedResultBase<T> GetPaged<T>(this IQueryable<T> query,
            int page, int pageSize) where T : class
        {
            var result = new PagedResultBase<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize);
            return result;
        }
    }
}