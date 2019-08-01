using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.EntityFrameworkCore.Models
{
    public  class User : Entity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Employee { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 其它名称
        /// </summary>
        public string OtherName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public Guid UpdateUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 是否已启用
        /// </summary>
        public int IsAvailable { get; set; }
        /// <summary>
        ///token（账号+时间）
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstTime { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<UserRole> UserRole{ get;set;}
    }
}
