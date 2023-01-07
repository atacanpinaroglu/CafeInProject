using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICoffeeService
    {
        IResult Add(Coffee coffee);
        IResult Update(Coffee coffee);
        IResult Delete(Coffee coffee);
        IDataResult<List<Coffee>> GetAll();
        IDataResult<Coffee> GetById(int coffeeId);
        IDataResult<List<Coffee>> GetAllByCafeId(int cafeId);
    }
}
