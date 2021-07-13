using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class PaymentDto : IDto
    {
        public CreditCard CreditCard { get; set; }
        public double TotalPrice { get; set; }
    }
}
