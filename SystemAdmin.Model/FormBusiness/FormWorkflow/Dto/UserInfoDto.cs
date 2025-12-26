using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Dto
{
    public class UserInfoDto
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string UserNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职级名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称
        /// </summary>
        public string LaborName { get; set; } = string.Empty;

        /// <summary>
        /// 国籍名称
        /// </summary>
        public string NationalityName { get; set; } = string.Empty;

        /// <summary>
        /// 是否签核描述
        /// </summary>
        [JsonConverter(typeof(IntToStringConverter))]
        public int IsApproval { get; set; }

        /// <summary>
        /// 是否签核描述
        /// </summary>
        public string IsApprovalName { get; set; } = string.Empty;
    }
}
