using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Common.Enums.FormBusiness
{
    /// <summary>
    /// 签核状态
    /// </summary>
    public enum FormReviewResult
    {
        /// <summary>
        /// 未签核
        /// </summary>
        Unsigned,

        /// <summary>
        /// 审批中
        /// </summary>
        UnderReview,

        /// <summary>
        /// 已签核
        /// </summary>
        Signed,
    }
}
