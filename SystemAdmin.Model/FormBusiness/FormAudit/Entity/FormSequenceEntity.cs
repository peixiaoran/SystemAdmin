using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormAudit.Entity
{
    /// <summary>
    /// 表单年月单号计数实体类
    /// </summary>
    [SugarTable("[Form].[FormSequence]")]
    public class FormSequenceEntity
    {
        /// <summary>
        /// 表单类型Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long FormTypeId { get; set; }

        /// <summary>
        /// 年月（yyyyMM）
        /// </summary>
        public string Ym { get; set; } = string.Empty;

        /// <summary>
        /// 当前表单数量
        /// </summary>
        public int Total { get; set; }

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
