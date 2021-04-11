using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userdal;

        public UserManager(IUserDal userdal)
        {
            _userdal = userdal;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userdal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(User user)
        {
            _userdal.Delete(user);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userdal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
           return new SuccessDataResult<User>(_userdal.Get(u=>u.UserId==id));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userdal.Get(u => u.Email == email));
            
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userdal.GetClaims(user));
            
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            var currentUser = _userdal.Get(x => x.UserId == user.UserId);
            if (currentUser == null)
            {
                return new ErrorResult("User Doesn't exists");
            }
            var userForUpdate = user;
            userForUpdate.Status = currentUser.Status;
            userForUpdate.PasswordHash = currentUser.PasswordHash;
            userForUpdate.PasswordSalt = currentUser.PasswordSalt;
            _userdal.Update(userForUpdate);
            return new SuccessResult(Messages.CarUpdated);
        }


        public IResult ChangeUserPassword(ChangePasswordDto changePasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            var userToCheck = GetByMail(changePasswordDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.OldPassWord, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.Data.PasswordHash = passwordHash;
            userToCheck.Data.PasswordSalt = passwordSalt;
            _userdal.Update(userToCheck.Data);
            return new SuccessResult(Messages.PasswordChanged);
        }


    }
}
