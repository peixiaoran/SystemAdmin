using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 部门级别下拉框Dto
    /// </summary>
    public class DepartmentLevelDropDto
    {
        /// <summary>
        /// 部门级别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentLevelId { get; set; }

        /// <summary>
        /// 部门级别名称
        /// </summary>
        public string DepartmentLevelName { get; set; } = string.Empty;
    }
}
