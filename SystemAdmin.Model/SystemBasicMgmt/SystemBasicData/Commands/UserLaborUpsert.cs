namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands
{
    /// <summary>
    /// 职业新增/修改类
    /// </summary>
    public class UserLaborUpsert
    {
        /// <summary>
        /// 职业Id
        /// </summary>
        public string LaborId { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称（中文）
        /// </summary>
        public string LaborNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称（英文）
        /// </summary>
        public string LaborNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职业描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
