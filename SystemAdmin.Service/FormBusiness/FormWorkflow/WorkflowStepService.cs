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
                int insertStepCustomCount = 0;
                WorkflowStepEntity insertStep = new WorkflowStepEntity
                {
                    StepId = stepId,
                    FormTypeId = long.Parse(workflowStep.FormTypeId),
                    StepNameCn = workflowStep.StepNameCn,
                    StepNameEn = workflowStep.StepNameEn,
                    Assignment = workflowStep.Assignment,
                    IsStartStep = workflowStep.IsStartStep,
                    ArchitectureLevel = workflowStep.ArchitectureLevel,
                    ApproveMode = workflowStep.ApproveMode,
                    IsReminderEnabled = workflowStep.IsReminderEnabled,
                    ReminderIntervalMinutes = workflowStep.ReminderIntervalMinutes,
                    Description = workflowStep.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                await _db.BeginTranAsync();
                // 如果时起始步骤，则只新增审批步骤信息
                if (workflowStep.IsStartStep == 1)
                {
                    int insertStepCount = await _workflowStepRepository.InsertWorkflowStep(insertStep);
                    await _db.CommitTranAsync();
                    return insertStepCount >= 1
                            ? Result<int>.Ok(insertStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
                // 不是起始步骤，则新增审批步骤信息及对应的审批人选取方式数据
                else
                {
                    int insertStepCount = await _workflowStepRepository.InsertWorkflowStep(insertStep);
                    // 根据不同的审批人选取方式，新增对应的审批人选取方式数据
                    if (workflowStep.Assignment.MatchEnum(Assignment.Org))
                    {
                        WorkflowStepOrgEntity insertStepOrg = new WorkflowStepOrgEntity()
                        {
                            StepOrgId = SnowFlakeSingle.Instance.NextId(),
                            StepId = stepId,
                            DeptLeaveId = workflowStep.workflowStepOrgUpsert.DeptLeaveId,
                            PositionIds = workflowStep.workflowStepOrgUpsert.PositionIds,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        insertStepOrgCount = await _workflowStepRepository.InsertWorkflowStepOrg(insertStepOrg);
                    }
                    else if (workflowStep.Assignment.MatchEnum(Assignment.DeptUser))
                    {
                        WorkflowStepDeptUserEntity insertStepDeptUser = new WorkflowStepDeptUserEntity()
                        {
                            StepDeptUserId = SnowFlakeSingle.Instance.NextId(),
                            StepId = stepId,
                            DeptIds = workflowStep.workflowStepDeptUserUpsert.DeptIds,
                            PositionIds = workflowStep.workflowStepDeptUserUpsert.PositionIds,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        insertStepDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(insertStepDeptUser);
                    }
                    else if (workflowStep.Assignment.MatchEnum(Assignment.User))
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
                    else if (workflowStep.Assignment.MatchEnum(Assignment.Custom))
                    {
                        WorkflowStepCustomEntity inserrtStepCustom = new WorkflowStepCustomEntity()
                        {
                            StepCustomId = SnowFlakeSingle.Instance.NextId(),
                            StepId = stepId,
                            HandlerKey = workflowStep.workflowStepCustomUpsert.HandlerKey,
                            LogicalExplanation = workflowStep.workflowStepCustomUpsert.LogicalExplanation,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        insertStepCustomCount = await _workflowStepRepository.InsertWorkflowStepCustom(inserrtStepCustom);
                    }
                    await _db.CommitTranAsync();

                    return insertStepCount >= 1 && (insertStepOrgCount >= 1 || insertStepDeptUserCount >= 1 || insertStepUserCount >= 1 || insertStepCustomCount >= 1)
                            ? Result<int>.Ok(insertStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
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
                int deleteStepCustomCount = 0;

                // 删除所有审批步骤的流程配置
                deleteStepCount = await _workflowStepRepository.DeleteWorkflowStep(long.Parse(workflowStep.StepId));
                deleteStepOrgCount = await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                deleteStepDeptUserCount = await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                deleteStepUserCount = await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                deleteStepCustomCount = await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(workflowStep.StepId));

                return deleteStepCount >= 1 && (deleteStepOrgCount >= 1 || deleteStepDeptUserCount >= 1 || deleteStepUserCount >= 1 || deleteStepCustomCount >= 1)
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
                int updateStepCount = 0;
                int updateStepOrgCount = 0;
                int updateStepDeptUserCount = 0;
                int updateStepUserCount = 0;
                int updateStepCustomCount = 0;
                WorkflowStepEntity updateWorkflowStep = new WorkflowStepEntity
                {
                    StepId = long.Parse(workflowStep.StepId),
                    FormTypeId = long.Parse(workflowStep.FormTypeId),
                    StepNameCn = workflowStep.StepNameCn,
                    StepNameEn = workflowStep.StepNameEn,
                    IsStartStep = workflowStep.IsStartStep,
                    ArchitectureLevel = workflowStep.ArchitectureLevel,
                    Assignment = workflowStep.Assignment,
                    ApproveMode = workflowStep.ApproveMode,
                    IsReminderEnabled = workflowStep.IsReminderEnabled,
                    ReminderIntervalMinutes = workflowStep.ReminderIntervalMinutes,
                    Description = workflowStep.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.BeginTranAsync();

                // 如果时起始步骤，则只修改审批步骤信息删除其余审批人选取方式数据
                if (workflowStep.IsStartStep == 1)
                {
                    updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(workflowStep.StepId));
                    
                    await _db.CommitTranAsync();
                    return updateStepCount >= 1
                            ? Result<int>.Ok(updateStepCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }

                // 根据不同的审批人选取方式，删除其他选取方式数据，修改对应的审批人选取方式数据
                if (workflowStep.Assignment.MatchEnum(Assignment.Org))
                {
                    updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);
                    WorkflowStepOrgEntity updateStepOrg = new WorkflowStepOrgEntity()
                    {
                        StepOrgId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        DeptLeaveId = workflowStep.workflowStepOrgUpsert.DeptLeaveId,
                        PositionIds = workflowStep.workflowStepOrgUpsert.PositionIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.UpdateWorkflowStepOrg(updateStepOrg);
                }
                else if (workflowStep.Assignment.MatchEnum(Assignment.DeptUser))
                {
                    updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);
                    WorkflowStepDeptUserEntity updateStepDeptUser = new WorkflowStepDeptUserEntity()
                    {
                        StepDeptUserId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        DeptIds = workflowStep.workflowStepDeptUserUpsert.DeptIds,
                        PositionIds = workflowStep.workflowStepDeptUserUpsert.PositionIds,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(workflowStep.StepId));
                    updateStepDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(updateStepDeptUser);
                }
                else if (workflowStep.Assignment.MatchEnum(Assignment.User))
                {
                    updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);
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
                    await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(workflowStep.StepId));
                    updateStepUserCount = await _workflowStepRepository.InsertWorkflowStepUser(udpateStepUser);
                }
                else if (workflowStep.Assignment.MatchEnum(Assignment.Custom))
                {
                    updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(updateWorkflowStep);
                    WorkflowStepCustomEntity updateStepCustom = new WorkflowStepCustomEntity()
                    {
                        StepCustomId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(workflowStep.StepId),
                        HandlerKey = workflowStep.workflowStepCustomUpsert.HandlerKey,
                        LogicalExplanation = workflowStep.workflowStepCustomUpsert.LogicalExplanation,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(workflowStep.StepId));
                    await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(workflowStep.StepId));
                    updateStepCustomCount = await _workflowStepRepository.InsertWorkflowStepCustom(updateStepCustom);
                }
                await _db.CommitTranAsync();

                return updateStepCount >= 1 && (updateStepOrgCount >= 1 || updateStepDeptUserCount >= 1 || updateStepUserCount >= 1 || updateStepCustomCount >= 1)
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowStepPageDto>> GetWorkflowStepPage(GetWorkflowStepPage getPage)
        {
            try
            {
                return await _workflowStepRepository.GetWorkflowStepPage(getPage);
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
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowStepEntityDto>> GetWorkflowStepEntity(GetWorkflowStepEntity getEntity)
        {
            try
            {
                var workflowStepEntity = await _workflowStepRepository.GetWorkflowStepEntity(long.Parse(getEntity.StepId));

                workflowStepEntity.workflowStepOrgEntity = await _workflowStepRepository.GetWorkflowStepOrgEntity(long.Parse(getEntity.StepId));
                workflowStepEntity.workflowStepDeptUserEntity = await _workflowStepRepository.GetWorkflowStepDeptUserEntity(long.Parse(getEntity.StepId));
                workflowStepEntity.workflowStepUserEntity = await _workflowStepRepository.GetWorkflowStepUserEntity(long.Parse(getEntity.StepId));
                workflowStepEntity.workflowStepApproverCustomEntity = await _workflowStepRepository.GetWorkflowStepCustomEntity(long.Parse(getEntity.StepId));

                return Result<WorkflowStepEntityDto>.Ok(workflowStepEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowStepEntityDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetFormGroupDropDown();
                return Result<List<FormGroupDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(GetFormTypeDropDown getDrop)
        {
            try
            {
                var drop = await _workflowStepRepository.GetFormTypeDropDown(long.Parse(getDrop.FormGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 审批人选取方式下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetAssignmentDropDown();
                return Result<List<AssignmentDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<AssignmentDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetDepartmentLevelDropDown();
                return Result<List<DepartmentLevelDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserPositionDropDto>>> GetUserPositionDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetUserPositionDropDown();
                return Result<List<UserPositionDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 职业下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserLaborDropDto>>> GetLaborDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(drop, "");
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
        public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            try
            {
                var page = await _workflowStepRepository.GetUserInfoPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserInfoDto>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
