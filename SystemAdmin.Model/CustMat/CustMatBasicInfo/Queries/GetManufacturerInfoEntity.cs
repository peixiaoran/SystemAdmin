namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries
{
    /// <summary>
    /// 查询厂商信息实体请求参数
    /// </summary>
    public class GetManufacturerInfoEntity
    {
        /// <summary>
        /// 厂商
        /// </summary>
        public string ManufacturerId { get; set; } = string.Empty;
    }
}
