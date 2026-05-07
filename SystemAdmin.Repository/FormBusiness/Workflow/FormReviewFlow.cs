using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.FormReviewFlow;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class FormReviewFlow
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public FormReviewFlow(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _localization = localization;
            _lang = lang;
        }

        /// <summary>
        /// 查询表单审批流程
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<FormReview> GetFullReviewFlow(long formId)
        {
            var formReview = new FormReview();
            var stepReviewList = new List<StepReview>();
            formReview.FormId = formId;

            var formDetail = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .InnerJoin<FormTypeEntity>((instance, formtype) => instance.FormTypeId == formtype.FormTypeId)
                                      .InnerJoin<UserInfoEntity>((instance, formtype, user) => instance.ApplicantUserId == user.UserId)
                                      .InnerJoin<DepartmentInfoEntity>((instance, formtype, user, dept) => user.DepartmentId == dept.DepartmentId)
                                      .InnerJoin<DepartmentLevelEntity>((instance, formtype, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                      .InnerJoin<PositionInfoEntity>((instance, formtype, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                      .Where((instance, formtype, user, dept, deptlevel, position) => instance.FormId == formId)
                                      .Select((instance, formtype, user, dept, deptlevel, position) => new ApplicantFormDetail
                                      {
                                          FormId = instance.FormId,
                                          FormTypeId = instance.FormTypeId,
                                          RuleId = instance.RuleId,
                                          CurrentStepId = instance.CurrentStepId,
                                          ApplicantUserId = user.UserId,
                                          ApplicantDeptId = dept.DepartmentId,
                                          DeptLevelSort = deptlevel.SortOrder,
                                          PositionSort = position.SortOrder
                                      }).FirstAsync();

            // 申请人上级部门列表（包含申请人所在部门）
            var applicantDept = await _db.Queryable<DepartmentInfoEntity>()
                                         .With(SqlWith.NoLock)
                                         .ToParentListAsync(dept => dept.ParentId, formDetail.ApplicantDeptId);
            // 所属规则的初始步骤
            var ruleStep = await _db.Queryable<WorkflowRuleStepEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(rule => rule.RuleId == formDetail.RuleId && rule.SortOrder == 1)
                                    .FirstAsync();
            var currentStepId = ruleStep.CurrentStepId;
            while (currentStepId != 0)
            {
                var stepReview = new StepReview();
                var userReview = new List<UserReview>();

                var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == currentStepId)
                                        .Select(step => new
                                        {
                                            StepName = _lang.Locale == "zh-CN"
                                                       ? step.StepNameCn
                                                       : step.StepNameEn,
                                            step.IsStartStep,
                                            step.Assignment,
                                            step.ReviewMode,
                                        }).FirstAsync();

                stepReview.StepId = currentStepId;
                stepReview.StepName = stepInfo.StepName;
                if (stepInfo.IsStartStep == 1)
                {
                    userReview = await GetStartReviewUser(formDetail.ApplicantUserId);
                }
                else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
                {
                    var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(step => step.StepId == currentStepId)
                                           .FirstAsync();

                    // 查询部门级别信息和职位信息
                    var deptInfo = await _db.Queryable<DepartmentLevelEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(dept => dept.DepartmentLevelId == orgInfo.DeptLeaveId)
                                            .FirstAsync();
                    var posInfo = await _db.Queryable<PositionInfoEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(pos => pos.PositionId == orgInfo.PositionId)
                                           .FirstAsync();
                    if (formDetail.DeptLevelSort < deptInfo.SortOrder || formDetail.PositionSort <= posInfo.SortOrder)
                    {
                        stepReview.Skip = 1;
                    }
                    else
                    {
                        userReview = await GetOrgReviewUser(applicantDept, orgInfo.DeptLeaveId, orgInfo.PositionId, stepInfo.ReviewMode);
                    }
                }
                else if (stepInfo.Assignment == Assignment.DeptUser.ToEnumString())
                {
                    var deptUserInfo = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(step => step.StepId == currentStepId)
                                                .FirstAsync();
                    userReview = await GetDeptUserReviewUser(deptUserInfo.DepartmentId, deptUserInfo.PositionId, stepInfo.ReviewMode);
                }
                else if (stepInfo.Assignment == Assignment.User.ToEnumString())
                {
                    var userInfo = await _db.Queryable<WorkflowStepUserEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(step => step.StepId == currentStepId)
                                            .FirstAsync();
                    userReview = await GetUserReviewUser(userInfo.UserId, stepInfo.ReviewMode);
                }

                stepReview.stepReviewUser.AddRange(userReview);
                stepReviewList.Add(stepReview);

                currentStepId = await _db.Queryable<WorkflowRuleStepEntity>()
                                         .With(SqlWith.NoLock)
                                         .Where(rule => rule.RuleId == formDetail.RuleId && rule.CurrentStepId == currentStepId)
                                         .Select(rule => rule.NextStepId)
                                         .FirstAsync();
            }

            // 获取审批结果
            formReview.stepReviewFlowList = await GetFormReviewResult(formId, formDetail.CurrentStepId, stepReviewList);
            formReview.RejectCount = await GetRejectCount(formId);
            return formReview;
        }

        #region 查询完整流程审核人员

        /// <summary>
        /// 查询起始步骤审核人员
        /// </summary>
        /// <param name="applicantUserId"></param>
        /// <returns></returns>
        public async Task<List<UserReview>> GetStartReviewUser(long applicantUserId)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            #region SQL

            string sql = $@"SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @Agent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @Actual
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                                    ELSE @Actual
                                END AS AppointmentTypeCode
                            FROM Basic.UserInfo users
                            LEFT JOIN Basic.UserAgent agent
                                   ON users.UserId = agent.SubstituteUserId
                                  AND agent.StartTime <= @Now
                                  AND agent.EndTime >= @Now
                            LEFT JOIN Basic.UserInfo agentusers
                                   ON agent.AgentUserId = agentusers.UserId
                            WHERE users.UserId = @ApplicantUserId";

            var parameters = new[]
            {
                new SugarParameter("@ApplicantUserId", applicantUserId),
                new SugarParameter("@Now", now),

                new SugarParameter("@Actual", actual),
                new SugarParameter("@Agent", agent),
            };

            #endregion

            var result = await _db.Ado.SqlQueryAsync<UserReview>(sql, parameters);

            return result ?? new List<UserReview>();
        }

        /// <summary>
        /// 查询按照组织架构审核人员
        /// </summary>
        /// <param name="applicantParentDept"></param>
        /// <param name="deptLeaveId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewMode"></param>
        /// <returns></returns>
        public async Task<List<UserReview>> GetOrgReviewUser(List<DepartmentInfoEntity> applicantParentDept, long deptLeaveId, long positionId, string reviewMode)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptLeaveId)
                                     .FirstAsync();
            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == positionId)
                                   .FirstAsync();

            var parentDeptIds = applicantParentDept.Select(dept => dept.DepartmentId).ToList();
            string parentDeptIdsStr = string.Join(",", parentDeptIds);

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                SELECT {topN} 
                    ReviewUserId, 
                    ReviewUserName, 
                    AgentUserId, 
                    AgentUserName, 
                    AppointmentTypeName, 
                    AppointmentTypeCode, 
                    DeptLevelSort, 
                    PositionSort, 
                    HireDate
                FROM (

                    -- 实职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT JOIN Basic.UserAgent agent             ON users.UserId          = agent.SubstituteUserId
                                                                AND agent.StartTime     <= @Now
                                                                AND agent.EndTime       >= @Now
                    LEFT JOIN Basic.UserInfo agentusers         ON agent.AgentUserId     = agentusers.UserId
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsReview    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime partime
                    INNER JOIN Basic.UserInfo users             ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo dept        ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo position      ON partime.PartTimePositionId = position.PositionId
                    LEFT JOIN Basic.UserAgent agent             ON users.UserId               = agent.SubstituteUserId
                                                                AND agent.StartTime          <= @Now
                                                                AND agent.EndTime            >= @Now
                    LEFT JOIN Basic.UserInfo agentusers         ON agent.AgentUserId          = agentusers.UserId
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsReview    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DeptLevelSort", deptlevel.SortOrder),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),

                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }
            else
            {
                // ────────────────────────────────────────────────
                // 第二次：自动指派，先递减职级，同职级下再递减部门职级
                // 每次减 1，最小值为 1，找到即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {       
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                        SELECT {topN}
                            ReviewUserId,
                            ReviewUserName,
                            AgentUserId,
                            AgentUserName,
                            AppointmentTypeName,
                            AppointmentTypeCode,
                            DeptLevelSort,
                            PositionSort,
                            HireDate
                        FROM (

                            -- 实职（自动指派）
                            SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoActual
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON users.PositionId       = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId           = agent.SubstituteUserId
                                                                       AND agent.StartTime      <= @Now
                                                                       AND agent.EndTime        >= @Now
                            LEFT JOIN Basic.UserInfo agentusers        ON agent.AgentUserId      = agentusers.UserId
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol} AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrentAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrent
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserPartTime partime
                            INNER JOIN Basic.UserInfo users            ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo position     ON partime.PartTimePositionId = position.PositionId
                            LEFT JOIN Basic.UserAgent agent            ON users.UserId               = agent.SubstituteUserId
                                                                       AND agent.StartTime          <= @Now
                                                                       AND agent.EndTime            >= @Now
                            LEFT JOIN Basic.UserInfo agentusers        ON agent.AgentUserId          = agentusers.UserId
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),

                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }
            }

            return new List<UserReview>();
        }

        /// <summary>
        /// 查询按照指定部门指定职级审核人员
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewMode"></param>
        /// <returns></returns>
        public async Task<List<UserReview>> GetDeptUserReviewUser(long departmentId, long positionId, string reviewMode)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == positionId)
                                   .FirstAsync();
            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(dept => dept.DepartmentId == departmentId)
                                    .FirstAsync();
            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            // ────────────────────────────────────────────────
            // 第一次：精确匹配，按签核方式返回笔数
            // ────────────────────────────────────────────────

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                SELECT {topN}
                    ReviewUserId, 
                    ReviewUserName, 
                    AgentUserId, 
                    AgentUserName, 
                    AppointmentTypeName, 
                    AppointmentTypeCode,
                    DeptLevelSort, 
                    PositionSort, 
                    HireDate
                FROM (

                    -- 实职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept       ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON users.PositionId        = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId            = agent.SubstituteUserId
                                                               AND agent.StartTime        <= @Now
                                                               AND agent.EndTime          >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId      = agentusers.UserId
                    WHERE dept.DepartmentId  = @DepartmentId
                      AND position.SortOrder = @PositionSort
                      AND users.IsReview   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime          partime
                    INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime           <= @Now
                                                               AND agent.EndTime             >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                    WHERE dept.DepartmentId  = @DepartmentId
                      AND position.SortOrder = @PositionSort
                      AND users.IsReview   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DepartmentId", departmentId),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),

                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }

            else
            {
                // ────────────────────────────────────────────────
                // 自动指派：精确匹配找不到时，先递减职级再递减部门职级
                // 每次减 1，最小值为 1，找到候选人即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                        SELECT {topN}
                            ReviewUserId, 
                            ReviewUserName, 
                            AgentUserId, 
                            AgentUserName, 
                            AppointmentTypeName, 
                            AppointmentTypeCode,
                            DeptLevelSort, 
                            PositionSort, 
                            HireDate
                        FROM (

                            -- 实职（自动指派）
                            SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoActual
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept       ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position   ON users.PositionId        = position.PositionId
                            LEFT  JOIN Basic.UserAgent       agent      ON users.UserId            = agent.SubstituteUserId
                                                                       AND agent.StartTime        <= @Now
                                                                       AND agent.EndTime          >= @Now
                            LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId      = agentusers.UserId
                            WHERE dept.DepartmentId  = @DepartmentId
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrentAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrent
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserPartTime          partime
                            INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                            LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                                       AND agent.StartTime           <= @Now
                                                                       AND agent.EndTime             >= @Now
                            LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                            WHERE dept.DepartmentId  = @DepartmentId
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", departmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),

                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }

                if (exactResult.Any())
                {
                    return exactResult;
                }
            }

            return new List<UserReview>();
        }

        /// <summary>
        /// 查询按照指定人审核人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reviewMode"></param>
        /// <returns></returns>
        public async Task<List<UserReview>> GetUserReviewUser(long userId, string reviewMode)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var userInfo = await _db.Queryable<UserInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(user => user.UserId == userId)
                                    .FirstAsync();

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(position => position.PositionId == userInfo.PositionId)
                                   .FirstAsync();

            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(dept => dept.DepartmentId == userInfo.DepartmentId)
                                    .FirstAsync();

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            // ────────────────────────────────────────────────
            // 第一次：精确匹配指定用户
            // ────────────────────────────────────────────────

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                SELECT {topN}
                    ReviewUserId, 
                    ReviewUserName, 
                    AgentUserId, 
                    AgentUserName, 
                    AppointmentTypeName, 
                    AppointmentTypeCode,
                    DeptLevelSort, 
                    PositionSort, 
                    HireDate
                FROM (

                    -- 主职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept       ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON users.PositionId       = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId           = agent.SubstituteUserId
                                                               AND agent.StartTime       <= @Now
                                                               AND agent.EndTime         >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId     = agentusers.UserId
                    WHERE users.UserId      = @UserId
                      AND users.IsReview  = 1
                      AND users.IsEmployed  = 1
                      AND users.IsFreeze    = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId AS ReviewUserId,
                        {userNameCol} AS ReviewUserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime          partime
                    INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime           <= @Now
                                                               AND agent.EndTime             >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                    WHERE partime.UserId    = @UserId
                      AND users.IsReview  = 1
                      AND users.IsEmployed  = 1
                      AND users.IsFreeze    = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@UserId", userId),

                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }
            else
            {
                // ────────────────────────────────────────────────
                // 自动指派：指定人找不到时，按该用户所属部门
                // 先递减职级，同职级下再递减部门职级
                // 每次减 1，最小值为 1，找到即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<UserReview>($@"
                        SELECT {topN}
                            ReviewUserId, 
                            ReviewUserName, 
                            AgentUserId, 
                            AgentUserName, 
                            AppointmentTypeName, 
                            AppointmentTypeCode,
                            DeptLevelSort, 
                            PositionSort, 
                            HireDate
                        FROM (

                            -- 主职（自动指派）
                            SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoActual
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                    ELSE @AutoActual
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserInfo users
                            INNER JOIN Basic.DepartmentInfo  dept       ON users.DepartmentId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position   ON users.PositionId       = position.PositionId
                            LEFT  JOIN Basic.UserAgent       agent      ON users.UserId           = agent.SubstituteUserId
                                                                       AND agent.StartTime       <= @Now
                                                                       AND agent.EndTime         >= @Now
                            LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId     = agentusers.UserId
                            WHERE dept.DepartmentId  = @DepartmentId
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId AS ReviewUserId,
                                {userNameCol} AS ReviewUserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrentAgent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @AutoConcurrent
                                    )
                                END AS AppointmentTypeName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                    ELSE @AutoConcurrent
                                END AS AppointmentTypeCode,
                                deptlevel.SortOrder AS DeptLevelSort,
                                position.SortOrder  AS PositionSort,
                                users.HireDate      AS HireDate
                            FROM Basic.UserPartTime          partime
                            INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                            INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                            INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                            INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                            LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                                       AND agent.StartTime           <= @Now
                                                                       AND agent.EndTime             >= @Now
                            LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                            WHERE dept.DepartmentId  = @DepartmentId
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsReview    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {autoOrderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", now),
                            new SugarParameter("@DepartmentId", userInfo.DepartmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),

                            new SugarParameter("@AutoActual", autoActual),
                            new SugarParameter("@AutoAgent", autoAgent),
                            new SugarParameter("@AutoConcurrent", autoConcurrent),
                            new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                        });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }
            }

            return new List<UserReview>();
        }

        /// <summary>
        /// 取最后一笔驳回记录之后的有效签核数据
        /// </summary>
        private async Task<List<FormReviewRecordEntity>> GetValidReviewRecords(long formId)
        {
            var reviewRecord = await _db.Queryable<FormReviewRecordEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(record => record.FormId == formId && record.ReviewResult == ReviewResult.Approve.ToEnumString())
                                        .OrderBy(record => record.ReviewDateTime)
                                        .ToListAsync();

            int lastRejectedIndex = -1;
            for (int i = reviewRecord.Count - 1; i >= 0; i--)
            {
                if (reviewRecord[i].ReviewResult == ReviewResult.Reject.ToEnumString())
                {
                    lastRejectedIndex = i;
                    break;
                }
            }

            return lastRejectedIndex >= 0
                    ? reviewRecord.Skip(lastRejectedIndex + 1).ToList()
                    : reviewRecord;
        }

        /// <summary>
        /// 获取表单签核结果，填充每个步骤及每位签核人员的状态
        /// </summary>
        public async Task<List<StepReview>> GetFormReviewResult(long formId, long currentStepId, List<StepReview> reviewFlow)
        {
            var validRecords = await GetValidReviewRecords(formId);

            foreach (var flow in reviewFlow)
            {
                if (flow.Skip == 1)
                {
                    continue;
                }
                else
                {
                    foreach (var user in flow.stepReviewUser)
                    {
                        bool isCurrentStep = currentStepId == flow.StepId;

                        if (!isCurrentStep)
                        {
                            // 步骤不匹配，检查历史记录是否有签核
                            bool hasSigned = validRecords.Any(record =>
                                record.StepId == flow.StepId &&
                                (record.OperationUserId == user.ReviewUserId || record.OperationUserId == user.AgentUserId));

                            user.Result = hasSigned
                                ? FormReviewResult.Approve.ToEnumString()
                                : FormReviewResult.Unsigned.ToEnumString();
                        }
                        else
                        {
                            // 步骤匹配，再看该用户是否已签核
                            bool hasSigned = validRecords.Any(record =>
                                record.StepId == flow.StepId &&
                                (record.OperationUserId == user.ReviewUserId || record.OperationUserId == user.AgentUserId));

                            user.Result = hasSigned
                                ? FormReviewResult.Approve.ToEnumString()
                                : FormReviewResult.UnderReview.ToEnumString();
                        }
                    }
                }
            }
            return reviewFlow;
        }

        /// <summary>
        /// 查询表单总计驳回次数
        /// </summary>
        private async Task<int> GetRejectCount(long formId)
        {
            return await _db.Queryable<FormReviewRecordEntity>()
                            .With(SqlWith.NoLock)
                            .Where(record => record.FormId == formId && record.ReviewResult == ReviewResult.Reject.ToEnumString())
                            .CountAsync();
        }

        #endregion


        /// <summary>
        /// 查询指定步骤的待审批人员列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetFormPendingReview(long formId, List<long> pendingUserIds)
        {
            var formDetail = await _db.Queryable<FormInstanceEntity>()
                                      .With(SqlWith.NoLock)
                                      .InnerJoin<UserInfoEntity>((instance, user) => instance.ApplicantUserId == user.UserId)
                                      .InnerJoin<DepartmentInfoEntity>((instance, user, dept) => user.DepartmentId == dept.DepartmentId)
                                      .InnerJoin<DepartmentLevelEntity>((instance, user, dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                                      .InnerJoin<PositionInfoEntity>((instance, user, dept, deptlevel, position) => user.PositionId == position.PositionId)
                                      .Where((instance, user, dept, deptlevel, position) => instance.FormId == formId)
                                      .Select((instance, user, dept, deptlevel, position) => new ApplicantFormDetail
                                      {
                                          FormId = instance.FormId,
                                          FormTypeId = instance.FormTypeId,
                                          RuleId = instance.RuleId,
                                          CurrentStepId = instance.CurrentStepId,
                                          ApplicantUserId = user.UserId,
                                          ApplicantDeptId = dept.DepartmentId,
                                          DeptLevelSort = deptlevel.SortOrder,
                                          PositionSort = position.SortOrder
                                      }).FirstAsync();

            bool isChinese = _lang.Locale == "zh-CN";

            var stepInfo = await _db.Queryable<WorkflowStepEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(step => step.StepId == formDetail.CurrentStepId)
                                    .Select(step => new
                                    {
                                        StepName = isChinese ? step.StepNameCn : step.StepNameEn,
                                        step.IsStartStep,
                                        step.Assignment,
                                        step.ReviewMode,
                                    }).FirstAsync();

            List<FormPendingReviewDto> result;

            if (stepInfo.IsStartStep == 1)
            {
                result = await GetStartReviewUserByStep(formDetail.ApplicantUserId);
            }
            else if (stepInfo.Assignment == Assignment.Org.ToEnumString())
            {
                var applicantDept = await _db.Queryable<DepartmentInfoEntity>()
                                             .With(SqlWith.NoLock)
                                             .ToParentListAsync(dept => dept.ParentId, formDetail.ApplicantDeptId);

                var orgInfo = await _db.Queryable<WorkflowStepOrgEntity>()
                                       .With(SqlWith.NoLock)
                                       .Where(step => step.StepId == formDetail.CurrentStepId)
                                       .FirstAsync();

                result = await GetOrgReviewUserByStep(applicantDept, orgInfo.DeptLeaveId, orgInfo.PositionId, stepInfo.ReviewMode, pendingUserIds);
            }
            else if (stepInfo.Assignment == Assignment.DeptUser.ToEnumString())
            {
                var deptUserInfo = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(step => step.StepId == formDetail.CurrentStepId)
                                            .FirstAsync();

                result = await GetDeptUserReviewUserByStep(deptUserInfo.DepartmentId, deptUserInfo.PositionId, stepInfo.ReviewMode, pendingUserIds);
            }
            else if (stepInfo.Assignment == Assignment.User.ToEnumString())
            {
                var userInfo = await _db.Queryable<WorkflowStepUserEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(step => step.StepId == formDetail.CurrentStepId)
                                        .FirstAsync();

                result = await GetUserReviewUserByStep(userInfo.UserId, stepInfo.ReviewMode);
            }
            else
            {
                result = new List<FormPendingReviewDto>();
            }

            result.ForEach(r => r.StepName = stepInfo.StepName);

            return result;
        }

        /// <summary>
        /// 查询起始步骤待审批人员
        /// </summary>
        /// <param name="applicantUserId"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetStartReviewUserByStep(long applicantUserId)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var (actual, agent, _, _, _, _, _, _) = AppointmentEnumStrings();
            var now = DateTime.Now;

            #region SQL

            string sql = $@"SELECT
                                users.UserNo       AS ReviewUserNo,
                                {userNameCol}      AS ReviewUserName,
                                agentusers.UserNo  AS AgentUserNo,
                                {agentNameCol}     AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @Agent
                                    )
                                    ELSE (
                                        SELECT {dicNameCol}
                                        FROM Basic.DictionaryInfo dic
                                        WHERE dic.DicType = 'AppointmentType'
                                          AND dic.DicCode = @Actual
                                    )
                                END AS AppointmentTypeName
                            FROM Basic.UserInfo users
                            LEFT JOIN Basic.UserAgent agent
                                   ON users.UserId     = agent.SubstituteUserId
                                  AND agent.StartTime <= @Now
                                  AND agent.EndTime   >= @Now
                            LEFT JOIN Basic.UserInfo agentusers
                                   ON agent.AgentUserId = agentusers.UserId
                            WHERE users.UserId = @ApplicantUserId";

            var parameters = new[]
            {
                new SugarParameter("@ApplicantUserId", applicantUserId),
                new SugarParameter("@Now", now),
                new SugarParameter("@Actual", actual),
                new SugarParameter("@Agent", agent),
            };

            #endregion

            var result = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>(sql, parameters);

            return result ?? new List<FormPendingReviewDto>();
        }

        /// <summary>
        /// 查询按照组织架构指定步骤待审批人员
        /// </summary>
        /// <param name="applicantParentDept"></param>
        /// <param name="deptLeaveId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewMode"></param>
        /// <param name="pendingUserId"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetOrgReviewUserByStep(List<DepartmentInfoEntity> applicantParentDept, long deptLeaveId, long positionId, string reviewMode, List<long> pendingUserIds)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(d => d.DepartmentLevelId == deptLeaveId)
                                     .FirstAsync();
            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(p => p.PositionId == positionId)
                                   .FirstAsync();

            string parentDeptIdsStr = string.Join(",", applicantParentDept.Select(d => d.DepartmentId));
            string pendingUserIdsStr = string.Join(",", pendingUserIds.Select(p => p));
            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";

            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                SELECT {topN}
                    ReviewUserNo,
                    ReviewUserName,
                    AgentUserNo,
                    AgentUserName,
                    AppointmentTypeName,
                    AppointmentTypeCode,
                    DeptLevelSort,
                    PositionSort,
                    HireDate
                FROM (

                    -- 实职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                              AND agent.StartTime       <= @Now
                                                              AND agent.EndTime         >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND users.UserId IN ({pendingUserIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsReview    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime          partime
                    INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime           <= @Now
                                                               AND agent.EndTime             >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                     WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND users.UserId IN ({pendingUserIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsReview    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DeptLevelSort", deptlevel.SortOrder),
                    new SugarParameter("@PositionSort",  posInfo.SortOrder),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }
            else
            {
                // ────────────────────────────────────────────────
                // 自动指派：先递减职级，同职级下再递减部门职级
                // 每次减 1，最小值为 1，找到即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                            SELECT {topN}
                                ReviewUserNo,
                                ReviewUserName,
                                AgentUserNo,
                                AgentUserName,
                                AppointmentTypeName,
                                AppointmentTypeCode,
                                DeptLevelSort,
                                PositionSort,
                                HireDate
                            FROM (

                                -- 实职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoActual
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                        ELSE @AutoActual
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserInfo users
                                INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                          AND agent.StartTime       <= @Now
                                                                          AND agent.EndTime         >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                                WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                                  AND users.UserId IN ({pendingUserIdsStr})
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                                UNION ALL

                                -- 兼职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrentAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrent
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                        ELSE @AutoConcurrent
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserPartTime          partime
                                INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                                INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                                           AND agent.StartTime           <= @Now
                                                                           AND agent.EndTime             >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                                WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                                  AND users.UserId IN ({pendingUserIdsStr})
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                            ) combined
                            {autoOrderBy}",
                            new[]
                            {
                                new SugarParameter("@Now", now),
                                new SugarParameter("@CurrentPositionSort", currentPositionSort),
                                new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                                new SugarParameter("@AutoActual", autoActual),
                                new SugarParameter("@AutoAgent", autoAgent),
                                new SugarParameter("@AutoConcurrent", autoConcurrent),
                                new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                            });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }
            }

            return new List<FormPendingReviewDto>();
        }

        /// <summary>
        /// 查询按照指定部门指定职级指定步骤待审批人员
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="reviewMode"></param>
        /// <param name="pendingUserIds"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetDeptUserReviewUserByStep(long departmentId, long positionId, string reviewMode, List<long> pendingUserIds)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(p => p.PositionId == positionId)
                                   .FirstAsync();
            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(d => d.DepartmentId == departmentId)
                                    .FirstAsync();
            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(d => d.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            string pendingUserIdsStr = string.Join(",", pendingUserIds.Select(p => p));

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";
            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            // ────────────────────────────────────────────────
            // 第一次：精确匹配，按签核方式返回笔数
            // ────────────────────────────────────────────────

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                SELECT {topN}
                    ReviewUserNo,
                    ReviewUserName,
                    AgentUserNo,
                    AgentUserName,
                    AppointmentTypeName,
                    AppointmentTypeCode,
                    DeptLevelSort,
                    PositionSort,
                    HireDate
                FROM (

                    -- 实职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                              AND agent.StartTime       <= @Now
                                                              AND agent.EndTime         >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                    WHERE dept.DepartmentId  = @DepartmentId
                      AND users.UserId IN ({pendingUserIdsStr})
                      AND position.SortOrder = @PositionSort
                      AND users.IsReview   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime          partime
                    INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime           <= @Now
                                                               AND agent.EndTime             >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                    WHERE dept.DepartmentId  = @DepartmentId
                      AND users.UserId IN ({pendingUserIdsStr})
                      AND position.SortOrder = @PositionSort
                      AND users.IsReview   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@DepartmentId", departmentId),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }
            else
            {
                // ────────────────────────────────────────────────
                // 自动指派：精确匹配找不到时，先递减职级再递减部门职级
                // 每次减 1，最小值为 1，找到候选人即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                            SELECT {topN}
                                ReviewUserNo,
                                ReviewUserName,
                                AgentUserNo,
                                AgentUserName,
                                AppointmentTypeName,
                                AppointmentTypeCode,
                                DeptLevelSort,
                                PositionSort,
                                HireDate
                            FROM (

                                -- 实职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoActual
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                        ELSE @AutoActual
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserInfo users
                                INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                          AND agent.StartTime       <= @Now
                                                                          AND agent.EndTime         >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                                WHERE dept.DepartmentId  = @DepartmentId
                                  AND users.UserId IN ({pendingUserIdsStr})
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                                UNION ALL

                                -- 兼职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrentAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrent
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                        ELSE @AutoConcurrent
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserPartTime          partime
                                INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                                INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                                           AND agent.StartTime           <= @Now
                                                                           AND agent.EndTime             >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                                WHERE dept.DepartmentId  = @DepartmentId
                                  AND users.UserId IN ({pendingUserIdsStr})
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                            ) combined
                            {autoOrderBy}",
                            new[]
                            {
                                new SugarParameter("@Now", now),
                                new SugarParameter("@DepartmentId", departmentId),
                                new SugarParameter("@CurrentPositionSort", currentPositionSort),
                                new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                                new SugarParameter("@AutoActual", autoActual),
                                new SugarParameter("@AutoAgent", autoAgent),
                                new SugarParameter("@AutoConcurrent", autoConcurrent),
                                new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                            });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }
            }

            return new List<FormPendingReviewDto>();
        }

        /// <summary>
        /// 查询按照指定人指定步骤待审批人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reviewMode"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetUserReviewUserByStep(long userId, string reviewMode)
        {
            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            var userInfo = await _db.Queryable<UserInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(u => u.UserId == userId)
                                    .FirstAsync();
            var posInfo = await _db.Queryable<PositionInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(p => p.PositionId == userInfo.PositionId)
                                   .FirstAsync();
            var deptInfo = await _db.Queryable<DepartmentInfoEntity>()
                                    .With(SqlWith.NoLock)
                                    .Where(d => d.DepartmentId == userInfo.DepartmentId)
                                    .FirstAsync();
            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(d => d.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = reviewMode == ReviewMode.Single.ToEnumString();
            string topN = isSingle ? "TOP 1" : "";
            string exactOrderBy = BuildOrderBy(isSingle, isAuto: false);
            string autoOrderBy = BuildOrderBy(isSingle, isAuto: true);

            var (actual, agent, concurrent, concurrentAgent, autoActual, autoAgent, autoConcurrent, autoConcurrentAgent) = AppointmentEnumStrings();
            var now = DateTime.Now;

            // ────────────────────────────────────────────────
            // 第一次：精确匹配指定用户
            // ────────────────────────────────────────────────

            #region SQL

            var exactResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                SELECT {topN}
                    ReviewUserNo,
                    ReviewUserName,
                    AgentUserNo,
                    AgentUserName,
                    AppointmentTypeName,
                    AppointmentTypeCode,
                    DeptLevelSort,
                    PositionSort,
                    HireDate
                FROM (

                    -- 主职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Agent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Actual
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @Agent
                            ELSE @Actual
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserInfo users
                    INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                              AND agent.StartTime       <= @Now
                                                              AND agent.EndTime         >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                    WHERE users.UserId     = @UserId
                      AND users.IsReview = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze   = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserNo      AS ReviewUserNo,
                        {userNameCol}     AS ReviewUserName,
                        agentusers.UserNo AS AgentUserNo,
                        {agentNameCol}    AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @ConcurrentAgent
                            )
                            ELSE (
                                SELECT {dicNameCol}
                                FROM Basic.DictionaryInfo dic
                                WHERE dic.DicType = 'AppointmentType'
                                  AND dic.DicCode = @Concurrent
                            )
                        END AS AppointmentTypeName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL THEN @ConcurrentAgent
                            ELSE @Concurrent
                        END AS AppointmentTypeCode,
                        deptlevel.SortOrder AS DeptLevelSort,
                        position.SortOrder  AS PositionSort,
                        users.HireDate      AS HireDate
                    FROM Basic.UserPartTime          partime
                    INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                    INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                    INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                    INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                    LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                               AND agent.StartTime           <= @Now
                                                               AND agent.EndTime             >= @Now
                    LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                    WHERE partime.UserId   = @UserId
                      AND users.IsReview = 1
                      AND users.IsEmployed = 1
                      AND users.IsFreeze   = 0

                ) combined
                {exactOrderBy}",
                new[]
                {
                    new SugarParameter("@Now", now),
                    new SugarParameter("@UserId", userId),
                    new SugarParameter("@Actual", actual),
                    new SugarParameter("@Agent", agent),
                    new SugarParameter("@Concurrent", concurrent),
                    new SugarParameter("@ConcurrentAgent", concurrentAgent),
                });

            #endregion

            if (exactResult.Any())
            {
                return exactResult;
            }
            else
            {
                // ────────────────────────────────────────────────
                // 自动指派：指定人找不到时，按该用户所属部门
                // 先递减职级，同职级下再递减部门职级
                // 每次减 1，最小值为 1，找到即跳出
                // ────────────────────────────────────────────────
                int currentPositionSort = posInfo.SortOrder - 1;
                int currentDeptLevelSort = deptlevel.SortOrder;

                while (currentPositionSort >= 1)
                {
                    while (currentDeptLevelSort >= 1)
                    {
                        #region SQL

                        var autoResult = await _db.Ado.SqlQueryAsync<FormPendingReviewDto>($@"
                            SELECT {topN}
                                ReviewUserNo,
                                ReviewUserName,
                                AgentUserNo,
                                AgentUserName,
                                AppointmentTypeName,
                                AppointmentTypeCode,
                                DeptLevelSort,
                                PositionSort,
                                HireDate
                            FROM (

                                -- 主职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoActual
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoAgent
                                        ELSE @AutoActual
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserInfo users
                                INNER JOIN Basic.DepartmentInfo  dept      ON users.DepartmentId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel ON dept.DepartmentLevelId = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position  ON users.PositionId       = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent     ON users.UserId           = agent.SubstituteUserId
                                                                          AND agent.StartTime       <= @Now
                                                                          AND agent.EndTime         >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId    = agentusers.UserId
                                WHERE dept.DepartmentId  = @DepartmentId
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                                UNION ALL

                                -- 兼职（自动指派）
                                SELECT
                                    users.UserNo      AS ReviewUserNo,
                                    {userNameCol}     AS ReviewUserName,
                                    agentusers.UserNo AS AgentUserNo,
                                    {agentNameCol}    AS AgentUserName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL
                                        THEN (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrentAgent
                                        )
                                        ELSE (
                                            SELECT {dicNameCol}
                                            FROM Basic.DictionaryInfo dic
                                            WHERE dic.DicType = 'AppointmentType'
                                              AND dic.DicCode = @AutoConcurrent
                                        )
                                    END AS AppointmentTypeName,
                                    CASE
                                        WHEN agent.AgentUserId IS NOT NULL THEN @AutoConcurrentAgent
                                        ELSE @AutoConcurrent
                                    END AS AppointmentTypeCode,
                                    deptlevel.SortOrder AS DeptLevelSort,
                                    position.SortOrder  AS PositionSort,
                                    users.HireDate      AS HireDate
                                FROM Basic.UserPartTime          partime
                                INNER JOIN Basic.UserInfo        users      ON partime.UserId             = users.UserId
                                INNER JOIN Basic.DepartmentInfo  dept       ON partime.PartTimeDeptId     = dept.DepartmentId
                                INNER JOIN Basic.DepartmentLevel deptlevel  ON dept.DepartmentLevelId     = deptlevel.DepartmentLevelId
                                INNER JOIN Basic.PositionInfo    position   ON partime.PartTimePositionId = position.PositionId
                                LEFT  JOIN Basic.UserAgent       agent      ON users.UserId               = agent.SubstituteUserId
                                                                           AND agent.StartTime           <= @Now
                                                                           AND agent.EndTime             >= @Now
                                LEFT  JOIN Basic.UserInfo        agentusers ON agent.AgentUserId         = agentusers.UserId
                                WHERE dept.DepartmentId  = @DepartmentId
                                  AND position.SortOrder  = @CurrentPositionSort
                                  AND deptlevel.SortOrder = @CurrentDeptLevelSort
                                  AND users.IsReview    = 1
                                  AND users.IsEmployed    = 1
                                  AND users.IsFreeze      = 0

                            ) combined
                            {autoOrderBy}",
                            new[]
                            {
                                new SugarParameter("@Now", now),
                                new SugarParameter("@DepartmentId", userInfo.DepartmentId),
                                new SugarParameter("@CurrentPositionSort", currentPositionSort),
                                new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                                new SugarParameter("@AutoActual", autoActual),
                                new SugarParameter("@AutoAgent", autoAgent),
                                new SugarParameter("@AutoConcurrent", autoConcurrent),
                                new SugarParameter("@AutoConcurrentAgent", autoConcurrentAgent),
                            });

                        #endregion

                        if (autoResult.Any())
                        {
                            return autoResult;
                        }
                        else
                        {
                            currentDeptLevelSort--;
                        }
                    }

                    currentPositionSort--;
                    currentDeptLevelSort = deptlevel.SortOrder;
                }
            }

            return new List<FormPendingReviewDto>();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="isSingle"></param>
        /// <param name="isAuto"></param>
        /// <returns></returns>
        private string BuildOrderBy(bool isSingle, bool isAuto)
        {
            if (!isSingle)
            {
                return "ORDER BY combined.HireDate DESC";
            }
            else
            {
                string c0 = isAuto ? AppointmentType.AutoActual.ToEnumString() : AppointmentType.Actual.ToEnumString();
                string c1 = isAuto ? AppointmentType.AutoAgent.ToEnumString() : AppointmentType.Agent.ToEnumString();
                string c2 = isAuto ? AppointmentType.AutoConcurrent.ToEnumString() : AppointmentType.Concurrent.ToEnumString();
                string c3 = isAuto ? AppointmentType.AutoConcurrentAgent.ToEnumString() : AppointmentType.ConcurrentAgent.ToEnumString();

                return $@"ORDER BY CASE combined.AppointmentTypeCode
                        WHEN '{c0}' THEN 0
                        WHEN '{c1}' THEN 1
                        WHEN '{c2}' THEN 2
                        WHEN '{c3}' THEN 3
                        ELSE 9
                    END ASC, combined.HireDate DESC";
            }
        }

        /// <summary>
        /// 一次性取出所有 AppointmentType 枚举字符串
        /// </summary>
        /// <returns></returns>
        private (string actual, string agent, string concurrent, string concurrentAgent, string autoActual, string autoAgent, string autoConcurrent, string autoConcurrentAgent) AppointmentEnumStrings() =>
        (
            AppointmentType.Actual.ToEnumString(),
            AppointmentType.Agent.ToEnumString(),
            AppointmentType.Concurrent.ToEnumString(),
            AppointmentType.ConcurrentAgent.ToEnumString(),
            AppointmentType.AutoActual.ToEnumString(),
            AppointmentType.AutoAgent.ToEnumString(),
            AppointmentType.AutoConcurrent.ToEnumString(),
            AppointmentType.AutoConcurrentAgent.ToEnumString()
        );
    }
}
