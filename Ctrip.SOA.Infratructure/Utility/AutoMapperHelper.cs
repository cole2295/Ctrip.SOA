using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility
{
    public static class AutoMapperHelper
    {
        //[DebuggerStepThrough]
        //public static T Convert<T>(object obj)
        //{
        //    if (obj == null) return default(T);
        //    Mapper.CreateMap(obj.GetType(), typeof(T));
        //    return Mapper.Map<T>(obj);
        //}

        /// <summary>
        ///  类型映射
        /// </summary>
        [DebuggerStepThrough]
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);
            Mapper.CreateMap(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }

        [DebuggerStepThrough]
        public static void CreateAutoMap<T>(this object obj)
        {
            Mapper.CreateMap(obj.GetType(), typeof(T));
        }

        [DebuggerStepThrough]
        public static T MapTo<T, T2Source, T2Destination>(this object obj)
        {
            if (obj == null) return default(T);
            Mapper.CreateMap(obj.GetType(), typeof(T));
            Mapper.CreateMap(typeof(T2Source), typeof(T2Destination));
            return Mapper.Map<T>(obj);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        [DebuggerStepThrough]
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            if (source == null) return default(List<TDestination>);

            foreach (var first in source)
            {
                var type = first.GetType();
                Mapper.CreateMap(type, typeof(TDestination));
                break;
            }
            return Mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射，同时包含一个子类型
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="T2Source"></typeparam>
        /// <typeparam name="T2Destination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static List<TDestination> MapToList<TDestination, T2Source, T2Destination>(this IEnumerable source)
        {
            foreach (var first in source)
            {
                var type = first.GetType();
                Mapper.CreateMap(type, typeof(TDestination));
                break;
            }

            Mapper.CreateMap(typeof(T2Source), typeof(T2Destination));
            return Mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        [DebuggerStepThrough]
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            //IEnumerable<T> 类型需要创建元素的映射
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        [DebuggerStepThrough]
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// DataReader映射
        /// </summary>
        [DebuggerStepThrough]
        public static IEnumerable<T> DataReaderMapTo<T>(this IDataReader reader)
        {
            Mapper.Reset();
            Mapper.CreateMap<IDataReader, IEnumerable<T>>();
            return Mapper.Map<IDataReader, IEnumerable<T>>(reader);
        }
    }
}
