using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yq.Application.MenuApp.Dtos;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Application.MenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ActionResult GetMenuDto(int PageNo=0,int PageSize=10)
        { 
            ActionResult result = new ActionResult();
            result.PageNo = PageNo;
            result.PageSize = PageSize;

            string sql = @"
            select sql_calc_found_rows * from menus  where IsAvailable=1 and IsDel=0 order by  SortNo limit @PageNo,@PageSize;
            select found_rows() as allcount; ";
            try
            {
                using (var multi = _menuRepository.QueryMultiple(sql,new { PageNo=PageNo,PageSize=PageSize}))
                {
                    if (!multi.IsConsumed)
                    {
                        result.Data= multi.Read<Menu>();
                        result.TotalCount= multi.Read<int>().First();
                        int total = result.TotalCount / PageSize > 0 ? result.TotalCount / PageSize + 1: result.TotalCount / PageSize;
                        result.TotalPage = total;
                    }
                }
                return result;
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
