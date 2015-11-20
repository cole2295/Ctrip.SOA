using Ctrip.SOA.Repository.Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Repository.Order.Interface
{
    public interface IOrderInsertDB
    {

        long InsertOrder(OrderEntity entity);

        bool UpdateOrder(OrderEntity entity);

        void DeleteOrder(OrderEntity entity);
    }
}
