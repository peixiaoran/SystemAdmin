using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.Workflow.ReviewFlowManager;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class ReviewFlowManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public ReviewFlowManager(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
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
        public async Task<List<FormReviewFlow>> GetFullReviewFlow(long formId)
        {
            var formReviewFlowList = new List<FormReviewFlow>();

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
                var formReviewFlow = new FormReviewFlow();
                var stepReviewUser = new List<StepReviewUser>();

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
                                            step.ApproveMode,
                                        }).FirstAsync();

                formReviewFlow.StepId = currentStepId;
                formReviewFlow.StepName = stepInfo.StepName;
                if (stepInfo.IsStartStep == 1)
                {
                    stepReviewUser = await GetStartStepReviewUser(formDetail.ApplicantUserId);
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
                        formReviewFlow.Skip = 1;
                    }
                    else
                    {
                        stepReviewUser = await GetOrgStepReviewUser(applicantDept, orgInfo.DeptLeaveId, orgInfo.PositionId, stepInfo.ApproveMode);
                    }
                }
                else if (stepInfo.Assignment == Assignment.DeptUser.ToEnumString())
                {
                    var deptUserInfo = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                                .With(SqlWith.NoLock)
                                                .Where(step => step.StepId == currentStepId)
                                                .FirstAsync();
                    stepReviewUser = await GetDeptUserStepReviewUser(deptUserInfo.DepartmentId, deptUserInfo.PositionId, stepInfo.ApproveMode);
                }
                else if (stepInfo.Assignment == Assignment.User.ToEnumString())
                {
                    var userInfo = await _db.Queryable<WorkflowStepUserEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(step => step.StepId == currentStepId)
                                            .FirstAsync();
                    stepReviewUser = await GetUserStepReviewUser(userInfo.UserId, stepInfo.ApproveMode);
                }

                formReviewFlow.stepReviewUser.AddRange(stepReviewUser);
                formReviewFlowList.Add(formReviewFlow);

                currentStepId = await _db.Queryable<WorkflowRuleStepEntity>()
                                         .With(SqlWith.NoLock)
                                         .Where(rule => rule.RuleId == formDetail.RuleId && rule.CurrentStepId == currentStepId)
                                         .Select(rule => rule.NextStepId)
                                         .FirstAsync();
            }

            // 获取审批结果
            formReviewFlowList = await GetFormReviewResult(formId, formDetail.CurrentStepId, formReviewFlowList);
            return formReviewFlowList;
        }

        /// <summary>
        /// 查询起始步骤审核人员
        /// </summary>
        /// <param name="applicantUserId"></param>
        /// <returns></returns>
        public async Task<List<StepReviewUser>> GetStartStepReviewUser(long applicantUserId)
        {
            var stepReviewUser = new List<StepReviewUser>();

            bool isChinese = _lang.Locale == "zh-CN";
            string userNameCol = isChinese ? "users.UserNameCn" : "users.UserNameEn";
            string agentNameCol = isChinese ? "agentusers.UserNameCn" : "agentusers.UserNameEn";
            string dicNameCol = isChinese ? "dic.DicNameCn" : "dic.DicNameEn";

            string appointmentTypeSubQuery(string dicCode) => $@"
                    SELECT {dicNameCol}
                    FROM   Basic.DictionaryInfo dic
                    WHERE  dic.DicType = 'AppointmentType'
                      AND  dic.DicCode = '{dicCode}'";

            string sql = $@"SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN ({appointmentTypeSubQuery("Agent")})
                                    ELSE ({appointmentTypeSubQuery("Actual")})
                                END AS AppointmentTypeName
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
                new SugarParameter("@Now", DateTime.Now),
            };

            var result = await _db.Ado.SqlQueryAsync<StepReviewUser>(sql, parameters);

            return result ?? new List<StepReviewUser>();
        }

        /// <summary>
        /// 查询按照组织架构审核人员
        /// </summary>
        /// <param name="applicantParentDept"></param>
        /// <param name="deptLeaveId"></param>
        /// <param name="positionId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<List<StepReviewUser>> GetOrgStepReviewUser(List<DepartmentInfoEntity> applicantParentDept, long deptLeaveId, long positionId, string mode)
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

            var parentDeptIds = applicantParentDept.Select(d => d.DepartmentId).ToList();
            string parentDeptIdsStr = string.Join(",", parentDeptIds);

            bool isSingle = mode == ApproveMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            string orderBy = isSingle
                ? @"ORDER BY
                CASE AppointmentTypeCode
                    WHEN 'Actual'              THEN 0
                    WHEN 'AutoActual'          THEN 0
                    WHEN 'Agent'               THEN 1
                    WHEN 'AutoAgent'           THEN 1
                    WHEN 'Concurrent'          THEN 2
                    WHEN 'AutoConcurrent'      THEN 2
                    WHEN 'ConcurrentAgent'     THEN 3
                    WHEN 'AutoConcurrentAgent' THEN 3
                    ELSE 9
                END ASC,
                HireDate DESC"
                : "ORDER BY HireDate DESC";

            // ────────────────────────────────────────────────
            // 第一次：精确匹配 deptlevel.SortOrder + position.SortOrder
            // ────────────────────────────────────────────────
            var exactResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                SELECT {topN}
                    UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                    DeptLevelSort, PositionSort, HireDate
                FROM (

                    -- 主职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Agent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Actual')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'Agent' ELSE 'Actual' END AS AppointmentTypeCode,
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
                    WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsApproval    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'ConcurrentAgent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Concurrent')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'ConcurrentAgent' ELSE 'Concurrent' END AS AppointmentTypeCode,
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
                      AND deptlevel.SortOrder = @DeptLevelSort
                      AND position.SortOrder  = @PositionSort
                      AND users.IsApproval    = 1
                      AND users.IsEmployed    = 1
                      AND users.IsFreeze      = 0

                ) combined
                {orderBy}",
                new[]
                {
                    new SugarParameter("@Now", DateTime.Now),
                    new SugarParameter("@DeptLevelSort", deptlevel.SortOrder),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                });

            if (exactResult.Any())
                return exactResult;

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
                    var autoResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                        SELECT {topN}
                             UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                            DeptLevelSort, PositionSort, HireDate
                        FROM (

                            -- 主职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoActual')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoAgent' ELSE 'AutoActual' END AS AppointmentTypeCode,
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
                            WHERE dept.DepartmentId IN ({parentDeptIdsStr})
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrentAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrent')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoConcurrentAgent' ELSE 'AutoConcurrent' END AS AppointmentTypeCode,
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
                              AND position.SortOrder  = @CurrentPositionSort
                              AND deptlevel.SortOrder = @CurrentDeptLevelSort
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {orderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", DateTime.Now),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort",currentDeptLevelSort),
                        });
                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<StepReviewUser>();
        }

        /// <summary>
        /// 查询按照指定部门指定职级审核人员
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<List<StepReviewUser>> GetDeptUserStepReviewUser(long departmentId, long positionId, string mode)
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

            bool isSingle = mode == ApproveMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            string orderBy = isSingle
                ? @"ORDER BY
                CASE AppointmentTypeCode
                    WHEN 'Actual'              THEN 0
                    WHEN 'AutoActual'          THEN 0
                    WHEN 'Agent'               THEN 1
                    WHEN 'AutoAgent'           THEN 1
                    WHEN 'Concurrent'          THEN 2
                    WHEN 'AutoConcurrent'      THEN 2
                    WHEN 'ConcurrentAgent'     THEN 3
                    WHEN 'AutoConcurrentAgent' THEN 3
                    ELSE 9
                END ASC,
                HireDate DESC"
                : "ORDER BY HireDate DESC";

            // ────────────────────────────────────────────────
            // 第一次：精确匹配，按签核方式返回笔数
            // ────────────────────────────────────────────────
            var exactResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                SELECT {topN}
                    UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                    DeptLevelSort, PositionSort, HireDate
                FROM (

                    -- 主职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Agent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Actual')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'Agent' ELSE 'Actual' END AS AppointmentTypeCode,
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
                      AND users.IsApproval   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'ConcurrentAgent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Concurrent')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'ConcurrentAgent' ELSE 'Concurrent' END AS AppointmentTypeCode,
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
                      AND users.IsApproval   = 1
                      AND users.IsEmployed   = 1
                      AND users.IsFreeze     = 0

                ) combined
                {orderBy}",
                new[]
                {
                    new SugarParameter("@Now", DateTime.Now),
                    new SugarParameter("@DepartmentId", departmentId),
                    new SugarParameter("@PositionSort", posInfo.SortOrder),
                });

            if (exactResult.Any())
                return exactResult;

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
                    var autoResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                        SELECT {topN}
                            UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                            DeptLevelSort, PositionSort, HireDate
                        FROM (

                            -- 主职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoActual')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoAgent' ELSE 'AutoActual' END AS AppointmentTypeCode,
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
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrentAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrent')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoConcurrentAgent' ELSE 'AutoConcurrent' END AS AppointmentTypeCode,
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
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {orderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", DateTime.Now),
                            new SugarParameter("@DepartmentId", departmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                        });

                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<StepReviewUser>();
        }

        /// <summary>
        /// 查询按照指定人审核人员
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<List<StepReviewUser>> GetUserStepReviewUser(long userId, string mode)
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
                                    .Where(department => department.DepartmentId == userInfo.DepartmentId)
                                    .FirstAsync();

            var deptlevel = await _db.Queryable<DepartmentLevelEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(deptlevel => deptlevel.DepartmentLevelId == deptInfo.DepartmentLevelId)
                                     .FirstAsync();

            bool isSingle = mode == ApproveMode.Single.ToEnumString();

            string topN = isSingle ? "TOP 1" : "";
            string orderBy = isSingle
                ? @"ORDER BY
                CASE AppointmentTypeCode
                    WHEN 'Actual'              THEN 0
                    WHEN 'AutoActual'          THEN 0
                    WHEN 'Agent'               THEN 1
                    WHEN 'AutoAgent'           THEN 1
                    WHEN 'Concurrent'          THEN 2
                    WHEN 'AutoConcurrent'      THEN 2
                    WHEN 'ConcurrentAgent'     THEN 3
                    WHEN 'AutoConcurrentAgent' THEN 3
                    ELSE 9
                END ASC,
                HireDate DESC"
                : "ORDER BY HireDate DESC";

            // ────────────────────────────────────────────────
            // 第一次：精确匹配指定用户
            // ────────────────────────────────────────────────
            var exactResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                SELECT {topN}
                    UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                    DeptLevelSort, PositionSort, HireDate
                FROM (

                    -- 主职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Agent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Actual')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'Agent' ELSE 'Actual' END AS AppointmentTypeCode,
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
                    WHERE users.UserId      = @UserId
                      AND users.IsApproval  = 1
                      AND users.IsEmployed  = 1
                      AND users.IsFreeze    = 0

                    UNION ALL

                    -- 兼职
                    SELECT
                        users.UserId,
                        {userNameCol}  AS UserName,
                        agentusers.UserId AS AgentUserId,
                        {agentNameCol} AS AgentUserName,
                        CASE
                            WHEN agent.AgentUserId IS NOT NULL
                            THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'ConcurrentAgent')
                            ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'Concurrent')
                        END AS AppointmentTypeName,
                        CASE WHEN agent.AgentUserId IS NOT NULL THEN 'ConcurrentAgent' ELSE 'Concurrent' END AS AppointmentTypeCode,
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
                      AND users.IsApproval  = 1
                      AND users.IsEmployed  = 1
                      AND users.IsFreeze    = 0

                ) combined
                {orderBy}",
                new[]
                {
                    new SugarParameter("@Now",    DateTime.Now),
                    new SugarParameter("@UserId", userId),
                });

            if (exactResult.Any())
                return exactResult;

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
                    var autoResult = await _db.Ado.SqlQueryAsync<StepReviewUser>($@"
                        SELECT {topN}
                            UserId, UserName, AgentUserId, AgentUserName, AppointmentTypeName, AppointmentTypeCode,
                            DeptLevelSort, PositionSort, HireDate
                        FROM (

                            -- 主职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoActual')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoAgent' ELSE 'AutoActual' END AS AppointmentTypeCode,
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
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                            UNION ALL

                            -- 兼职（自动指派）
                            SELECT
                                users.UserId,
                                {userNameCol}  AS UserName,
                                agentusers.UserId AS AgentUserId,
                                {agentNameCol} AS AgentUserName,
                                CASE
                                    WHEN agent.AgentUserId IS NOT NULL
                                    THEN (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrentAgent')
                                    ELSE (SELECT {dicNameCol} FROM Basic.DictionaryInfo dic WHERE DicType = 'AppointmentType' AND DicCode = 'AutoConcurrent')
                                END AS AppointmentTypeName,
                                CASE WHEN agent.AgentUserId IS NOT NULL THEN 'AutoConcurrentAgent' ELSE 'AutoConcurrent' END AS AppointmentTypeCode,
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
                              AND users.IsApproval    = 1
                              AND users.IsEmployed    = 1
                              AND users.IsFreeze      = 0

                        ) combined
                        {orderBy}",
                        new[]
                        {
                            new SugarParameter("@Now", DateTime.Now),
                            new SugarParameter("@DepartmentId", userInfo.DepartmentId),
                            new SugarParameter("@CurrentPositionSort", currentPositionSort),
                            new SugarParameter("@CurrentDeptLevelSort", currentDeptLevelSort),
                        });

                    if (autoResult.Any())
                        return autoResult;

                    currentDeptLevelSort--;
                }

                currentPositionSort--;
                currentDeptLevelSort = deptlevel.SortOrder;
            }

            return new List<StepReviewUser>();
        }

        /// <summary>
        /// 取最后一笔驳回记录之后的有效签核数据
        /// </summary>
        private async Task<List<FormReviewRecordEntity>> GetValidReviewRecords(long formId)
        {
            var reviewRecord = await _db.Queryable<FormReviewRecordEntity>()
                                        .With(SqlWith.NoLock)
                                        .Where(record => record.FormId == formId)
                                        .OrderBy(record => record.ReviewDateTime)
                                        .ToListAsync();

            int lastRejectedIndex = -1;
            for (int i = reviewRecord.Count - 1; i >= 0; i--)
            {
                if (reviewRecord[i].ReviewResult == ReviewResult.Rejected.ToEnumString())
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
        public async Task<List<FormReviewFlow>> GetFormReviewResult(long formId, long currentStepId, List<FormReviewFlow> reviewFlow)
        {
            var validRecords = await GetValidReviewRecords(formId);

            // 查询签核状态字典
            var dicList = await _db.Queryable<DictionaryInfoEntity>()
                                   .With(SqlWith.NoLock)
                                   .Where(d => d.DicType == "FormReviewResult")
                                   .ToListAsync();

            bool isChinese = _lang.Locale == "zh-CN";

            string GetDicName(string dicCode)
            {
                var dic = dicList.FirstOrDefault(d => d.DicCode == dicCode);
                if (dic == null) return dicCode;
                return isChinese ? dic.DicNameCn : dic.DicNameEn;
            }

            foreach (var flow in reviewFlow)
            {
                // 跳过的步骤不处理
                if (flow.Skip == 1)
                    continue;

                // 当前步骤与该步骤不符，尚未轮到该步骤签核
                if (currentStepId != flow.StepId)
                {
                    flow.Result = GetDicName(FormReviewResult.Unsigned.ToEnumString());
                    foreach (var user in flow.stepReviewUser)
                        user.Result = GetDicName(FormReviewResult.Unsigned.ToEnumString());

                    continue;
                }

                // 当前步骤符合，逐一判断每位签核人员的状态
                // 实或代其中一个 UserId 符合记录即视为已签核
                foreach (var user in flow.stepReviewUser)
                {
                    bool hasSigned = validRecords.Any(r =>
                        r.StepId == flow.StepId &&
                        (r.ReviewUserId == user.UserId || r.ReviewUserId == user.AgentUserId));

                    user.Result = hasSigned
                        ? GetDicName(FormReviewResult.Signed.ToEnumString())
                        : GetDicName(FormReviewResult.UnderReview.ToEnumString());
                }

                // 步骤内所有人都已签核则步骤完成，否则审批中
                bool allSigned = flow.stepReviewUser.All(u => u.Result == GetDicName(FormReviewResult.Signed.ToEnumString()));

                flow.Result = allSigned
                    ? GetDicName(FormReviewResult.Signed.ToEnumString())
                    : GetDicName(FormReviewResult.UnderReview.ToEnumString());
            }

            return reviewFlow;
        }
    }
}
