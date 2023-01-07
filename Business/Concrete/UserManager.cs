using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Add(User user)
        {
            var result = _userDal.Get(u => u.Email.ToLower() == user.Email.ToLower());
            if (result == null)
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.SignUpSuccesful);
            }
            else
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }         
        }

        public IResult Update(User user)
        {          
            _userDal.Update(user);
            return new SuccessResult(Messages.UpdateAccountSettings);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == userId));
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _userDal.GetByEmail(email);
            if (user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            else
            {
                return new ErrorDataResult<User>(Messages.UserNotExist);
            }
        }
    }
}
