using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        IRentalService _rentalService;
        ICustomerService _customerService;

        public RentalsController(IRentalService rentalService, ICustomerService customerService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getRentalDetails")]
        public IActionResult GetRentalDetails()
        {
            var result = _rentalService.GetRentalDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var customer = _customerService.GetByUserId(int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)).Data;
            rental.CustomerId = customer.Id;
            var result = _rentalService.Add(rental);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
