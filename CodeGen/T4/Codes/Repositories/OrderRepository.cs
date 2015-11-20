/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/

using Ctrip.SOA.Repository.Order.Model;
using Ctrip.SOA.Repository.Order.Interface;
using Ctrip.SOA.Repository.Order.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ctrip.SOA.Repository.Order
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IOrderInsertDB orderInsertDB;
        private readonly IOrderSelectDB orderSelectDB;
        public OrderRepository(IOrderInsertDB orderInsertDB, IOrderSelectDB orderSelectDB)
        {
            this.orderInsertDB = orderInsertDB;
            this.orderSelectDB = orderSelectDB;
        }

        public long AddOrder(OrderEntity entity)
        {
            return orderInsertDB.InsertOrder(entity);
        }

        public bool UpdateOrder(OrderEntity entity)
        {
            return orderInsertDB.UpdateOrder(entity);
        }

        public void DeleteOrder(OrderEntity entity)
        {
            orderInsertDB.DeleteOrder(entity);
        }

        public OrderEntity GetOrder(long orderId)
        {
            return orderSelectDB.SelectOrder(orderId);
        }

        public List<OrderEntity> GetAllOrder()
        {
            return orderSelectDB.SelectAllOrders();
        }
    }
}


