using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarImageService _imageService;

        public CarManager(ICarDal carDal, ICarImageService imageService)
        {
            _carDal = carDal;
            _imageService = imageService;
        }

        [SecuredOperation("car.add, admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(3)]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.Updated);
        }


        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(3)]
        public IDataResult<List<Car>> GetAll()
        {
     
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(3)]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = _carDal.GetCarDetails();
            foreach (var car in result)
            {

                if (!car.Images.Any())
                {
                    car.Images.Add(new CarImage { ImagePath="default.jpg" });
                }
            }
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<CarDetailDto> GetCarDetailsById(int id)
        {
            var result = _carDal.GetCarDetails(c => c.CarId == id)[0];
            if (!result.Images.Any())
            {
                result.Images.Add(new CarImage { ImagePath = "default.jpg" });
            }
            return new SuccessDataResult<CarDetailDto>(result);
        }

        public IDataResult<int> GetFindeks(int id)
        {
            return new SuccessDataResult<int>(_carDal.Get(c => c.CarId == id).MinFindeks);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }
    }
}
