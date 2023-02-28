using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _customerService = customerService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental, int userId)
        {
            IResult result = BusinessRules.Run(CheckIfCarIsReturned(rental));
            if (result != null)
            {
                return result;
            }
            var customer = _customerService.GetByUserId(userId).Data;
            rental.CustomerId = customer.Id;
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalSucceeded);
        }

        public IDataResult<List<RentalDetailDto>> GetAll()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        private IResult CheckIfCarIsReturned(Rental rental)
        {
            if (_rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate == null).Any())
            {
                return new ErrorResult(Messages.RentalFailed);
            }

            return new SuccessResult();
        }
    }
}
