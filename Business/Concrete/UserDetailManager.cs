using Business.Abstract;
using Business.Constans;
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
    public class UserDetailManager : IUserDetailService
    {
        IUserDetailDal _userDetailDal;
        public UserDetailManager(IUserDetailDal userDetailDal)
        {
            _userDetailDal = userDetailDal;
        }
        public IResult Add(UserDetail userDetail)
        {
            var uD = _userDetailDal.Get(u => u.UserId == userDetail.UserId);
            if (uD == null)
            {
                _userDetailDal.Add(userDetail);
                return new SuccessResult(Messages.AddAccountDetails);
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IResult Update(UserDetail userDetail)
        {
            var uD = _userDetailDal.Get(u => u.UserId == userDetail.UserId);
            if (uD != null)
            {
                _userDetailDal.Update(userDetail);
                return new SuccessResult(Messages.UpdateAccountDetails);
            }
            else
            {
                return new ErrorResult();
            }        
        }

        public IResult Delete(UserDetail userDetail)
        {
            _userDetailDal.Delete(userDetail);
            return new SuccessResult();
        }

        public IDataResult<List<UserDetail>> GetAll()
        {
            return new SuccessDataResult<List<UserDetail>>(_userDetailDal.GetAll());
        }

        public IDataResult<UserDetail> GetByUserId(int userId)
        {
            var userDetail = _userDetailDal.Get(u => u.UserId == userId);
            if (userDetail != null)
            {
                return new SuccessDataResult<UserDetail>(userDetail);
            }
            else
            {
                return new ErrorDataResult<UserDetail>();
            }            
        }

        public IDataResult<UserDetail> GetByName(string name)
        {
            var userDetail = _userDetailDal.Get(u => u.FirstName == name);
            if (userDetail != null)
            {
                return new SuccessDataResult<UserDetail>(userDetail);
            }
            else
            {
                return new ErrorDataResult<UserDetail>();
            }
        }
    }
}
