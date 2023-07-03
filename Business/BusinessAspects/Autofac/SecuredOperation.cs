﻿using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation() 
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); 

        }

        protected override void OnBefore(IInvocation invocation) 
        {
            //var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); 
            //foreach (var role in _roles)
            //{
            //    if (roleClaims.Contains(role)) 
            //    {
            //        return;
            //    }
            //}
            //throw new UnauthorizedAccessException();

            
        }
    }
}