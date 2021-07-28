using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {

        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage)
        {

            var result = BusinessRules.Run(CheckCarPictureNumber(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            SetDirectory(carImage);
            carImage.Date = DateTime.Now.Date;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.Added);
        }


        public IResult Delete(CarImage carImage)
        {
            File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Pictures/" + _carImageDal.Get(c => c.Id == carImage.Id).ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }


        public IDataResult<List<CarImage>> GetAll()
        {
     
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }


        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }


        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Any())
            {
                return new SuccessDataResult<List<CarImage>>(result);
            }
            return new SuccessDataResult<List<CarImage>>(new List<CarImage>() { 
                new CarImage() { ImagePath = "default.jpg" } });
        }


        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage)
        {
            File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Pictures/" + _carImageDal.Get(c => c.Id == carImage.Id).ImagePath);
            SetDirectory(carImage);
            carImage.Date = DateTime.Now.Date;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.Updated);
        }


        private IResult CheckCarPictureNumber(int carId)
        {
            if (_carImageDal.GetAll(c => c.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.PhotoLimitExceeded);
            }
            return new SuccessResult();
        }

        private void SetDirectory(CarImage carImage)
        {
            string guidKey = Guid.NewGuid().ToString();
            string fileType = carImage.ImagePath.Substring(carImage.ImagePath.LastIndexOf('.'));
            File.Copy(carImage.ImagePath, Directory.GetCurrentDirectory() + "/wwwroot/Pictures/" + guidKey + fileType);
            carImage.ImagePath = guidKey + fileType;
        }
    }
}
