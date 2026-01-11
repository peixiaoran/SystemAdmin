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
        /// 新增审批步骤信息
        /// </summary>
        /// <param name="workflowStep"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStep(WorkflowStepEntity workflowStep)
        {
            return await _db.Insertable(workflowStep).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增审批步骤组织架构来源
        /// </summary>
        /// <param name="workflowStepOrg"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepOrg(WorkflowStepOrgEntity workflowStepOrg)
        {
            return await _db.Insertable(workflowStepOrg).ExecuteCommandAsync();
        }

        /// <summary>
        ///  新增审批步骤指定部门员工级别来源
        /// </summary>
        /// <param name="workflowStepDeptUser"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepDeptUser(WorkflowStepDeptUserEntity workflowStepDeptUser)
        {
            return await _db.Insertable(workflowStepDeptUser).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增审批步骤指定员工来源
        /// </summary>
        /// <param name="workflowStepUser"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepUser(WorkflowStepUserEntity workflowStepUser)
        {
            return await _db.Insertable(workflowStepUser).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增审批步骤自定义来源
        /// </summary>
        /// <param name="workflowStepCustom"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowStepCustom(WorkflowStepCustomEntity workflowStepCustom)
        {
            return await _db.Insertable(workflowStepCustom).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除审批步骤信息
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowStep(long stepId)
        {
            return await _db.Deleteable<WorkflowStepEntity>()
                            .Where(step => step.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除审批步骤组织架构来源
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
        /// 删除审批步骤指定部门员工级别来源
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
        /// 删除审批步骤指定员工来源
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
        /// 删除审批步骤自定义来源
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
        /// 修改审批步骤信息
        /// </summary>
        /// <param name="workflowStep"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStep(WorkflowStepEntity workflowStep)
        {
            return await _db.Updateable(workflowStep)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.FormTypeId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == workflowStep.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改审批步骤组织架构来源
        /// </summary>
        /// <param name="workflowStepOrg"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStepOrg(WorkflowStepOrgEntity workflowStepOrg)
        {
            return await _db.Updateable(workflowStepOrg)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == workflowStepOrg.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改审批步骤指定部门员工级别来源
        /// </summary>
        /// <param name="workflowStepDeptUser"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStepDeptUser(WorkflowStepDeptUserEntity workflowStepDeptUser)
        {
            return await _db.Updateable(workflowStepDeptUser)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == workflowStepDeptUser.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改审批步骤指定员工来源
        /// </summary>
        /// <param name="workflowStepUser"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStepUser(WorkflowStepUserEntity workflowStepUser)
        {
            return await _db.Updateable(workflowStepUser)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == workflowStepUser.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改审批步骤自定义来源
        /// </summary>
        /// <param name="workflowStepCustom"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStepCustom(WorkflowStepCustomEntity workflowStepCustom)
        {
            return await _db.Updateable(workflowStepCustom)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == workflowStepCustom.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询审批步骤分页
        /// </summary>
        /// <param name="getWorkflowStep"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowStepPageDto>> GetWorkflowStepPage(GetWorkflowStepPage getWorkflowStep)
        {
            RefAsync<int> totalCount = 0;
            var workflowStepPage = await _db.Queryable<WorkflowStepEntity>()
                                            .With(SqlWith.NoLock)
                                            .InnerJoin<DictionaryInfoEntity>((stepinfo, assigndic) => assigndic.DicType == " ApproverAssignment" && stepinfo.Assignment == assigndic.DicCode)
                                            .Where((stepinfo, assigndic) => stepinfo.FormTypeId == long.Parse(getWorkflowStep.FormTypeId))
                                            .OrderBy((stepinfo, assigndic) => stepinfo.CreatedDate)
                                            .Select((stepinfo, assigndic) => new WorkflowStepPageDto()
                                            {
                                                StepId = stepinfo.StepId,
                                                StepName = _lang.Locale == "zh-CN"
                                                           ? stepinfo.StepNameCn
                                                           : stepinfo.StepNameEn,
                                                Assignment = int.Parse(stepinfo.Assignment),
                                                AssignmentName = _lang.Locale == "zh-CN"
                                                           ? assigndic.DicNameCn
                                                           : assigndic.DicNameEn,
                                                Description = stepinfo.Description,
                                            }).ToPageListAsync(getWorkflowStep.PageIndex, getWorkflowStep.PageSize, totalCount);
            return ResultPaged<WorkflowStepPageDto>.Ok(workflowStepPage.Adapt<List<WorkflowStepPageDto>>(), totalCount);
        }

        /// <summary>
        /// 查询审批步骤实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepEntityDto> GetWorkflowStepEntity(long stepId)
        {
            var workflowStepEntity = await _db.Queryable<WorkflowStepEntity>()
                                              .With(SqlWith.NoLock)
                                              .Where(step => step.FormTypeId == stepId)
                                              .OrderBy(step => step.CreatedDate)
                                              .ToListAsync();
            return workflowStepEntity.Adapt<WorkflowStepEntityDto>();
        }

        /// <summary>
        /// 查询签核步骤组织架构来源实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepOrgEntity> GetWorkflowStepOrgEntity(long stepId)
        {
            var stepOrgEntity = await _db.Queryable<WorkflowStepOrgEntity>()
                                         .With(SqlWith.NoLock)
                                         .Where(steporg => steporg.StepId == stepId)
                                         .ToListAsync();
            return stepOrgEntity.Adapt<WorkflowStepOrgEntity>();
        }

        /// <summary>
        /// 查询签核步骤指定部门员工级别来源实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepDeptUserEntity> GetWorkflowStepDeptUserEntity(long stepId)
        {
            var stepDeptUserEntity = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                              .With(SqlWith.NoLock)   
                                              .Where(stepdeptcri => stepdeptcri.StepId == stepId)
                                              .ToListAsync();
            return stepDeptUserEntity.Adapt<WorkflowStepDeptUserEntity>();
        }

        /// <summary>
        /// 查询签核步骤指定员工来源表实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepUserEntity> GetWorkflowStepUserEntity(long stepId)
        {
            var stepUserEntity = await _db.Queryable<WorkflowStepUserEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(stepuser => stepuser.StepId == stepId)
                                          .ToListAsync();
            return stepUserEntity.Adapt<WorkflowStepUserEntity>();
        }

        /// <summary>
        /// 查询签核步骤指定员工来源表实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepCustomEntity> GetWorkflowStepCustomEntity(long stepId)
        {
            var stepCustomEntity = await _db.Queryable<WorkflowStepCustomEntity>()
                                            .With(SqlWith.NoLock)
                                            .Where(stepappcustom => stepappcustom.StepId == stepId)
                                            .ToListAsync();
            return stepCustomEntity.Adapt<WorkflowStepCustomEntity>();
        }

        /// <summary>
        /// 表单组别下拉框
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
        /// 表单类型下拉框
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
        /// 审批人选取方式下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssignmentDropDto>> GetAssignmentDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock) 
                            .Where(dic => dic.DicType == "StepAssignment")
                            .Select(dic => new AssignmentDropDto
                            {
                                AssignmentCode = dic.DicCode,
                                AssignmentName = _lang.Locale == "zh-CN"
                                                 ? dic.DicNameCn
                                                 : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉框
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
        /// 部门级别下拉框
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
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDropDto>> GetUserPositionDropDown()
        {
            return await _db.Queryable<UserPositionEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(userpos => userpos.CreatedDate)
                            .Select((userpos) => new UserPositionDropDto
                            {
                                PositionId = userpos.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? userpos.PositionNameCn
                                               : userpos.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDropDown()
        {
            return await _db.Queryable<UserLaborEntity>()
                            .With(SqlWith.NoLock)
                            .Select(userlabor => new UserLaborDropDto
                            {
                                LaborId = userlabor.LaborId,
                                LaborName = _lang.Locale == "zh-CN"
                                            ? userlabor.LaborNameCn
                                            : userlabor.LaborNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询选取员工分页
        /// </summary>
        /// <param name="getUserInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getUserInfoPage)
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
            if (!string.IsNullOrEmpty(getUserInfoPage.UserNo))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNo.Contains(getUserInfoPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserInfoPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNameCn.Contains(getUserInfoPage.UserName) ||
                    user.UserNameEn.Contains(getUserInfoPage.UserName));
            }
            // 部门Id（仅在工号与姓名都为空时）
            if (!string.IsNullOrEmpty(getUserInfoPage.DepartmentId)
                && string.IsNullOrEmpty(getUserInfoPage.UserNo)
                && string.IsNullOrEmpty(getUserInfoPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.DepartmentId == long.Parse(getUserInfoPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, userpos, userlabor, nation) => new { userpos.PositionOrderBy, user.HireDate });

            var userPage = await query
            .Select((user, dept, userpos, userlabor, nation) => new UserAgentDto
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
            }).ToPageListAsync(getUserInfoPage.PageIndex, getUserInfoPage.PageSize, totalCount);
            return ResultPaged<UserInfoDto>.Ok(userPage.Adapt<List<UserInfoDto>>(), totalCount, "");
        }
    }
}
