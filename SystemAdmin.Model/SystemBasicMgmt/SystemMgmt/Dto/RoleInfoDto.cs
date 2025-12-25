using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto
{
    /// <summary>
    /// 角色Dto
    /// </summary>
    public class RoleInfoDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoleId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
