using Core.Utilities.Results;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<UserDto>> GetUserInfo(int userId);
        IResult Add(User user);
        IResult Update(User user);
        IDataResult<List<User>> GetAll();
    }
}
