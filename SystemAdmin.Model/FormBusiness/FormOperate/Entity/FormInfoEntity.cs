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
        /// 表单简短描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 重要程度
        /// </summary>
        public int ImportanceCode { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        public int FormStatus { get; set; }

        /// <summary>
        /// 开单时间
        /// </summary>
        public string FormOpenTime { get; set; } = string.Empty;

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
