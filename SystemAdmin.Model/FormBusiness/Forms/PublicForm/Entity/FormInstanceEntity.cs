using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity
{
    /// <summary>
    /// 表单信息实体类
    /// </summary>
    [SugarTable("[Form].[FormInstance]")]
    public class FormInstanceEntity
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
        /// 申请人Id
        /// </summary>
        public long ApplicantUserId { get; set; }

        /// <summary>
        /// 所属分支
        /// </summary>
        public long BranchId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long CurrentStepId { get; set; }

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
