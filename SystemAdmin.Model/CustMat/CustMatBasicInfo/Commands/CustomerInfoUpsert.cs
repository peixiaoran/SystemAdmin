namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Commands
{
    /// <summary>
    /// 客户信息新增/修改类
    /// </summary>
    public class CustomerInfoUpsert
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; } = string.Empty;

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
    }
}
