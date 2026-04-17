namespace SystemAdmin.Common.Enums.FormBusiness
{
    /// <summary>
    /// 签核身份
    /// </summary>
    public enum AppointmentType
    {
        /// <summary>
        /// 实
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
        /// </summary>
        ConcurrentAgent = 4,

        /// <summary>
        /// 自动指派 - 实
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

        /// <summary>
        /// 职级覆盖跳过
        /// </summary
        HierarchySkipped = 9,
    }
}
