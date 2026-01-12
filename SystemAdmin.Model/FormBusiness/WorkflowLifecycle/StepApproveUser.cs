using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Model.FormBusiness.WorkflowLifecycle
{
    public class StepApproveUser
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
