using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserDetailService
    {
        IResult Add(UserDetail userDetail);
        IResult Update(UserDetail userDetail);
        IResult Delete(UserDetail userDetail);
        IDataResult<List<UserDetail>> GetAll();
        IDataResult<UserDetail> GetByUserId(int userId);
        IDataResult<UserDetail> GetByName(string name);
    }
}
