﻿using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfMatchDal : EfEntityRepositoryBase<Match, BrokerContext>, IMatchDal
    {
		
	}
}
