using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace HighSpotJson.Helper
{
    public class ModelHelper
    {
        /// <summary>
        /// Get new Playlist id for new playlist 
        /// </summary>
        /// <param name="iModel"></param>
        /// <returns></returns>
        public static string GetNewPlayListID(Model.MixtapeDatamodel iModel)
        {
            //to do use lock to make sure multiuple processes not getting the same id
            // find a better way to generate uniqueue ID  
            Int64 newUnique = Convert.ToInt64(iModel.playlists.MaxObject(x => x.id).id);
            return newUnique++.ToString();
        }
    }
}



static class EnumerableExtensions
{

    // public static IEnumerable<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
    //         Func<TSource, TKey> selector, IComparer<TKey> comparer)
    // {
    //     if (source == null) throw new ArgumentNullException(nameof(source));
    //     if (selector == null) throw new ArgumentNullException(nameof(selector));

    //     comparer ??= Comparer<TKey>.Default;
    //     return new IEnumerable<TSource, TKey>(source, selector, (x, y) => comparer.Compare(x, y));
    // }


    public static T MaxObject<T, U>(this IEnumerable<T> source, Func<T, U> selector)
      where U : IComparable<U>
    {
        if (source == null) throw new ArgumentNullException("source");
        bool first = true;
        T maxObj = default(T);
        U maxKey = default(U);
        foreach (var item in source)
        {
            if (first)
            {
                maxObj = item;
                maxKey = selector(maxObj);
                first = false;
            }
            else
            {
                U currentKey = selector(item);
                if (currentKey.CompareTo(maxKey) > 0)
                {
                    maxKey = currentKey;
                    maxObj = item;
                }
            }
        }
        if (first) throw new InvalidOperationException("Sequence is empty.");
        return maxObj;
    }
}