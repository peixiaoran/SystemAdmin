using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Model.FormBusiness.FormWorkflow.Queries
{
    /// <summary>
    /// 查询步骤信息实体请求参数
    /// </summary>
    public class GetFormStepEntity
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; } = string.Empty;
    }
}
