using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.Application.UserApp.Dtos
{
    public class UserAuth
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string telephone { get; set; }
        public string lastLoginIp { get; set; }
        public string lastLoginTime { get; set; }
        public string creatorId { get; set; }
        public string createTime { get; set; }
        public int deleted { get; set; }
        public string roleId { get; set; }
        public string lang { get; set; }
        public string token { get; set; }
    }
}
