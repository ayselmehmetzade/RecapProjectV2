﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramewrok
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, RecapContext>, IUserOperationClaimDal
    {
    }
}
