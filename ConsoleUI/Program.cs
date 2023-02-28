using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal(),new CarImageManager(new EfCarImageDal()));
            // carManager.Update(entity);
            //Color color1 = new Color() { ColorId = 1, ColorName = "Kırmızı" };
            //Color color2 = new Color() { ColorId = 2, ColorName = "Beyaz" };
            //IColorService colorService = new ColorManager(new EfColorDal());
            //colorService.Add(color1);
            //colorService.Add(color2);
            UserManager userManager = new UserManager(new EfUserDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            foreach (var car in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(car.Description + "/" + car.ColorName);
            }
        }
    }
}
