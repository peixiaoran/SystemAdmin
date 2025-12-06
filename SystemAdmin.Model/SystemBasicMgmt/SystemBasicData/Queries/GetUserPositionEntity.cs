namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries
{
    /// <summary>
    /// 查询职级实体请求参数
    /// </summary>
    public class GetUserPositionEntity
    {
        /// <summary>
        /// 职级Id
        /// </summary>
        public string PositionId { get; set; } = string.Empty;
    }
}
