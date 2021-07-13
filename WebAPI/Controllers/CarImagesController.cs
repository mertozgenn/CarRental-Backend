using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CarImage carImage)
        {
            var result = _carImageService.Update(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromForm(Name=("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var fileType = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var path = Path.GetTempFileName();
            path = path.Replace(".tmp", fileType);
            if (file.Length > 0)
                using (var stream = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(stream);
            var carimage = new CarImage { CarId = carImage.CarId, ImagePath = path};

            var result = _carImageService.Add(carimage);

            if (result.Success)
            {
                return Ok(result);
            }
            System.IO.File.Delete(path);
            return BadRequest(result);
        }

        

        [HttpGet("getByCarId")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
