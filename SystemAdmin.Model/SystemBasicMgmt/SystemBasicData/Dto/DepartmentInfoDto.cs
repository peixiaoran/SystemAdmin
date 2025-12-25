using SqlSugar;
using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 部门Dto
    /// </summary>
    public class DepartmentInfoDto
    {
        /// <summary>
        /// 部门主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门编码（唯一标识）
        /// </summary>
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称（中文）
        /// </summary>
        public string DepartmentNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称（英文）
        /// </summary>
        public string DepartmentNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 上级部门Id（可为空）
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ParentId { get; set; }

        /// <summary>
        /// 部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 部门级别名称
        /// </summary>
        public string DepartmentLevelName { get; set; } = string.Empty;

        /// <summary>
        /// 部门描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 排序值（默认0）
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 部门固定电话
        /// </summary>
        public string Landline { get; set; } = string.Empty;

        /// <summary>
        /// 部门邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 部门地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true, IsTreeKey = true)]
        public List<DepartmentInfoDto> DepartmentChildList { get; set; } = new List<DepartmentInfoDto>();
    }
}
