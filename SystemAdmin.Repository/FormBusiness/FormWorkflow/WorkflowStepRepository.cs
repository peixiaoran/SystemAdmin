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
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormGroupDropDto>> GetFormGroupDrop()
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
        /// <param name="fromGroupId"></param>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDrop(long fromGroupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormGroupId == fromGroupId)
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
        public async Task<List<AssignmentDropDto>> GetAssignmentDrop()
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
        /// 步骤签核方式下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApproveModeDropDto>> GetApproveModeDrop()
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
        public async Task<List<DepartmentDropDto>> GetDepartmentDrop()
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
        public async Task<List<DepartmentLevelDropDto>> GetDepartmentLevelDrop()
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
        public async Task<List<Model.SystemBasicMgmt.SystemBasicData.Dto.PositionDropDto>> GetPositionDrop()
        {
            return await _db.Queryable<PositionInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(position => position.SortOrder)
                            .Select((position) => new Model.SystemBasicMgmt.SystemBasicData.Dto.PositionDropDto
                            {
                                PositionId = position.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? position.PositionNameCn
                                               : position.PositionNameEn
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
                           .InnerJoin<PositionInfoEntity>((user, dept, position) => user.PositionId == position.PositionId)
                           .InnerJoin<UserLaborEntity>((user, dept, position, labor) => user.LaborId == labor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, dept, position, labor, nation) =>
                            user.Nationality == nation.NationId)
                           .Where((user, dept, position, labor, nation) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((user, dept, position, labor, nation) => user.UserNo.Contains(getPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where((user, dept, position, labor, nation) =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门Id
            if (!string.IsNullOrEmpty(getPage.DepartmentId) && long.Parse(getPage.DepartmentId) > -1)
            {
                query = query.Where((user, dept, position, labor, nation) =>
                    user.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, position, labor, nation) => new { position.SortOrder, user.HireDate });

            var page = await query.Select((user, dept, position, labor, nation) => new UserInfoDto
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
                           ? position.PositionNameCn
                           : position.PositionNameEn,
                LaborName = _lang.Locale == "zh-CN"
                           ? labor.LaborNameCn
                           : labor.LaborNameEn,
                NationalityName = _lang.Locale == "zh-CN"
                           ? nation.NationNameCn
                           : nation.NationNameEn,
                IsAgent = user.IsAgent,
                IsApproval = user.IsApproval,
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserInfoDto>.Ok(page, totalCount, "");
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
        /// 查询步骤是否有规则配置
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<bool> GetWorkflowRuleStepIsExist(long stepId)
        {
            return await _db.Queryable<WorkflowRuleStepEntity>()
                            .Where(rule => rule.CurrentStepId == stepId || rule.NextStepId == stepId)
                            .AnyAsync();
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
            return await _db.Deleteable<WorkflowStepDeptUserEntity>()
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
        /// 修改步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowStep(WorkflowStepEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(stepinfo => new
                            {
                                stepinfo.StepId,
                                stepinfo.FormTypeId,
                                stepinfo.CreatedBy,
                                stepinfo.CreatedDate,
                            }).Where(stepinfo => stepinfo.StepId == entity.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询步骤及分支列表
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowStepListDto>>> GetWorkflowStepList(string formTypeId)
        {
            var list = await _db.Queryable<WorkflowStepEntity>()
                                .With(SqlWith.NoLock)
                                .InnerJoin<DictionaryInfoEntity>((stepinfo, dic) => dic.DicType == "Assignment" && stepinfo.Assignment == dic.DicCode)
                                .Where((stepinfo, dic) => stepinfo.FormTypeId == long.Parse(formTypeId))
                                .OrderBy((stepinfo, dic) => stepinfo.SortOrder)
                                .Select((stepinfo, dic) => new WorkflowStepListDto()
                                {
                                    StepId = stepinfo.StepId,
                                    StepNameCn = stepinfo.StepNameCn,
                                    StepNameEn = stepinfo.StepNameEn,
                                    Assignment = dic.DicCode,
                                    AssignmentName = _lang.Locale == "zh-CN"
                                                     ? dic.DicNameCn
                                                     : dic.DicNameEn,
                                }).ToListAsync();
            return Result<List<WorkflowStepListDto>>.Ok(list.Adapt<List<WorkflowStepListDto>>());
        }

        /// <summary>
        /// 查询步骤实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepDto> GetWorkflowStepEntity(long stepId)
        {
            var entity = await _db.Queryable<WorkflowStepEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(stepinfo => stepinfo.StepId == stepId)
                                  .OrderBy(stepinfo => stepinfo.CreatedDate)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepDto>();
        }

        /// <summary>
        /// 查询步骤-组织架构实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepOrgDto> GetWorkflowStepOrgEntity(long stepId)
        {
            var entity = await _db.Queryable<WorkflowStepOrgEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(steporg => steporg.StepId == stepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepOrgDto>();
        }

        /// <summary>
        /// 查询步骤-指定部门员工级别实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepDeptUserDto> GetWorkflowStepDeptUserEntity(long stepId)
        {
            var entity = await _db.Queryable<WorkflowStepDeptUserEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(stepdeptuser => stepdeptuser.StepId == stepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepDeptUserDto>();
        }

        /// <summary>
        /// 查询步骤-指定员工实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepUserDto> GetWorkflowStepUserEntity(long stepId)
        {
            var entity = await _db.Queryable<WorkflowStepUserEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(stepuser => stepuser.StepId == stepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepUserDto>();
        }

        /// <summary>
        /// 查询步骤-自定义实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowStepCustomDto> GetWorkflowStepCustomEntity(long stepId)
        {
            var entity = await _db.Queryable<WorkflowStepCustomEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(stepcustom => stepcustom.StepId == stepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowStepCustomDto>();
        }
    }
}
