using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yq.Application.RoleApp;
using Yq.Application.UserApp;
using Yq.Application.UserApp.Dtos;
using Yq.Utility;
using Yq.WebApi.Model;
using Yq.WebApi.Model.User;

namespace Yq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _service;
        private readonly IRoleAppService _roleService;
        private readonly JwtSettings _jwt;
        public AuthController(IUserAppService service, IRoleAppService roleService, IOptions<JwtSettings> jwt)
        {
            _service = service;
            _roleService = roleService;
            _jwt = jwt.Value;
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)//判断是否合法
            {
                var user = await _service.GetUserDto(model.Username.Trim());
                #region 判断用户数据
                if (user == null || user.IsDel ==1)
                {
                    return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "用户不存在!" });
                }
                if (EncryptUtils.MD5Encrypt(user.Password) != model.Password.Trim())
                {
                    return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "密码不正确!" });
                }
                if (user.IsAvailable == 0)
                {
                    return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "账号被禁用!" });
                }
                #endregion
                #region JWT
                DateTime dt = new DateTime();
                string jti = EncryptUtils.MD5Encrypt(user.Id.ToString() + dt.ToString("yyyyMMddHHmmss"));
                var claim = new Claim[]{
                        new Claim(ClaimTypes.Name,user.FullName),
                        new Claim("guid",user.Id.ToString()),
                        new Claim("loginName",user.Employee),
                        new Claim("jti",jti),
                        new Claim("userType","")// JsonHelper.ObjectToJson(user.UserRole.Select(a=>a.RoleId).ToList()))
                     };
                //对称秘钥
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
                //签名证书(秘钥，加密算法)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                var getoken = new JwtSecurityToken(_jwt.Issuer, _jwt.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);
                var token = new JwtSecurityTokenHandler().WriteToken(getoken);

                #endregion
                #region 修改用户信息
                if (user.FirstTime.HasValue)
                {
                    user.LastLoginTime = dt;
                }
                else
                {
                    user.FirstTime = dt;
                    user.LastLoginTime = dt;
                }
                user.token = token;
                bool s = await _service.UpdateAsync(user);
                if (!s)
                {
                    return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "修改用户信息失败!" });
                }
                #endregion 
                //写入用户jti到缓存=2小时，如果找不着就掉线
                RedisHelper.Set("Auth_" + user.Id.ToString(), jti, 60 * 60 * 2);
                UserAuth auth = new UserAuth();
                auth.id = Guid.NewGuid();
                auth.username = "张三";
                auth.password = "";
                auth.avatar = "https://gw.alipayobjects.com/zos/rmsportal/jZUIxmJycoymBprLOUbT.png";
                auth.telephone = "";
                auth.lastLoginIp = "27.154.74.117";
                auth.lastLoginTime = "1534837621348";
                auth.creatorId = "admin";
                auth.createTime = "1497160610259";
                auth.deleted = 0;
                auth.roleId = "admin";
                auth.lang = "zh-CN";
                auth.token = token;
                return new JsonResult(new MyJsonResult { Code = State.Ok.ToString(), Result = auth });
            }
            return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "用户输入不合法!" });
        }
    }
}