using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Dto
{
    /// <summary>
    /// 员工表单绑定表单Dto
    /// </summary>
    public class UserFormBindViewTreeDto
    {
        /// <summary>
        /// 所属组别Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ParentId { get; set; }

        /// <summary>
        /// 表单绑定Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormGroupTypeId { get; set; }

        /// <summary>
        /// 表单绑定名称
        /// </summary>
        public string FormGroupTypeName { get; set; } = default!;

        /// <summary>
        /// 表单绑定描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用（1、是 0、否）
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 是否绑定
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 表单类型子集合
        /// </summary>
        public List<UserFormBindViewTreeDto> FormTypeChildren { get; set; } = new List<UserFormBindViewTreeDto>();
    }
}
