using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormOperate.Entity
{
    /// <summary>
    /// 表单信息实体类
    /// </summary>
    [SugarTable("[Form].[FormInfo]")]
    public class FormInfoEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long FormId { get; set; }

        /// <summary>
        /// 表单类别Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 表单编号
        /// </summary>
        public string FormNo { get; set; } = string.Empty;

        /// <summary>
        /// 表单状态
        /// </summary>
        public string FormStatus { get; set; } = string.Empty;

        /// <summary>
        /// 上一步骤Id
        /// </summary>
        public long? LastStepId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long NowStepId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
