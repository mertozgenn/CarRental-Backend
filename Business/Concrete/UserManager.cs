using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System.Collections.Generic;
using Core.Utilities.Security.Hashing;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<List<User>> GetAll()
        {
            _userDal.GetAll();
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<UserDto> GetUserInfo(int userId)
        {
            _userDal.GetUserInfo(userId);
            return new SuccessDataResult<UserDto>(_userDal.GetUserInfo(userId));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult ChangePassword(int userId, string password)
        {
            var userToUpdate = _userDal.Get(u => u.Id == userId);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordSalt, out passwordHash);
            userToUpdate.PasswordHash = passwordHash;
            userToUpdate.PasswordSalt = passwordSalt;
            _userDal.Update(userToUpdate);
            return new SuccessResult(Messages.PasswordChanged);
        }
    }
}
