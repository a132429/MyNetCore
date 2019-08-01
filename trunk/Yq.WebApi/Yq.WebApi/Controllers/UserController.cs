using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yq.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yq.Application.UserApp;
using Yq.Application.RoleApp;
using Yq.Application.MenuApp;
using Yq.Utility;
using Yq.Application.UserApp.Dtos;

namespace Yq.WebApi.Controllers
{
    //[Authorize("Bearer")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller// ControllerBase
    {
        private readonly IUserAppService _service;
        private readonly IRoleAppService _roleService;
        private readonly IMenuAppService _menuService;
        public UserController(IUserAppService service, IRoleAppService roleService, IMenuAppService menuService)
        {
            _service = service;
            _roleService = roleService;
            _menuService = menuService;
        }


        [HttpGet]
        public ActionResult Info()
        {
            UserInfo info = new UserInfo();
            info.id = Guid.NewGuid();
            info.name = "天野远子";
            info.username = "admin";
            info.password ="";
            info.avatar = "/avatar2.jpg";
            info.status =1;  
            info.telephone = "";
            info.lastLoginIp = "27.154.74.117";
            info.lastLoginTime = 1534837621348;
            info.creatorId = "admin";
            info.createTime = 1497160610259;
            info.merchantCode = "TLif2btpzg079h15bk";
            info.deleted = 0;
            info.roleId = "admin";
            RoleItem role = new RoleItem();
            role.id = "admin";
            role.name = "管理员";
            role.describe = "拥有所有权限";
            role.creatorId = "system";
            role.createTime = 1497160610259;
            role.deleted =0;
            List<Item> item = new List<Item>();
            item.Add(new Item { action="add",describe= "新增",defaultCheck=false });
            item.Add(new Item { action = "import", describe = "导入", defaultCheck = false });
            item.Add(new Item { action = "get", describe = "详情", defaultCheck = false });
            item.Add(new Item { action = "update", describe = "修改", defaultCheck = false });
            item.Add(new Item { action = "delete", describe = "删除", defaultCheck = false });
            item.Add(new Item { action = "export", describe = "导出", defaultCheck = false });
            List<Item> item1 = new List<Item>();
            item1.Add(new Item { action = "add", describe = "新增", defaultCheck = false });
            item1.Add(new Item { action = "import", describe = "导入", defaultCheck = false });
            item1.Add(new Item { action = "get", describe = "详情", defaultCheck = false });
            item1.Add(new Item { action = "update", describe = "修改", defaultCheck = false });
            item1.Add(new Item { action = "delete", describe = "删除", defaultCheck = false });
            item1.Add(new Item { action = "export", describe = "导出", defaultCheck = false });
            List<permissions> per = new List<permissions>();
            per.Add(new permissions { roleId = "admin", permissionId = "dashboard", permissionName = "仪表盘", actions = item, actionEntitySet = item1 });
            per.Add(new permissions { roleId = "admin",permissionId = "user", permissionName = "用户管理",actions = item,actionEntitySet = item1});
            per.Add(new permissions { roleId = "admin", permissionId = "support", permissionName = "超级模块", actions = item, actionEntitySet = item1 });

            role.permissions = per;
            info.role = role;
            
            return new JsonResult(new MyJsonResult { Code ="200", Result =info});
        }
    }
}