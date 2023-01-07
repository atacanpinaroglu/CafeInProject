using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderDetailService
    {
        IResult Add(OrderDetail orderDetail);
        IResult Update(OrderDetail orderDetail);
        IResult Delete(OrderDetail orderDetail);
        IDataResult<List<OrderDetail>> GetAll();
        IDataResult<OrderDetail> GetById(int orderDetailId);
        IDataResult<List<OrderDetail>> GetAllByOrderId(int orderId);
    }
}
