using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

       [HttpGet("getByUserId")]
        public IActionResult GetByUserId()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _customerService.GetByUserId(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            customer.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = _customerService.Add(customer);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
