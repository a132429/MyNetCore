using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yq.Application.MenuApp;
using Yq.WebApi.Model;

namespace Yq.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenuAppService _menuRepository;
        public MenuController(IMenuAppService menuRepository)
        {
            _menuRepository = menuRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetMenuList(int PageNo = 0, int PageSize = 10)
        {
           var list= _menuRepository.GetMenuDto((PageNo - 1) * PageSize, PageNo* PageSize);
           return new JsonResult(new MyJsonResult { Code = "200", Result=list});
        }
        [HttpPost]
        public async Task<ActionResult> GetMenuLists(int PageNo = 0, int PageSize = 10)
        {
            var list = _menuRepository.GetMenuDto((PageNo - 1) * PageSize, PageNo * PageSize);
            return new JsonResult(new MyJsonResult { Code = "200", Result = list });
        }

    }
}