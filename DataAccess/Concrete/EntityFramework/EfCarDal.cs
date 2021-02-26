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
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarsContext context = new CarsContext())
            {
                var result = from car in context.Cars
                             join colors in context.Color
                             on car.ColorId equals colors.ColorId
                             select new CarDetailDto
                             {
                                 CarId = car.CarId,
                                 ColorName = colors.ColorName,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear
                             };
                return result.ToList();
            }
        }
    }
}
