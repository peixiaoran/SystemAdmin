using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Common.Enums.FormBusiness
{
    public enum AppointmentType
    {
        /// <summary>
        /// 本
        /// </summary>
        Primary = 1,

        /// <summary>
        /// 兼
        /// </summary
        Concurrent = 2,

        /// <summary>
        /// 代
        /// </summary
        Agent = 3,

        /// <summary>
        /// 兼 - 代
        /// </summary
        ConcurrentAgent = 4,

        /// <summary>
        /// 自动指派 - 本
        /// </summary
        AutoPrimary = 5,

        /// <summary>
        /// 自动指派 - 兼
        /// </summary
        AutoConcurrent = 6,

        /// <summary>
        /// 自动指派 - 代
        /// </summary
        AutoAgent = 7,

        /// <summary>
        /// 自动指派 - 兼 - 代
        /// </summary
        AutoConcurrentAgent = 8,
    }
}
