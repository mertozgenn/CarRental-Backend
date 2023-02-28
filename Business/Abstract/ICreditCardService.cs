using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCard creditCard);
        IResult Delete(int id, int userId);
        IDataResult<CreditCard> Get(int userId);
    }
}
