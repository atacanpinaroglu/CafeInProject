using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICafeService
    {
        IResult Add(Cafe cafe);
        IResult Update(Cafe cafe);
        IResult Delete(Cafe cafe);
        IDataResult<List<Cafe>> GetAll();
        IDataResult<Cafe> GetById(int cafeId);       
    }
}
