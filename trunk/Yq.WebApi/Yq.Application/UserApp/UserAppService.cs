using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Yq.Application.UserApp.Dtos;
using Yq.EntityFrameworkCore.Models;
using Yq.Domain.IRepository;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;

namespace Yq.Application.UserApp
{
    public class UserAppService :IUserAppService
    {
        //用户管理仓储接口
        private  readonly  IUserRepository  _repository;
        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public UserAppService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }
        public async Task<User> GetUserDto(string userName)
        {
            return await _repository.GetFirstOrDefaultAsync(a => a.Employee == userName);
            ///return Mapper.Map<UserDto>(user); 
        }
        public async Task<bool> UpdateAsync(User user)
        {
            _repository.Update(user);
            return await _repository.SaveAsync();
        }


        //public User CheckUser(string userName, string password)
        //{
        //    return _repository.CheckUser(userName, password);
        //}
        //public IEnumerable<User> GetUser(string sql, object param = null)
        //{
        //    return _repository.SqlQuery<User>(sql, param);
        //}
        //public bool Update(User user)
        //{
        //    _repository.Update(user);
        //    return _repository.Save();
        //}
        //public UserDto GetUserDto(string userName, string password)
        //{
        //    return Mapper.Map<UserDto>(_repository.CheckUser(userName, password));
        //}
        //public async Task<bool> CreateUserAsync(User user)
        //{
        //    await _repository.InsertAsync(user);
        //    return true;
        //} 
    }
}
