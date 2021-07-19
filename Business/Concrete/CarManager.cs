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

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal carDal; 

        public CarManager(ICarDal carDal)
        {
            this.carDal = carDal;
        }

        [SecuredOperation("car.add, admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(3)]
        public IResult Add(Car car)
        {
            carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            carDal.Delete(car);
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
            return new SuccessDataResult<List<Car>>(carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        [PerformanceAspect(3)]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<CarDetailDto> GetCarDetailsById(int id)
        {
            return new SuccessDataResult<CarDetailDto>(carDal.GetCarDetails(c => c.CarId == id)[0]);
        }

        public IDataResult<int> GetFindeks(int id)
        {
            return new SuccessDataResult<int>(carDal.Get(c => c.CarId == id).MinFindeks);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(carDal.Get(c => c.CarId == id));
        }
    }
}
