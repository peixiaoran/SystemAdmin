using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 流程规则实体类
    /// </summary>
    [SugarTable("[Form].[WorkflowRule]")]
    public class WorkflowRuleEntity
    {
        /// <summary>
        /// 规则Id
        /// </summary>
        public long RuleId { get; set; }

        /// <summary>
        /// 表单类别Id
        /// </summary>
        public long FormTypeId { get; set; }

        /// <summary>
        /// 规则名称（中文）
        /// </summary>
        public string RuleNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 规则名称（英文）
        /// </summary>
        /// </summary>
        public string RuleNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 职级Id
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 导向
        /// </summary>
        public string Guidance { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
