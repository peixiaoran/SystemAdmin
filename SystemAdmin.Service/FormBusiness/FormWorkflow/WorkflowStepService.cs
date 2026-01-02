using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormProcessConfig.Queries;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Common.Enums.FormBusiness;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class WorkflowStepService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowStepService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowStepRepository _workflowStepRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowStep";

        public WorkflowStepService(CurrentUser loginuser, ILogger<WorkflowStepService> logger, SqlSugarScope db, WorkflowStepRepository workflowStepRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowStepRepository = workflowStepRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增审批步骤信息
        /// </summary>
        /// <param name="workflowStep"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowStep(WorkflowStepUpsert workflowStep)
        {
            try
            {
                long stepId = SnowFlakeSingle.Instance.NextId();
                int insertStepOrgCount = 0;
                int insertStepDeptUserCount = 0;
                int insertStepUserCount = 0;
                int insertStepRuleCount = 0;
                WorkflowStepEntity insertStep = new WorkflowStepEntity
                {
                    StepId = stepId,
                    FormTypeId = long.Parse(workflowStep.FormTypeId),
                    StepNameCn = workflowStep.StepNameCn,
                    StepNameEn = workflowStep.StepNameEn,
                    Assignment = workflowStep.Assignment,
                    ArchitectureLevel = workflowStep.ArchitectureLevel,
                    IsStartStep = workflowStep.IsStartStep,
                    ApproveMode = workflowStep.ApproveMode,
                    Description = workflowStep.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.BeginTranAsync();
                int insertStepCount = await _workflowStepRepository.InsertWorkflowStep(insertStep);

                // 根据不同的审批人选取方式，新增对应的审批人选取方式数据
                if (workflowStep.Assignment.MatchEnum(StepAssignment.Org))
                {
                    WorkflowStepOrgEntity insertStepOrg = new WorkflowStepOrgEntity()
                    {
                        StepOrgId = SnowFlakeSingle.Instance.NextId(),
                        StepId = stepId,
                        DeptLeaveIds = workflowStep.workflowStepOrgUpsert.DeptLeaveIds,
                        PositionIds = workflowStep.workflowStepOrgUpsert.PositionIds,
                        LaborIds = workflowStep.workflowStepOrgUpsert.LaborIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    insertStepOrgCount = await _workflowStepRepository.InsertWorkflowStepOrg(insertStepOrg);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.DeptUser))
                {
                    WorkflowStepDeptUserEntity insertStepDeptUser = new WorkflowStepDeptUserEntity()
                    {
                        StepDeptUserId = SnowFlakeSingle.Instance.NextId(),
                        StepId = stepId,
                        DeptIds = workflowStep.workflowStepDeptUserUpsert.DeptIds,
                        PositionIds = workflowStep.workflowStepDeptUserUpsert.PositionIds,
                        LaborIds = workflowStep.workflowStepDeptUserUpsert.LaborIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    insertStepDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(insertStepDeptUser);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.User))
                {
                    WorkflowStepUserEntity insertStepUser = new WorkflowStepUserEntity()
                    {
                        StepUserId = SnowFlakeSingle.Instance.NextId(),
                        StepId = stepId,
                        UserIds = workflowStep.workflowStepUserUpsert.UserIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    insertStepUserCount = await _workflowStepRepository.InsertWorkflowStepUser(insertStepUser);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.Rule))
                {
                    WorkflowStepRuleEntity inserrtStepRule = new WorkflowStepRuleEntity()
                    {
                        StepRuleId = SnowFlakeSingle.Instance.NextId(),
                        StepId = stepId,
                        HandlerKey = workflowStep.workflowStepRuleUpsert.HandlerKey,
                        LogicalExplanation = workflowStep.workflowStepRuleUpsert.LogicalExplanation,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    insertStepRuleCount = await _workflowStepRepository.InsertWorkflowStepRule(inserrtStepRule);
                }
                await _db.CommitTranAsync();

                return insertStepCount >= 1 && (insertStepOrgCount >= 1 || insertStepDeptUserCount >= 1 || insertStepUserCount >= 1 || insertStepRuleCount >= 1)
                        ? Result<int>.Ok(insertStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除审批步骤信息
        /// </summary>
        /// <param name="workflowStep"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowStep(WorkflowStepUpsert workflowStep)
        {
            try
            {
                int deleteStepCount = 0;
                int deleteStepOrgCount = 0;
                int deleteStepDeptUserCount = 0;
                int deleteStepUserCount = 0;
                int deleteStepRuleCount = 0;
                deleteStepCount = await _workflowStepRepository.DeleteWorkflowStep(long.Parse(workflowStep.StepId));
                deleteStepOrgCount = await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                deleteStepDeptUserCount = await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                deleteStepUserCount = await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                deleteStepRuleCount = await _workflowStepRepository.DeleteWorkflowStepRule(long.Parse(workflowStep.StepId));

                return deleteStepCount >= 1 && (deleteStepOrgCount >= 1 || deleteStepDeptUserCount >= 1 || deleteStepUserCount >= 1 || deleteStepRuleCount >= 1)
                        ? Result<int>.Ok(deleteStepCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改审批步骤信息
        /// </summary>
        /// <param name="workflowStep"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowStep(WorkflowStepUpsert workflowStep)
        {
            try
            {
                int updateStepOrgCount = 0;
                int updateStepDeptUserCount = 0;
                int updateStepUserCount = 0;
                int updateStepRuleCount = 0;
                WorkflowStepEntity updateWorkflowStep = new WorkflowStepEntity
                {
                    StepId = long.Parse(workflowStep.StepId),
                    FormTypeId = long.Parse(workflowStep.FormTypeId),
                    StepNameCn = workflowStep.StepNameCn,
                    StepNameEn = workflowStep.StepNameEn,
                    ArchitectureLevel = workflowStep.ArchitectureLevel,
                    IsStartStep = workflowStep.IsStartStep,
                    Assignment = workflowStep.Assignment,
                    ApproveMode = workflowStep.ApproveMode,
                    Description = workflowStep.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.BeginTranAsync();
                int updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);

                // 根据不同的审批人选取方式，删除其他选取方式数据，修改对应的审批人选取方式数据
                if (workflowStep.Assignment.MatchEnum(StepAssignment.Org))
                {
                    WorkflowStepOrgEntity updateStepOrg = new WorkflowStepOrgEntity()
                    {
                        StepOrgId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        DeptLeaveIds = workflowStep.workflowStepOrgUpsert.DeptLeaveIds,
                        PositionIds = workflowStep.workflowStepOrgUpsert.PositionIds,
                        LaborIds = workflowStep.workflowStepOrgUpsert.LaborIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepRule(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.UpdateWorkflowStepOrg(updateStepOrg);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.DeptUser))
                {
                    WorkflowStepDeptUserEntity updateStepDeptUser = new WorkflowStepDeptUserEntity()
                    {
                        StepDeptUserId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        DeptIds = workflowStep.workflowStepDeptUserUpsert.DeptIds,
                        PositionIds = workflowStep.workflowStepDeptUserUpsert.PositionIds,
                        LaborIds = workflowStep.workflowStepDeptUserUpsert.LaborIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepRule(long.Parse(workflowStep.StepId));
                    updateStepDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(updateStepDeptUser);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.User))
                {
                    WorkflowStepUserEntity udpateStepUser = new WorkflowStepUserEntity()
                    {
                        StepUserId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        UserIds = workflowStep.workflowStepUserUpsert.UserIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepRule(long.Parse(workflowStep.StepId));
                    updateStepUserCount = await _workflowStepRepository.InsertWorkflowStepUser(udpateStepUser);
                }
                else if (workflowStep.Assignment.MatchEnum(StepAssignment.Rule))
                {
                    WorkflowStepRuleEntity updateStepRule = new WorkflowStepRuleEntity()
                    {
                        StepRuleId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        HandlerKey = workflowStep.workflowStepRuleUpsert.HandlerKey,
                        LogicalExplanation = workflowStep.workflowStepRuleUpsert.LogicalExplanation,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    updateStepRuleCount = await _workflowStepRepository.InsertWorkflowStepRule(updateStepRule);
                }
                await _db.CommitTranAsync();

                return updateStepCount >= 1 && (updateStepOrgCount >= 1 || updateStepDeptUserCount >= 1 || updateStepUserCount >= 1 || updateStepRuleCount >= 1)
                        ? Result<int>.Ok(updateStepCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询审批步骤分页
        /// </summary>
        /// <param name="getWorkflowStep"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowStepPageDto>> GetWorkflowStepPage(GetWorkflowStepPage getWorkflowStep)
        {
            try
            {
                return await _workflowStepRepository.GetWorkflowStepPage(getWorkflowStep);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<WorkflowStepPageDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询审批步骤实体
        /// </summary>
        /// <param name="getWorkflowStep"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowStepEntityDto>> GetWorkflowStepEntity(GetWorkflowStepEntity getWorkflowStep)
        {
            try
            {
                var workflowStepEntity = await _workflowStepRepository.GetWorkflowStepEntity(long.Parse(getWorkflowStep.StepId));
                workflowStepEntity.workflowStepOrgEntity = await _workflowStepRepository.GetWorkflowStepOrgEntity(long.Parse(getWorkflowStep.StepId));
                workflowStepEntity.workflowStepDeptUserEntity = await _workflowStepRepository.GetWorkflowStepDeptUserEntity(long.Parse(getWorkflowStep.StepId));
                workflowStepEntity.workflowStepUserEntity = await _workflowStepRepository.GetWorkflowStepUserEntity(long.Parse(getWorkflowStep.StepId));
                workflowStepEntity.workflowStepApproverRuleEntity = await _workflowStepRepository.GetWorkflowStepRuleEntity(long.Parse(getWorkflowStep.StepId));
                return Result<WorkflowStepEntityDto>.Ok(workflowStepEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowStepEntityDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单组别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            try
            {
                var formGroupDropDown = await _workflowStepRepository.GetFormGroupDropDown();
                return Result<List<FormGroupDropDto>>.Ok(formGroupDropDown);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单类别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(GetFormTypeDropDown getFormTypeDropDown)
        {
            try
            {
                var formTypeDropDown = await _workflowStepRepository.GetFormTypeDropDown(long.Parse(getFormTypeDropDown.FormGroupId));
                return Result<List<FormTypeDropDto>>.Ok(formTypeDropDown);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 审批人选取方式下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDropDown()
        {
            try
            {
                var assigDropDown = await _workflowStepRepository.GetAssignmentDropDown();
                return Result<List<AssignmentDropDto>>.Ok(assigDropDown);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<AssignmentDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var deptDrop = await _workflowStepRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(deptDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门级别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            try
            {
                var deptLevelDrop = await _workflowStepRepository.GetDepartmentLevelDropDown();
                return Result<List<DepartmentLevelDropDto>>.Ok(deptLevelDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            try
            {
                var userPositionDrop = await _workflowStepRepository.GetUserPositionDropDown();
                return Result<List<UserPositionDropDto>>.Ok(userPositionDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            try
            {
                var laborDrop = await _workflowStepRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(laborDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询用户信息分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<Model.FormBusiness.FormWorkflow.Dto.UserInfoDto>> GetUserInfoPage(GetUserInfoPage getUserInfoPage)
        {
            try
            {
                var userPage = await _workflowStepRepository.GetUserInfoPage(getUserInfoPage);
                return userPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<Model.FormBusiness.FormWorkflow.Dto.UserInfoDto>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
