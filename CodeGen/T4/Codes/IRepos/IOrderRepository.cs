/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using Ctrip.SOA.Repository.Order.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Repository.Order.IRepository
{
    public interface IOrderRepository
    {
        long AddOrder(OrderEntity entity);

        bool UpdateOrder(OrderEntity entity);

        void DeleteOrder(OrderEntity entity);

        OrderEntity GetOrder(long orderId);

        List<OrderEntity> GetAllOrder();
    }
}


