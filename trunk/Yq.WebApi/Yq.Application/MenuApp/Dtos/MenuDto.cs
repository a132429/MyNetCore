using System;
using System.Collections.Generic;
using System.Text;

namespace Yq.Application.MenuApp.Dtos
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 类型：0、导航菜单 1、操作按钮 2、列表按钮
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单备注
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
