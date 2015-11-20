using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    public class ServiceProxyFactory
    {
        private readonly static ConcurrentDictionary<Type, Func<object>> _creatorCache = new ConcurrentDictionary<Type, Func<object>>();

        public static TChannel CreateChannel<TChannel>() where TChannel : class
        {
            return (TChannel)new ServiceProxy<TChannel>().GetTransparentProxy();
        }

        public static object CreateChannel(Type type)
        {
            var creator = _creatorCache.GetOrAdd(type, CreateCreator);
            return ((RealProxy)creator()).GetTransparentProxy();

        }

        private static Func<object> CreateCreator(Type type)
        {
            var proxyType = typeof(ServiceProxy<>).MakeGenericType(type);
            return (Func<Object>)Expression.Lambda(
                Expression.New(proxyType.GetConstructor(Type.EmptyTypes))).Compile();
        }
    }
}
