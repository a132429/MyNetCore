using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yq.WebApi.Model.User
{
    public class LoginViewModel
    {
        //用户名
        [Required(ErrorMessage = "用户名不能为空")]
        public string Username { get; set; }
        //密码
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
