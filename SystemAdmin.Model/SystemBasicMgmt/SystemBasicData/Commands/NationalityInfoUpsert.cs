namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands
{
    /// <summary>
    /// 国籍新增/修改类
    /// </summary>
    public class NationalityInfoUpsert
    {
        /// <summary>
        /// 国籍Id
        /// </summary>
        public string NationId { get; set; } = string.Empty;

        /// <summary>
        /// 国籍名称（中文）
        /// </summary>
        public string NationNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 国籍名称（英文）
        /// </summary>
        public string NationNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
