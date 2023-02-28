using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int id, int userId)
        {
            var creditCard = _creditCardDal.Get(c => c.Id == userId);
            if (creditCard.UserId != userId)
            {
                return new ErrorResult(Messages.AuthorizationDenied);
            }
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<CreditCard> Get(int userId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.UserId == userId));
        }
    }
}
