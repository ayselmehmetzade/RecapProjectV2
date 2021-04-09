using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<Payment> GetById(int id);
        IDataResult<Payment> GetByCustomerId(int customerId);
        IDataResult<List<Payment>> GetAll();
        IResult Add(Payment payment);
        IResult Delete(Payment payment);
        IResult Update(Payment payment);

    }
}
