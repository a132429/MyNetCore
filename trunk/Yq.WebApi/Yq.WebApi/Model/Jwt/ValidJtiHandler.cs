using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Yq.Application.UserApp;
using Yq.Utility;

namespace Yq.WebApi.Model.Jwt
{
    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        private readonly IUserAppService _dbContext;

        public ValidJtiHandler(IUserAppService dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {
            // 检查 Jti 是否存在
            var userID = context.User.FindFirst("guid")?.Value;
            var jti = context.User.FindFirst("jti")?.Value; 
            var exp = context.User.FindFirst("exp")?.Value;//结束时间 
            if (jti == null|| (exp!=null&& Common.ConvertIntDateTime(double.Parse(exp))<DateTime.Now))
            {
                context.Fail(); // 显式的声明验证失败
                return Task.CompletedTask;
            }
            string myJti= RedisHelper.Get("Auth_" + userID);
            if (string.IsNullOrEmpty(myJti)||jti!=myJti)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement); // 显式的声明验证成功
            }
            return Task.CompletedTask;

        }
    }
}
