using System.Collections.Generic;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int CarId { get; set; }
        public string Description { get; set; }
        public  int ColorId { get; set; }
        public string ColorName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ModelYear { get; set; }
        public int DailyPrice { get; set; }
        public List<CarImage> Images { get; set; }
    }
}
