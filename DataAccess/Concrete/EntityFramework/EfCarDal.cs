using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarsContext> ,ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarsContext context = new CarsContext())
            {
                var result = from car in context.Cars
                             join colors in context.Colors
                             on car.ColorId equals colors.ColorId
                             join brands in context.Brands
                             on car.BrandId equals brands.BrandId
                             select new CarDetailDto
                             {
                                 CarId = car.CarId,
                                 BrandId = car.BrandId,
                                 BrandName = brands.BrandName,
                                 ColorId = car.ColorId,
                                 ColorName = colors.ColorName,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
