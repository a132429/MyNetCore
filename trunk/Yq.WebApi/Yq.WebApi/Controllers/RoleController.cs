using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yq.Application.MenuApp;
using Yq.Application.RoleApp;
using Yq.WebApi.Model;

namespace Yq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    { 
        private readonly IRoleAppService _roleService;
        private readonly IMenuAppService _menuService;
        public RoleController(IRoleAppService roleService, IMenuAppService menuService)
        { 
            _roleService = roleService;
            _menuService = menuService;
        }
        [HttpPost]
        public async Task<ActionResult> GetRoleList()
        {
           
            return new JsonResult(new MyJsonResult { Code = State.Error.ToString(), Message = "用户输入不合法!" });
        }
    }
}