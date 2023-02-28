using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        AccessToken CreateAccessToken(User user);
    }
}
