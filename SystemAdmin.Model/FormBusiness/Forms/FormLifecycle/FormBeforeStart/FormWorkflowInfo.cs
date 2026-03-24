using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Model.FormBusiness.Forms.FormLifecycle.FormBeforeStart
{
    /// <summary>
    /// 表单完整流程Dto
    /// </summary>
    public class FormWorkflowInfo
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        public long NowStepId { get; set; }

        /// <summary>
        /// 驳回记录
        /// </summary>
        public List<RejectLogList> RejectLogList { get; set; } = new List<RejectLogList>();

        /// <summary>
        /// 流程签核人员列表
        /// </summary>
        public List<WorkflowApproveUser> WorkflowApproveUser { get; set; } = new List<WorkflowApproveUser>();
    }
}
