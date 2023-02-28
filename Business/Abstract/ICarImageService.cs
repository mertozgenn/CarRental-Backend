using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetByCarId(int carId);
        IResult Add(IFormFile file, int carId);
        IResult Delete(int imageId);
    }
}
