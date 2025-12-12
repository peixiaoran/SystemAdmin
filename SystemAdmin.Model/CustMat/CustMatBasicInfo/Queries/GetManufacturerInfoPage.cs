using SqlSugar;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries
{
    /// <summary>
    /// 查询厂商信息分页请求参数
    /// </summary>
    public class GetManufacturerInfoPage : PageModel
    {
        /// <summary>
        /// 厂商编码
        /// </summary>
        public string ManufacturerCode { get; set; } = string.Empty;

        /// <summary>
        /// 厂商名称
        /// </summary>
        public string ManufacturerName { get; set; } = string.Empty;
    }
}
