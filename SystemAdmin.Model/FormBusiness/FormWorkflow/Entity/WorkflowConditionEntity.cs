using SqlSugar;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Entity
{
    /// <summary>
    /// 流程审批条件实体类
    /// </summary>
    [SugarTable("[Form].[WorkflowCondition]")]
    public class WorkflowConditionEntity
    {
        /// <summary>
        /// 审批条件Id
        /// </summary>
        public long ConditionId { get; set; }

        /// <summary>
        /// 流程条件名称（中文）
        /// </summary>
        public string ConditionNameCn { get; set; } = string.Empty;

        /// <summary>
        /// 流程条件名称（英文）
        /// </summary>
        public string ConditionNameEn { get; set; } = string.Empty;

        /// <summary>
        /// 代码标记
        /// </summary>
        public string HandlerKey { get; set; } = string.Empty;

        /// <summary>
        /// 逻辑说明
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
