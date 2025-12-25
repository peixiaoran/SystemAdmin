using SqlSugar;
using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto
{
    /// <summary>
    /// 部门下拉框Dto
    /// </summary>
    public class DepartmentDropDto
    {
        /// <summary>
        /// 部门主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 上级部门Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ParentId { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        [SugarColumn(IsIgnore = true, IsTreeKey = true)]
        public List<DepartmentDropDto> DepartmentChildList { get; set; } = new List<DepartmentDropDto>();
    }
}
