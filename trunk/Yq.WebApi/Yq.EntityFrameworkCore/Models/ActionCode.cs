using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.EntityFrameworkCore.Models
{
    /// <summary>
    /// 操作表
    /// </summary>
    public class ActionCode : Entity
    {
        /// <summary>
        /// 请求
        /// </summary>
        public string Action { get; set; }
        public bool DefaultCheck { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 是否已启用
        /// </summary>
        public int IsAvailable { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 添加时间
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
