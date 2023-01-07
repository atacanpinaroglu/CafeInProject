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

namespace Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        IOrderDetailDal _orderDetailDal;
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }
        public IResult Add(OrderDetail orderDetail)
        {
            _orderDetailDal.Add(orderDetail);
            return new SuccessResult();
        }

        public IResult Update(OrderDetail orderDetail)
        {
            _orderDetailDal.Update(orderDetail);
            return new SuccessResult();
        }

        public IResult Delete(OrderDetail orderDetail)
        {
            _orderDetailDal.Delete(orderDetail);
            return new SuccessResult();
        }

        public IDataResult<List<OrderDetail>> GetAll()
        {
            return new SuccessDataResult<List<OrderDetail>>(_orderDetailDal.GetAll());
        }

        public IDataResult<OrderDetail> GetById(int orderDetailId)
        {
            return new SuccessDataResult<OrderDetail>(_orderDetailDal.Get(o => o.OrderDetailId == orderDetailId));
        }

        public IDataResult<List<OrderDetail>> GetAllByOrderId(int orderId)
        {
            return new SuccessDataResult<List<OrderDetail>>(_orderDetailDal.GetAll(o => o.OrderId == orderId));
        }
    }
}
