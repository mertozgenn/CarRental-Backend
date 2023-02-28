using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
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
        public IResult Add(IFormFile file, int carId)
        {
            var result = BusinessRules.Run(CheckCarPictureNumber(carId));
            if (result != null)
            {
                return result;
            }
            var path = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(path, FileMode.Create))
                    file.CopyTo(stream);
            var carImage = new CarImage { CarId = carId, ImagePath = path };
            PlaceFile(carImage);
            carImage.Date = DateTime.Now.Date;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.Added);
        }


        public IResult Delete(int imageId)
        {
            File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Pictures/" + _carImageDal.Get(c => c.Id == imageId).ImagePath);
            _carImageDal.Delete(new CarImage { Id = imageId });
            return new SuccessResult(Messages.Deleted);
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


        private IResult CheckCarPictureNumber(int carId)
        {
            if (_carImageDal.GetAll(c => c.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.PhotoLimitExceeded);
            }
            return new SuccessResult();
        }

        private void PlaceFile(CarImage carImage)
        {
            string guidKey = Guid.NewGuid().ToString();
            string fileType = carImage.ImagePath.Substring(carImage.ImagePath.LastIndexOf('.'));
            string newName = guidKey + fileType;
            File.Move(carImage.ImagePath, Directory.GetCurrentDirectory() + "/wwwroot/Pictures/" + newName);
            carImage.ImagePath = newName;
        }
    }
}
