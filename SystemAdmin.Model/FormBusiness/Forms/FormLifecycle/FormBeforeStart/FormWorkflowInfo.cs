using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using SystemAdmin.Model.ModelHelper.ModelConverter;

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
        [JsonConverter(typeof(LongToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 当前步骤Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
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
