using SqlSugar;

namespace SystemAdmin.Model.CustMat.CustMatBasicInfo.Queries
{
    /// <summary>
    /// 查询客户信息分页请求参数
    /// </summary>
    public class GetCustomerInfoPage : PageModel
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;
    }
}
