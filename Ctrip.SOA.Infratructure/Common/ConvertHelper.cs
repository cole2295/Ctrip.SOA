using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Collections;

namespace Ctrip.SOA.Infratructure.Common
{
    public static class ConvertHelper
    {
        public static T Convert<T>(object obj) {
            if (obj == null) return default(T);

            Mapper.CreateMap(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }



        public static T MapTo<T>(this object obj)
        {
            if (obj == null)
                return default(T);
            Mapper.CreateMap(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }

        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            foreach (var first in source)
            {
                var type = first.GetType();
                Mapper.CreateMap(type, typeof(TDestination));
                break;
            }
            return Mapper.Map<List<TDestination>>(source);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            //IEnumerable<T> 类型需要创建元素的映射
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<List<TDestination>>(source);
        }


        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            if (source == null)
                return destination;

            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
