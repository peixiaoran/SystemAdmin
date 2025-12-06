using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 部门级别Dto
    /// </summary>
    public class DepartmentLevelDto
    {
        /// <summary>
        /// 部门级别ID
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 部门级别编号
        /// </summary>
        public string DepartmentLevelCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门级别名称（中文）
        /// </summary>
        public string DepartmentLevelNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 部门级别名称（英文）
        /// </summary>
        public string DepartmentLevelNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 排序值（默认0）
        /// </summary>
        public int SortOrder { get; set; }
    }
}
