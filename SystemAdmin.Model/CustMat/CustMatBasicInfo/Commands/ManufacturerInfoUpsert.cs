namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Commands
{
    /// <summary>
    /// 厂商信息新增/修改类
    /// </summary>
    public class ManufacturerInfoUpsert
    {
        /// <summary>
        /// 厂商Id
        /// </summary>
        public string ManufacturerId { get; set; } = string.Empty;

        /// <summary>
        /// 厂商编码
        /// </summary>
        public string ManufacturerCode { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（中文）
        /// </summary>
        public string ManufacturerNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称（英文）
        /// </summary>
        public string ManufacturerNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 厂商邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 厂商传真
        /// </summary>
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// 厂商描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
