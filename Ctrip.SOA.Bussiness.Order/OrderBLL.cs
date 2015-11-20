/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:01:46 
*/

using Ctrip.SOA.Bussiness.Order.DataContract;
using Ctrip.SOA.Bussiness.Order.IBusiness;
using Ctrip.SOA.Repository.Order.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrip.SOA.Infratructure.Utility;
using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Repository.Order.Model;

namespace Ctrip.SOA.Bussiness.Order
{
    public class OrderBLL:IOrderBLL
    {
        private readonly IOrderRepository orderRepository;

        public OrderBLL(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public OrderModelResponse AddOrder(OrderModelRequest entity)
        {
            try
            {
                var orderEntity = entity.MapTo<OrderEntity>();
               
                long id = orderRepository.AddOrder(orderEntity);

                return id > 0 ? new OrderModelResponse { OrderId = id, IsSuccess = true } : new OrderModelResponse { IsSuccess = false, OrderId = 0 };
            }
            catch(Exception ex)
            {
                return new OrderModelResponse { OrderId = 0, IsSuccess = false, Msg = ex.Message };
            }

        }

        public BaseResponse UpdateOrder(OrderModelRequest entity)
        {
            try
            {
                var orderEntity = entity.MapTo<OrderEntity>();
                var ordermodel = orderRepository.GetOrder(orderEntity.OrderId);

                if (ordermodel == null)
                    return new BaseResponse { IsSuccess = false, Msg = "can not find mddel" };

             
                bool result = orderRepository.UpdateOrder(ordermodel);

                return result ? new BaseResponse { IsSuccess = true } : new BaseResponse { IsSuccess = false};
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public BaseResponse DeleteOrder(OrderModelRequest entity)
        {
            try
            {
                var orderEntity = entity.MapTo<OrderEntity>();
                orderRepository.DeleteOrder(orderEntity);

                return new BaseResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse { IsSuccess = false, Msg = ex.Message };
            }
        }

        public List<OrderModelResponse> GetAllOrders()
        {
            try
            {
                List<OrderEntity> orders = orderRepository.GetAllOrder();

                return orders.MapToList<OrderModelResponse>();
            }
            catch (Exception)
            {
                return null ;
            }
        }

        public OrderModelResponse GetOrder(OrderModelRequest entity)
        {
            try
            {
                OrderEntity order = orderRepository.GetOrder(entity.OrderId);

                return order.MapTo<OrderModelResponse>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


