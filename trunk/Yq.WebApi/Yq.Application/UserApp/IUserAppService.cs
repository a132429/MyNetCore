using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Yq.Application.UserApp.Dtos;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Application.UserApp
{
    public interface  IUserAppService  
    {
        //User CheckUser(string userName, string password);
        Task<User> GetUserDto(string userName);
        Task<bool> UpdateAsync(User user);
        //IEnumerable<User> GetUser(string sql, object param = null);
        //bool Update(User user);
        //UserDto GetUserDto(string userName, string password);
        //Task<bool> CreateUserAsync(User user);
    }
}
