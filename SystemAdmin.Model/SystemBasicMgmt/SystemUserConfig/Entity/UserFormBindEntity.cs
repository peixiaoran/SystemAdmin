using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity
{
    /// <summary>
    /// 员工表单绑定实体表
    /// </summary>
    [SugarTable("[Basic].[UserFormBind]")]
    public class UserFormBindEntity
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 表单组别类型Id
        /// </summary>
        public long FormGroupTypeId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
