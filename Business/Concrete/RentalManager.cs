using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICustomerService _customerService;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICustomerService customerService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _customerService = customerService;
            _carService = carService;
        }

        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result.Count > 0)
            {

                return new ErrorResult(Messages.CarInvalid);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
          return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentId)
        {
           return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentId));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult FindeksControl(int carId, int customerId)
        {
            int customerFindeks = _customerService.GetById(customerId).Data.Findeks;
            int carFindeks = _carService.GetById(carId).Data.MinFindeks;

            if (customerFindeks<carFindeks)
            {
                return new ErrorResult(Messages.Findeks);
            }
            return new SuccessResult(Messages.SuccesFindeks);
        }
    }
}
