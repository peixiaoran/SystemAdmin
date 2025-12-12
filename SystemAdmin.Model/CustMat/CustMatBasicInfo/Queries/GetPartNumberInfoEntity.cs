namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries
{
    /// <summary>
    /// 查询料号信息实体请求参数
    /// </summary>
    public class GetPartNumberInfoEntity
    {
        /// <summary>
        /// 料号Id
        /// </summary>
        public string PartNumberId { get; set; } = string.Empty;
    }
}
