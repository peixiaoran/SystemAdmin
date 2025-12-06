using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity
{
    /// <summary>
    /// 表单表单控件实体
    /// </summary>
    [SugarTable("[Form].[ControlInfo]")]
    public class ControlInfoEntity
    {
        /// <summary>
        /// 表单控件编码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public string ControlCode { get; set; } = string.Empty;

        /// <summary>
        /// 表单控件名称
        /// </summary>
        public string ControlName { get; set; } = string.Empty;

        /// <summary>
        /// 表单控件描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

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
