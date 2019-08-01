using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yq.Application.RoleApp.Dtos;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Application.RoleApp
{
    public class RoleAppService:IRoleAppService
    {
        private readonly IRoleRepository _repository;
        public RoleAppService(IRoleRepository repository)
        {
            _repository = repository;
        }
        //public bool Update(Role role)
        //{
        //    _repository.Update(role);
        //    return _repository.Save();
        //}
        //public async Task<RoleDto> GetRoleList()
        //{
        //  string sql = "select Code,RoleName from Role where isdel=0";
        //  var role=await _repository.SqlQueryAsync("",new { });
        //   return Mapper.Map<RoleDto>(role); 
        //}
    }
}
