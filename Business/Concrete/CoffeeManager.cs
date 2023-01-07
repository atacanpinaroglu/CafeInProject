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
    public class CoffeeManager : ICoffeeService
    {
        ICoffeeDal _coffeeDal;
        public CoffeeManager(ICoffeeDal coffeeDal)
        {
            _coffeeDal = coffeeDal;
        }

        public IResult Add(Coffee coffee)
        {
            _coffeeDal.Add(coffee);
            return new SuccessResult();
        }

        public IResult Update(Coffee coffee)
        {
            _coffeeDal.Update(coffee);
            return new SuccessResult();
        }

        public IResult Delete(Coffee coffee)
        {
            _coffeeDal.Delete(coffee);
            return new SuccessResult();
        }

        public IDataResult<List<Coffee>> GetAll()
        {
            return new SuccessDataResult<List<Coffee>>(_coffeeDal.GetAll());
        }

        public IDataResult<Coffee> GetById(int coffeeId)
        {
            return new SuccessDataResult<Coffee>(_coffeeDal.Get(c => c.CoffeeId == coffeeId));
        }

        public IDataResult<List<Coffee>> GetAllByCafeId(int cafeId)
        {
            var coffees = _coffeeDal.GetAll(c => c.CafeId == cafeId);
            if (coffees.Count > 0)
            {
                return new SuccessDataResult<List<Coffee>>(coffees);
            }
            else
            {
                return new ErrorDataResult<List<Coffee>>();
            }            
        }
    }
}
