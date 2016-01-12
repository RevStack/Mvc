using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevStack.Mvc
{
    public static partial class Extensions
    {
       
        /// <summary>
        /// Exception handling for SingleOrDefault() 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="src"></param>
        /// <param name="parseSequence"></param>
        /// <returns></returns>
        public static TSource ToSingleOrDefault<TSource>(this IEnumerable<TSource> src)
        {
            try
            {
                return src.SingleOrDefault();
            }
            catch (Exception)
            {
                if(src !=null && src.Any())
                {
                    return src.FirstOrDefault();
                }else
                {
                    return default(TSource);
                }
            }
        }


        /// <summary>
        /// Uses EqualityComparer<T>.Default as an equality comparison for generics not constrained by a reference type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Compare<TSource,TKey>(this TSource src, TKey id,TKey value)
        {
            return EqualityComparer<TKey>.Default.Equals(id, value);
        }
    }
}
