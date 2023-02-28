using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }

        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if(userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordSalt, userToCheck.PasswordHash))
            {
                return new ErrorDataResult<AccessToken>(Messages.PasswordError);
            }
            var token = CreateAccessToken(userToCheck);
            return new SuccessDataResult<AccessToken>(token, Messages.SuccessfulLogin);
        }

        public IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordSalt, out passwordHash);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            var result =_userService.Add(user);
            if (result.Success)
            {
                var token = Login(new UserForLoginDto { Password = userForRegisterDto.Password, Email = userForRegisterDto.Email}).Data;
                return new SuccessDataResult<AccessToken>(token, Messages.UserRegistered);
            }
            return new ErrorDataResult<AccessToken>(result.Message);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
