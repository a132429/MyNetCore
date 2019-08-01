using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.EntityFrameworkCore.Models
{
    /// <summary>
    /// 部门表
    /// </summary>
    public  class Department:Entity
    {
        /// <summary>
        /// 父级部门ID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public string OrgLevel { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDel { get; set; }

        /// <summary>
        /// 是否已启用
        /// </summary>
        public int IsAvailable { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 组织编号
        /// </summary>
        public string OrgParentPath { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        public string OrgParentName { get; set; }

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


       
    }
}
