using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormAudit.Entity
{
    /// <summary>
    /// 表单年月单号计数实体类
    /// </summary>
    [SugarTable("[Form].[FormCounting]")]
    public class FormCountingEntity
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 年月（yyyyMM）
        /// </summary>
        public string YM { get; set; } = string.Empty;

        /// <summary>
        /// 当前表单数量
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 草稿数量
        /// </summary>
        public int Draft { get; set; }

        /// <summary>
        /// 提交数量
        /// </summary>
        public int Submitted { get; set; }

        /// <summary>
        /// 送审数量
        /// </summary>
        public int Approved { get; set; }

        /// <summary>
        /// 驳回数量
        /// </summary>
        public int Rejected { get; set; }

        /// <summary>
        /// 作废数量
        /// </summary>
        public int Canceled { get; set; }

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
