using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

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
            var result = _creditCardService.Add(creditCard);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CreditCard creditCard)
        {
            var result = _creditCardService.Delete(creditCard);
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
        public IActionResult Get(int userId)
        {
            var result = _creditCardService.Get(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
