using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CafeManager : ICafeService
    {
        ICafeDal _cafeDal;
        public CafeManager(ICafeDal cafeDal)
        {
            _cafeDal = cafeDal;
        }
        public IResult Add(Cafe cafe)
        {
            _cafeDal.Add(cafe);
            return new SuccessResult();
        }

        public IResult Update(Cafe cafe)
        {
            _cafeDal.Update(cafe);
            return new SuccessResult();
        }

        public IResult Delete(Cafe cafe)
        {
            _cafeDal.Delete(cafe);
            return new SuccessResult();
        }

        public IDataResult<List<Cafe>> GetAll()
        {
            return new SuccessDataResult<List<Cafe>>(_cafeDal.GetAll());
        }

        public IDataResult<Cafe> GetById(int cafeId)
        {
            return new SuccessDataResult<Cafe>(_cafeDal.Get(c => c.CafeId == cafeId));
        }       
    }
}
