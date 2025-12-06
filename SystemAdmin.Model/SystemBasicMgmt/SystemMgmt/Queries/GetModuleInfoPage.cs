using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Queries
{
   /// <summary>
   /// 查询模块分页请求参数
   /// </summary>
    public class GetModuleInfoPage : PageModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;
    }
}
