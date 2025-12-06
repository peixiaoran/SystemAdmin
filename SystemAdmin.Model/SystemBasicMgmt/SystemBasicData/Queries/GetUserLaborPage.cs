using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries
{
    /// <summary>
    /// 查询职业分页请求参数
    /// </summary>
    public class GetUserLaborPage : PageModel
    {
        /// <summary>
        /// 职业编号
        /// </summary>
        public string LaborNo { get; set; } = string.Empty;

        /// <summary>
        /// 职业名称
        /// </summary>
        public string LaborName { get; set; } = string.Empty;
    }
}
