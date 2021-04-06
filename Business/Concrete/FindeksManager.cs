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
    public class FindeksManager : IFindeksService
    {
        IFindeksDal _findeksDal;

        public FindeksManager(IFindeksDal findeksDal)
        {
            _findeksDal = findeksDal;
        }
        //[SecuredOperation("findeks.delete,moderator,admin")]
        public IResult Add(Findeks findeks)
        {
            _findeksDal.Add(findeks);

            return new SuccessResult(Messages.Added);
        }

        //[SecuredOperation("findeks.delete,moderator,admin")]
        public IResult Delete(Findeks findeks)
        {
            _findeksDal.Delete(findeks);

            return new SuccessResult(Messages.Deleted);
        }
       
        public IResult Update(Findeks findeks)
        {
            //kontrol eklencek
            _findeksDal.Update(findeks);

            return new SuccessResult(Messages.Updated);
        }

        //[SecuredOperation("findeks.get,moderator,admin")]
        public IDataResult<List<Findeks>> GetAll()
        {
            return new SuccessDataResult<List<Findeks>>(_findeksDal.GetAll());
        }

        [SecuredOperation("user")]
        public IDataResult<Findeks> GetByCustomerId(int customerId)
        {
            var findeks = _findeksDal.Get(f => f.CustomerId == customerId);
            if (findeks == null) return new ErrorDataResult<Findeks>(Messages.NotFound);

            return new SuccessDataResult<Findeks>(findeks);
        }

        public IDataResult<Findeks> GetById(int id)
        {
            return new SuccessDataResult<Findeks>(_findeksDal.Get(f => f.Id == id));
        }

     
    }
}
