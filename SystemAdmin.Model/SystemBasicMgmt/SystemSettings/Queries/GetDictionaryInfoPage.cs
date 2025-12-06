using SqlSugar;

namespace SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries
{
    /// <summary>
    /// 查询字典分页请求参数
    /// </summary>
    public class GetDictionaryInfoPage : PageModel
    {
        /// <summary>
        /// 所属模块Id
        /// </summary>
        public string ModuleId { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称
        /// </summary>
        public string DicName { get; set; } = string.Empty;

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DicType { get; set; } = string.Empty;
    }
}
