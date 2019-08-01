using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.EntityFrameworkCore.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public  class Role:Entity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDel { get; set; }

        /// <summary>
        /// 是否已启用
        /// </summary>
        public int IsAvailable { get; set; }

        public int SortNo { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public Guid UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
