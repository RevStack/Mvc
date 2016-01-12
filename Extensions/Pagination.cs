using System;
using System.Collections.Generic;
using System.Linq;


namespace RevStack.Mvc
{
    public static partial class Extensions
    {
        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return source.AsEnumerable()
                .Skip(skip).Take(pageSize)
                .ToList();
        }
    }
}
