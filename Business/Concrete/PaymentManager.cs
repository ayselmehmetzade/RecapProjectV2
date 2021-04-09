using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }
        //[SecuredOperation("findeks.delete,moderator,admin")]
        public IResult Add(Payment payment)
        {
            _paymentDal.Add(payment);

            return new SuccessResult(Messages.Added);
        }

        //[SecuredOperation("findeks.delete,moderator,admin")]
        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);

            return new SuccessResult(Messages.Deleted);
        }
       
        public IResult Update(Payment payment)
        {
            //kontrol eklencek
            _paymentDal.Update(payment);

            return new SuccessResult(Messages.Updated);
        }

        //[SecuredOperation("findeks.get,moderator,admin")]
        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        [SecuredOperation("user")]
        public IDataResult<Payment> GetByCustomerId(int customerId)
        {
            var findeks = _paymentDal.Get(f => f.CustomerId == customerId);
            if (findeks == null) return new ErrorDataResult<Payment>(Messages.NotFound);

            return new SuccessDataResult<Payment>(findeks);
        }

        public IDataResult<Payment> GetById(int id)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(f => f.Id == id));
        }

     
    }
}
