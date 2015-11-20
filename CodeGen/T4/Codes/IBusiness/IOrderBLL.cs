/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using Ctrip.SOA.Bussiness.Order.DataContract;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrip.SOA.Bussiness.Order.IBusiness
{
    public interface IOrderBLL
    {
        OrderModelResponse AddOrder(OrderModelRequest entity);

        BaseResponse UpdateOrder(OrderModelRequest entity);

        BaseResponse DeleteOrder(OrderModelRequest entity);

        List<OrderModelResponse> GetAllOrders();

        OrderModelResponse GetOrder(OrderModelRequest entity);
    }
}

