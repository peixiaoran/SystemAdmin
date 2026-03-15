using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowStepRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowStepRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStep(WorkflowStepEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增步骤-组织架构
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepOrg(WorkflowStepOrgEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        ///  新增步骤-指定部门员工级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepDeptUser(WorkflowStepDeptUserEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增步骤-指定员工
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepUser(WorkflowStepUserEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增步骤-自定义逻辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepCustom(WorkflowStepCustomEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStep(long stepId)
        {
            return await _db.Deleteable<WorkflowStepEntity>()
                            .Where(stepinfo => stepinfo.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤-组织架构
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepOrg(long stepId)
        {
            return await _db.Deleteable<WorkflowStepOrgEntity>()
                            .Where(steporg => steporg.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤-指定部门员工级别
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepDeptUser(long stepId)
        {
            return await _db.Deleteable<WorkflowStepOrgEntity>()
                            .Where(stepdeptuser => stepdeptuser.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤-指定员工
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepUser(long stepId)
        {
            return await _db.Deleteable<WorkflowStepUserEntity>()
                            .Where(stepuser => stepuser.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤-自定义逻辑
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepCustom(long stepId)
        {
            return await _db.Deleteable<WorkflowStepOrgEntity>()
                            .Where(stepcustom => stepcustom.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤流程分支
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepBranch(long stepId)
        {
            return await _db.Deleteable<WorkflowStepBranchEntity>()
                            .Where(stepcondition => stepcondition.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改下一步骤为此步骤 -1
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStepBranch(long stepId)
        {
            return await _db.Updateable<WorkflowStepBranchEntity>()
                            .SetColumns(stepcondition => stepcondition.NextStepId == -1)
                            .Where(stepcondition => stepcondition.NextStepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStep(WorkflowStepEntity upsert)
        {
            return await _db.Updateable(upsert)
                            .IgnoreColumns(stepinfo => new
                            {
                                stepinfo.StepId,
                                stepinfo.FormTypeId,
                                stepinfo.CreatedBy,
                                stepinfo.CreatedDate,
                            }).Where(stepinfo => stepinfo.StepId == upsert.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询步骤及流程分支列表
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowStepListDto>> GetWorkflowStepList(GetWorkflowStepList getPage)
        {
            RefAsync<int> totalCount = 0;
            var page = await _db.Queryable<WorkflowStepEntity>()
                                    .With(SqlWith.NoLock)
                                    .InnerJoin<DictionaryInfoEntity>((stepinfo, dic) => dic.DicType == "Assignment" && stepinfo.Assignment == dic.DicCode)
                                    .Where((stepinfo, dic) => stepinfo.FormTypeId == long.Parse(getPage.FormTypeId))
                                    .OrderBy((stepinfo, dic) => stepinfo.CreatedDate)
                                    .Select((stepinfo, dic) => new WorkflowStepListDto()
                                    {
                                        StepId = stepinfo.StepId,
                                        StepName = _lang.Locale == "zh-CN"
                                                   ? stepinfo.StepNameCn
                                                   : stepinfo.StepNameEn,
                                        Assignment = dic.DicCode,
                                        AssignmentName = _lang.Locale == "zh-CN"
                                                   ? dic.DicNameCn
                                                   : dic.DicNameEn,
                                    }).ToListAsync();

            // 循环查询步骤流程分支信息
            foreach (var stepItem in page)
            {
                var stepConList = await _db.Queryable<WorkflowStepBranchEntity>()
                                           .With(SqlWith.NoLock)
                                           .LeftJoin<WorkflowConditionEntity>((stepBranch, condition) => stepBranch.ConditionId == condition.ConditionId)
                                           .LeftJoin<WorkflowStepEntity>((stepBranch, condition, nextStep) => stepBranch.NextStepId == nextStep.StepId)
                                           .Where((stepBranch, condition, nextStep) => stepBranch.StepId == stepItem.StepId)
                                           .Select((stepBranch, condition, nextStep) => new WorkflowStepBranchDto()
                                           {
                                               StepId = stepItem.StepId,
                                               ConditionId = condition.ConditionId,
                                               ConditionName = _lang.Locale == "zh-CN"
                                                               ? condition.ConditionNameCn
                                                               : condition.ConditionNameEn,
                                               ExecuteMatched = stepBranch.ExecuteMatched,
                                               NextStepId = stepBranch.NextStepId,
                                               NextStepName = _lang.Locale == "zh-CN"
                                                               ? nextStep.StepNameCn
                                                               : nextStep.StepNameEn
                                           }).ToListAsync();
                stepItem.StepBranchList = stepConList;
            }
            return ResultPaged<WorkflowStepListDto>.Ok(page.Adapt<List<WorkflowStepListDto>>(), totalCount);
        }

        /// <summary>
        /// 查询步骤实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepEntity> GetWorkflowStepEntity(long stepId)
        {
            return await _db.Queryable<WorkflowStepEntity>()
                            .With(SqlWith.NoLock)
                            .Where(stepinfo => stepinfo.StepId == stepId)
                            .OrderBy(stepinfo => stepinfo.CreatedDate)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询步骤-组织架构实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepOrgEntity> GetWorkflowStepOrgEntity(long stepId)
        {
            return await _db.Queryable<WorkflowStepOrgEntity>()
                            .With(SqlWith.NoLock)
                            .Where(steporg => steporg.StepId == stepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询步骤-指定部门员工级别实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepDeptUserEntity> GetWorkflowStepDeptUserEntity(long stepId)
        {
            return await _db.Queryable<WorkflowStepDeptUserEntity>()
                            .With(SqlWith.NoLock)
                            .Where(stepdeptuser => stepdeptuser.StepId == stepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询步骤-指定员工实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepUserEntity> GetWorkflowStepUserEntity(long stepId)
        {
            return await _db.Queryable<WorkflowStepUserEntity>()
                            .With(SqlWith.NoLock)
                            .Where(stepuser => stepuser.StepId == stepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 查询步骤-指定员工实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepCustomEntity> GetWorkflowStepCustomEntity(long stepId)
        {
            return await _db.Queryable<WorkflowStepCustomEntity>()
                            .With(SqlWith.NoLock)
                            .Where(stepappcustom => stepappcustom.StepId == stepId)
                            .FirstAsync();
        }

        /// <summary>
        /// 新增步骤流程分支
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepBranch(WorkflowStepBranchEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除步骤流程分支
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStepBranch(long stepId, long conditionId)
        {
            return await _db.Deleteable<WorkflowStepBranchEntity>()
                            .Where(stepbranch => stepbranch.StepId == stepId && stepbranch.ConditionId == conditionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询步骤流程分支实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepBranchDto> GetWorkflowStepBranchEntity(long stepId, long conditionId)
        {
            var entity = await _db.Queryable<WorkflowStepBranchEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(condition => condition.StepId == stepId && condition.ConditionId == conditionId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepBranchDto>();
        }
       
        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormGroupDropDto>> GetFormGroupDropDown()
        {
            return await _db.Queryable<FormGroupEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(formgroup => formgroup.SortOrder)
                            .Select(formgroup => new FormGroupDropDto
                            {
                                FormGroupId = formgroup.FormGroupId,
                                FormGroupName = _lang.Locale == "zh-CN"
                                                ? formgroup.FormGroupNameCn
                                                : formgroup.FormGroupNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 表单类型下拉
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDropDown(long groupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormGroupId == groupId)
                            .OrderBy(formtype => formtype.SortOrder)
                            .Select(formtype => new FormTypeDropDto
                            {
                                FormTypeId = formtype.FormTypeId,
                                FormTypeName = _lang.Locale == "zh-CN"
                                               ? formtype.FormTypeNameCn
                                               : formtype.FormTypeNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 步骤指派规则下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssignmentDropDto>> GetAssignmentDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.DicType == "Assignment")
                            .Select(dic => new AssignmentDropDto
                            {
                                AssignmentCode = dic.DicCode,
                                AssignmentName = _lang.Locale == "zh-CN"
                                                 ? dic.DicNameCn
                                                 : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 步骤签核级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArchiLevelDropDto>> GetArchiLevelDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderByDescending(dic => dic.CreatedDate)
                            .Where(dic => dic.DicType == "ArchitectureLevel")
                            .Select(dic => new ArchiLevelDropDto
                            {
                                ArchiLevelCode = dic.DicCode,
                                ArchiLevelName = _lang.Locale == "zh-CN"
                                                ? dic.DicNameCn
                                                : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 步骤签核方式下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApproveModeDropDto>> GetApproveModeDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderByDescending(dic => dic.CreatedDate)
                            .Where(dic => dic.DicType == "ApproveMode")
                            .Select(dic => new ApproveModeDropDto
                            {
                                ApproveModeCode = dic.DicCode,
                                ApproveModeName = _lang.Locale == "zh-CN"
                                                  ? dic.DicNameCn
                                                  : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy(dept => dept.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 部门级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentLevelDropDto>> GetDepartmentLevelDropDown()
        {
            return await _db.Queryable<DepartmentLevelEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(deptlevel => deptlevel.SortOrder)
                            .Select(deptlevel => new DepartmentLevelDropDto
                            {
                                DepartmentLevelId = deptlevel.DepartmentLevelId,
                                DepartmentLevelName = _lang.Locale == "zh-CN"
                                                      ? deptlevel.DepartmentLevelNameCn
                                                      : deptlevel.DepartmentLevelNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDropDto>> GetUserPositionDropDown()
        {
            return await _db.Queryable<UserPositionEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(userpos => userpos.SortOrder)
                            .Select((userpos) => new UserPositionDropDto
                            {
                                PositionId = userpos.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? userpos.PositionNameCn
                                               : userpos.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<UserPositionEntity>((user, dept, userpos) => user.PositionId == userpos.PositionId)
                           .InnerJoin<UserLaborEntity>((user, dept, userpos, userlabor) => user.LaborId == userlabor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, dept, userpos, userlabor, nation) =>
                            user.Nationality == nation.NationId)
                           .Where((user, dept, userpos, userlabor, nation) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNo.Contains(getPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门Id
            if (!string.IsNullOrEmpty(getPage.DepartmentId) && long.Parse(getPage.DepartmentId) > -1)
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, userpos, userlabor, nation) => new { userpos.SortOrder, user.HireDate });

            var page = await query.Select((user, dept, userpos, userlabor, nation) => new UserWorkflowStepDto
            {
                                     UserId = user.UserId,
                                     UserNo = user.UserNo,
                                     UserName = _lang.Locale == "zh-CN"
                                                ? user.UserNameCn
                                                : user.UserNameEn,
                                     DepartmentName = _lang.Locale == "zh-CN"
                                                ? dept.DepartmentNameCn
                                                : dept.DepartmentNameEn,
                                     PositionName = _lang.Locale == "zh-CN"
                                                ? userpos.PositionNameCn
                                                : userpos.PositionNameEn,
                                     LaborName = _lang.Locale == "zh-CN"
                                                ? userlabor.LaborNameCn
                                                : userlabor.LaborNameEn,
                                     NationalityName = _lang.Locale == "zh-CN"
                                                ? nation.NationNameCn
                                                : nation.NationNameEn,
                                     IsAgent = user.IsAgent,
                                     IsApproval = user.IsApproval,
                                 }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserInfoDto>.Ok(page.Adapt<List<UserInfoDto>>(), totalCount, "");
        }

        /// <summary>
        /// 条件下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<WorkflowConditionDropDto>> GetConditionDropDown(long formTypeId)
        {
            return await _db.Queryable<WorkflowConditionEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(condition => condition.CreatedDate)
                            .Where(condition => condition.FormTypeId == formTypeId)
                            .Select(condition => new WorkflowConditionDropDto
                            {
                                ConditionId = condition.ConditionId,
                                ConditionName = _lang.Locale == "zh-CN"
                                                ? condition.ConditionNameCn
                                                : condition.ConditionNameEn,
                            }).ToListAsync();
        }
    }
}
