using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<UserDto> GetUserInfo(int userId);
    }
}
