namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Commands
{
    /// <summary>
    /// 料号信息新增/修改类
    /// </summary>
    public class PartNumberInfoUpsert
    {
        /// <summary>
        /// 料号Id
        /// </summary>
        public string PartNumberId { get; set; } = string.Empty;

        /// <summary>
        /// 所属厂商Id
        /// </summary>
        public long ManufacturerId { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string PartNumberNo { get; set; } = string.Empty;

        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 规格
        /// </summary>
        public string Specifications { get; set; } = string.Empty;
    }
}
