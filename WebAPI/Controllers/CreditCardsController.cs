using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCard creditCard)
        {
            creditCard.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _creditCardService.Add(creditCard);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _creditCardService.Delete(id, userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("pay")]
        public IActionResult Pay(PaymentDto paymentDto)
        {
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _creditCardService.Get(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
