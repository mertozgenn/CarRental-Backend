using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getAllByColor")]
        public IActionResult GetAllByColor(int colorId)
        {
            var result = _carService.GetAllByColor(colorId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getAllByBrand")]
        public IActionResult GetAllByBrand(int brandId)
        {
            var result = _carService.GetAllByBrand(brandId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getCarFindeks")]
        public IActionResult GetCarFindeks(int id)
        {
            var result = _carService.GetFindeks(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _carService.Delete(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
