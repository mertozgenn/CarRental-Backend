using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(int id);
        IDataResult<int> GetFindeks(int id);
        IDataResult<List<CarDetailDto>> GetAll();
        IDataResult<List<CarDetailDto>> GetByFilter(CarFilterModel carFilterModel);
        IDataResult<CarDetailDto> GetById(int id);
    }
}
