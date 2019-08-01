using System;
using System.Collections.Generic;
using System.Text;
using Yq.Application.MenuApp.Dtos;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Application.MenuApp
{
    public interface IMenuAppService
    {
        ActionResult GetMenuDto(int PageNo = 0, int PageSize = 10);
    }
}
