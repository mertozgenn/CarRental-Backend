using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal carDal; 

        public CarManager(ICarDal carDal)
        {
            this.carDal = carDal;
        }

        [SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<List<Car>> GetAll()
        {
     
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            
            return new SuccessDataResult<List<CarDetailDto>>(carDal.GetCarDetails());
        }
    }
}
