using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(Order order);
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int orderId);
        IDataResult<int> GetOrderId(Order order);
        IDataResult<List<Order>> GetAllByUserId(int userId);
    }
}
