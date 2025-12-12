using SqlSugar;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Entity
{
    /// <summary>
    /// 客户信息实体类
    /// </summary>
    [SugarTable("[CustMat].[CustomerInfo]")]
    public class CustomerInfoEntity
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "Primary Key")]
        public long CustomerId { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称（中文）
        /// </summary>
        public string CustomerNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称（英文）
        /// </summary>
        public string CustomerNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 客户描述
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
