using Ctrip.SOA.Repository.Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Repository.Order.Interface
{
    public interface IOrderSelectDB
    {
        /// <summary>
        /// 获得数据实体。
        /// </summary>
        /// <param name="orderId">数据实体的Id。</param>
        /// <returns>数据实体信息。</returns>
        OrderEntity SelectOrder(long orderId);

        List<OrderEntity> SelectAllOrders();
    }
}
