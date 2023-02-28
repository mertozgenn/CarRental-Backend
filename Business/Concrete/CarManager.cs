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
using Entities.Models;
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
        public IResult Delete(int id)
        {
            _carDal.Delete(new Car { CarId = id });
            return new SuccessResult(Messages.Updated);
        }

        [CacheAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceAspect(3)]
        public IDataResult<List<CarDetailDto>> GetAll()
        {
            var result = _carDal.GetCarDetails();
            SetDefaultImages(result);
            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<CarDetailDto>> GetByFilter(CarFilterModel carFilterModel)
        {
            bool isBrandSelected = carFilterModel.BrandId != null && carFilterModel.BrandId != 0;
            bool isColorSelected = carFilterModel.ColorId != null && carFilterModel.ColorId != 0;
            var data = _carDal.GetCarDetails(x => (isBrandSelected ? x.BrandId == carFilterModel.BrandId : true) &&
                                       (isColorSelected ? x.ColorId == carFilterModel.ColorId : true));
            SetDefaultImages(data);
            return new SuccessDataResult<List<CarDetailDto>>(data);
        }

        private void SetDefaultImages(List<CarDetailDto> cars)
        {
            foreach (var car in cars)
            {

                if (!car.Images.Any())
                {
                    car.Images.Add(new CarImage { ImagePath = "default.jpg" });
                }
            }
        }

        public IDataResult<CarDetailDto> GetById(int id)
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
    }
}
