using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.Application.UserApp.Dtos
{
    public class UserInfo
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }
        public int status { get; set; }
        public string telephone { get; set; }
        public string lastLoginIp { get; set; }
        public long lastLoginTime { get; set; }
        public string creatorId { get; set; }
        public long  createTime { get; set; }
        public string merchantCode { get; set; }
        public int deleted { get; set; }
        public string roleId { get; set; }
        public RoleItem role { get; set; }
    }
    public class RoleItem
    {

        public string id { get; set; }
        public string name { get; set; }
        public string describe { get; set; }
        public string status { get; set; }
        public string creatorId { get; set; }
        public long createTime { get; set; }
        public int deleted { get; set; }
        public List<permissions> permissions { get; set; }
    }
    public class permissions
    {
        public string roleId { get; set; }
        public string permissionId { get; set; }
        public string permissionName { get; set; }
        public string actionList { get; set; }
        public string dataAccess { get; set; }
        public List<Item> actions { get; set; }
        public List<Item> actionEntitySet { get; set; }

    }
    public class Item
    {
       public string action { get; set; }
        public string describe { get; set; }
        public bool defaultCheck { get; set; }
    }
         
}
