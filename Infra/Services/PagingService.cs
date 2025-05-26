using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;

namespace Infra.Services;

public class PagingService<T>
{
    public static async Task<Model<T>> getPaging(int page, int pageSize, IQueryable<T> result, string additionaldata = "")
    {
        try
        {
            var totalCount = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var dataList = await result.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            Model<T> model = new Model<T>();
            model.Results = dataList;
            model.TotalCount = totalCount;
            model.TotalPages = totalPages;
            model.AdditionalData = additionaldata;
            return model;
        }
        catch (Exception ex)
        {

        }

        return null;

    }

    public static async Task<Model<T>> getPagingList(int page, int pageSize, List<T> result, string additionaldata = "")
    {
        try
        {
            var totalCount = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var dataList = result.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            Model<T> model = new Model<T>();
            model.Results = dataList;
            model.TotalCount = totalCount;
            model.TotalPages = totalPages;
            model.AdditionalData = additionaldata;
            return model;
        }
        catch (Exception ex)
        {

        }

        return null;

    }

    public static PagedListClient<T> Convert(int page, int pageSize, Model<T> data)
    {
        var model = new PagedListClient<T>();
        var pagedList = new StaticPagedList<T>(data.Results, page, pageSize, data.TotalCount);
        model.Results = pagedList;
        model.TotalCount = data.TotalCount;
        model.TotalPages = data.TotalPages;
        model.AdditionalData = data.AdditionalData;
        return model;
    }
    
    public class PagedListServer<T>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string prevLink { get; set; }
    public string nextLink { get; set; }
    public IEnumerable<T> Results { get; set; }
    public string AdditionalData { get; set; }

}


public class PagedListClient<T>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string prevLink { get; set; }
    public string nextLink { get; set; }
    public IPagedList<T> Results { get; set; }
    public string AdditionalData { get; set; }
}

public class Model<T>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string prevLink { get; set; }
    public string nextLink { get; set; }
    public IEnumerable<T> Results { get; set; }
    public string AdditionalData { get; set; }
}

public static class SORTLIT<T>
{
    public static IQueryable<T> Sort(IQueryable<T> source, string Field, string Direction = "asc")
    {
        var type = typeof(T);
        var property = type.GetProperty(Field);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);
        if (Direction == "asc")
        {
            return Queryable.OrderBy(source, orderByExp);
        }
        else
        {
            return Queryable.OrderByDescending(source, orderByExp);
        }
    }
}
}

