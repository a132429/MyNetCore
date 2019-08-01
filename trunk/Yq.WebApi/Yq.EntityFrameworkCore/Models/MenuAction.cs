using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.EntityFrameworkCore.Models
{
    public class MenuAction : Entity
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; } 

        /// <summary>
        /// 请求ID
        /// </summary>
        public Guid  ActionID{ get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
