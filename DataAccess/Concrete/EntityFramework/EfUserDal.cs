using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarsContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new CarsContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public List<UserDto> GetUserInfo(int userId)
        {
            using (var context = new CarsContext())
            {
                var result = from user in context.Users
                    join customer in context.Customers
                        on user.Id equals customer.UserId
                    where user.Id == userId
                    select new UserDto
                    {
                        CompanyName = customer.CompanyName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                return result.ToList();
            }
        }
    }
}
