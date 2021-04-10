﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
           return new SuccessDataResult<List<Customer>>( _customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>( _customerDal.Get(c=>c.CustomerId==id));
        }
        public IDataResult<Customer> GetByUserId(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.GetAll().Where(c=> c.UserId == id).FirstOrDefault());
        }
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CarUpdated);
        }

       
    }
}
