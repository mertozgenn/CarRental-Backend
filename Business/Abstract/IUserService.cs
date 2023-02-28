using Core.Utilities.Results;
using Core.Entities.Concrete;
using System.Collections.Generic;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
        IDataResult<UserDto> GetUserInfo(int userId);
        IResult Add(User user);
        IResult Update(User user);
        IResult ChangePassword(int userId, string password);
    }
}
