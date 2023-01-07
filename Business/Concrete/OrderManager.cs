using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult();
        }

        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult();
        }

        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult();
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll());
        }

        public IDataResult<Order> GetById(int orderId)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(o => o.OrderId == orderId));
        }
        
        public IDataResult<int> GetOrderId(Order order)
        {
            var ord = _orderDal.Get(o => o.UserId == order.UserId && o.OrderPrice == order.OrderPrice && o.OrderDate.Hour == order.OrderDate.Hour && o.OrderDate.Minute == order.OrderDate.Minute);
            return new SuccessDataResult<int>(ord.OrderId);
        }

        public IDataResult<List<Order>> GetAllByUserId(int userId)
        {
            var orders = _orderDal.GetAll(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToList();
            if (orders.Count > 0)
            {
                if (orders.Count <= 3)
                {
                    return new SuccessDataResult<List<Order>>(orders);
                }
                else
                {
                    List<Order> lastOrders = new List<Order>();
                    for (int i = 0; i < 3; i++)
                    {
                        lastOrders.Add(orders[i]);
                    }
                    return new SuccessDataResult<List<Order>>(lastOrders);
                }
            }
            else
            {
                return new ErrorDataResult<List<Order>>();
            }
        }
    }
}
