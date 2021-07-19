using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            IDataResult<UserDto> result = null;
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                result = _userService.GetUserInfo(userId);
                if (result.Success)
                    return Ok(result);
            }
            catch
            {
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                user.Id = int.Parse(userId.Value);
                user.Status = true;
                var result = _userService.Update(user);
                if (result.Success)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword(IDictionary<string,string> dataDictionary)
        {
            var password = dataDictionary["password"];
            var result = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (result != null)
            {
                var finalResult = _userService.ChangePassword(int.Parse(result.Value), password);
                return Ok(finalResult);
            }

            return BadRequest();
        }
    }
}
