using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto
{
    /// <summary>
    /// 菜单Dto
    /// </summary>
    public class MenuInfoDto
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MenuId { get; set; }

        /// <summary>
        /// 所属模块Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ModuleId { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ParentMenuId { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MenuCode { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 菜单级别
        /// </summary>
        public string MenuType { get; set; } = string.Empty;

        /// <summary>
        /// 菜单级别名称
        /// </summary>
        public string MenuTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        public int IsVisible { get; set; }

        /// <summary>
        /// 对应API路由
        /// </summary>
        public string RoutePath { get; set; } = string.Empty;

        /// <summary>
        /// 前端重定向
        /// </summary>
        public string Redirect { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatedName { get; set; } = string.Empty;

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;
    }
}
