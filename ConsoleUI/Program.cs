using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Car entity = new Car(){Id = 1, BrandId = 2, ColorId = 1, DailyPrice = 50000, Description = "Peugeot 208", ModelYear = "2006" };
            // EfCarDal entityRepository = new EfCarDal();
            // entityRepository.Update(entity);
            CarManager carManager = new CarManager(new EfCarDal());
            // carManager.Update(entity);
            //Color color1 = new Color() { ColorId = 1, ColorName = "Kırmızı" };
            //Color color2 = new Color() { ColorId = 2, ColorName = "Beyaz" };
            //IColorService colorService = new ColorManager(new EfColorDal());
            //colorService.Add(color1);
            //colorService.Add(color2);
            UserManager userManager = new UserManager(new EfUserDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            //Console.WriteLine(userManager.Add(new User() { Id = 1, FirstName = "Mert", LastName = "Özgen", Email = "mert@h", Password = "123" }).Message);
            //Console.WriteLine(userManager.Add(new User() { Id = 2, FirstName = "Elif", LastName = "Yiğit", Email = "elif@g", Password = "1234" }).Message);
            //Console.WriteLine(customerManager.Add(new Customer() {Id = 1, UserId = 1, CompanyName = "Özgen" }).Message);
            //Console.WriteLine(rentalManager.Add(new Rental() { Id = 2, CarId = 1, CustomerId = 2, RentDate = "19.01.2021" }).Message);
            //Console.WriteLine(customerManager.Add(new Customer() { Id = 2, UserId = 2, CompanyName = "Yiğit" }).Message);
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.Description + "/" + car.ColorName);
            }
        }
    }
}
