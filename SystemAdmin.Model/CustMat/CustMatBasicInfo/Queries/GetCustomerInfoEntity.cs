namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries
{
    /// <summary>
    /// 查询客户信息实体请求参数
    /// </summary>
    public class GetCustomerInfoEntity
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; } = string.Empty;
    }
}
