using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
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
        private readonly WorkflowStepRepository _workflowStepRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowStep";

        public WorkflowStepService(CurrentUser loginuser, ILogger<WorkflowStepService> logger, SqlSugarScope db, WorkflowStepRepository workflowStepRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowStepRepo = workflowStepRepo;
            _localization = localization;
        }

        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetFormGroupDrop();
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
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDrop(string formGroupId)
        {
            try
            {
                var drop = await _workflowStepRepo.GetFormTypeDrop(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 步骤指派规则下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<AssignmentDropDto>>> GetAssignmentDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetAssignmentDrop();
                return Result<List<AssignmentDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<AssignmentDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 步骤签核方式下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ApproveModeDropDto>>> GetApproveModeDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetApproveModeDrop();
                return Result<List<ApproveModeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<ApproveModeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 部门级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetDepartmentLevelDrop();
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
        public async Task<Result<List<PositionDropDto>>> GetPositionDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetPositionDrop();
                return Result<List<PositionDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<PositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            try
            {
                var drop = await _workflowStepRepo.GetDepartmentDrop();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工信息分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            try
            {
                var page = await _workflowStepRepo.GetUserInfoPage(getPage);
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowStep(WorkflowStepUpsert upsert)
        {
            try
            {
                long stepId = SnowFlakeSingle.Instance.NextId();
                int insertOrgCount = 0;
                int insertDeptUserCount = 0;
                int insertUserCount = 0;
                int insertCustomCount = 0;

                var stepEntity = new WorkflowStepEntity
                {
                    StepId = stepId,
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    StepNameCn = upsert.StepNameCn,
                    StepNameEn = upsert.StepNameEn,
                    IsStartStep = upsert.IsStartStep,
                    Assignment = upsert.Assignment,
                    ApproveMode = upsert.ApproveMode,
                    IsReminderEnabled = upsert.IsReminderEnabled,
                    ReminderIntervalMinutes = upsert.ReminderIntervalMinutes,
                    SortOrder = upsert.SortOrder,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                // 如果时起始步骤，则只新增审批步骤信息
                if (upsert.IsStartStep == 1)
                {
                    int insertStepCount = await _workflowStepRepo.InsertWorkflowStep(stepEntity);
                    await _db.CommitTranAsync();
                    return insertStepCount >= 1
                            ? Result<int>.Ok(insertStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
                // 不是起始步骤，则新增审批步骤信息及对应的步骤指派规则
                else
                {
                    int insertStepCount = await _workflowStepRepo.InsertWorkflowStep(stepEntity);
                    // 根据不同的步骤指派规则，新增对应的步骤指派规则
                    if (upsert.Assignment.MatchEnum(Assignment.Org))
                    {
                        var orgEntity = new WorkflowStepOrgEntity()
                        {
                            StepId = stepId,
                            DeptLeaveId = long.Parse(upsert.stepOrgUpsert.DeptLeaveId),
                            PositionId = long.Parse(upsert.stepOrgUpsert.PositionId),
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now
                        };
                        insertOrgCount = await _workflowStepRepo.InsertWorkflowStepOrg(orgEntity);
                    }
                    else if (upsert.Assignment.MatchEnum(Assignment.DeptUser))
                    {
                        var stepDeptUserEntity = new WorkflowStepDeptUserEntity()
                        {
                            StepId = stepId,
                            DepartmentId = long.Parse(upsert.stepDeptUserUpsert.DepartmentId),
                            PositionId = long.Parse(upsert.stepDeptUserUpsert.PositionId),
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now
                        };
                        insertDeptUserCount = await _workflowStepRepo.InsertWorkflowStepDeptUser(stepDeptUserEntity);
                    }
                    else if (upsert.Assignment.MatchEnum(Assignment.User))
                    {
                        var userEntity = new WorkflowStepUserEntity()
                        {
                            StepId = stepId,
                            DepartmentId = long.Parse(upsert.stepUserUpsert.DepartmentId),
                            UserId = long.Parse(upsert.stepUserUpsert.UserId),
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now
                        };
                        insertUserCount = await _workflowStepRepo.InsertWorkflowStepUser(userEntity);
                    }
                    else if (upsert.Assignment.MatchEnum(Assignment.Custom))
                    {
                        var customEntity = new WorkflowStepCustomEntity()
                        {
                            StepId = stepId,
                            HandlerKey = upsert.stepCustomUpsert.HandlerKey,
                            LogicalExplanation = upsert.stepCustomUpsert.LogicalExplanation,
                            CreatedBy = _loginuser.UserId,
                            CreatedDate = DateTime.Now
                        };
                        insertCustomCount = await _workflowStepRepo.InsertWorkflowStepCustom(customEntity);
                    }
                    await _db.CommitTranAsync();

                    return insertStepCount >= 1 && (insertOrgCount >= 1 || insertDeptUserCount >= 1 || insertUserCount >= 1 || insertCustomCount >= 1)    
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
        /// 删除步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowStep(string stepId)
        {
            try
            {
                var isExist = await _workflowStepRepo.GetWorkflowRuleStepIsExist(long.Parse(stepId));
                if (isExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}Exist"));
                }
                else
                {
                    // 删除所有步骤的配置
                    int delStepCount = await _workflowStepRepo.DeleteWorkflowStep(long.Parse(stepId));
                    int delOrgCount = await _workflowStepRepo.DeleteWorkflowStepOrg(long.Parse(stepId));
                    int delDeptUserCount = await _workflowStepRepo.DeleteWorkflowStepDeptUser(long.Parse(stepId));
                    int delUserCount = await _workflowStepRepo.DeleteWorkflowStepUser(long.Parse(stepId));
                    int delCustomCount = await _workflowStepRepo.DeleteWorkflowStepCustom(long.Parse(stepId));

                    return delStepCount >= 1 && (delOrgCount >= 1 || delDeptUserCount >= 1 || delUserCount >= 1 || delCustomCount >= 1)
                            ? Result<int>.Ok(delStepCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
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
        /// 修改步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowStep(WorkflowStepUpsert upsert)
        {
            try
            {
                int updateStepCount = 0;
                int insertStepOrgCount = 0;
                int insertStepDeptUserCount = 0;
                int insertStepUserCount = 0;
                int insertStepCustomCount = 0;

                var stepEntity = new WorkflowStepEntity
                {
                    StepId = long.Parse(upsert.StepId),
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    StepNameCn = upsert.StepNameCn,
                    StepNameEn = upsert.StepNameEn,
                    IsStartStep = upsert.IsStartStep,
                    Assignment = upsert.Assignment,
                    ApproveMode = upsert.ApproveMode,
                    IsReminderEnabled = upsert.IsReminderEnabled,
                    ReminderIntervalMinutes = upsert.ReminderIntervalMinutes,
                    SortOrder = upsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                updateStepCount = await _workflowStepRepo.UpdateWorkflowStep(stepEntity);
                await _workflowStepRepo.DeleteWorkflowStepDeptUser(long.Parse(upsert.StepId));
                await _workflowStepRepo.DeleteWorkflowStepUser(long.Parse(upsert.StepId));
                await _workflowStepRepo.DeleteWorkflowStepCustom(long.Parse(upsert.StepId));
                await _workflowStepRepo.DeleteWorkflowStepOrg(long.Parse(upsert.StepId));
                // 如果时开始步骤，则只修改步骤信息
                if (upsert.IsStartStep == 1)
                {
                    await _db.CommitTranAsync();
                    return updateStepCount >= 1
                            ? Result<int>.Ok(updateStepCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }

                // 根据不同的步骤指派规则，删除其他选人方式数据，再新增步骤指派规则
                if (upsert.Assignment.MatchEnum(Assignment.Org))
                {
                    var stepOrgEntity = new WorkflowStepOrgEntity()
                    {
                        StepId = long.Parse(upsert.StepId),
                        DeptLeaveId = long.Parse(upsert.stepOrgUpsert.DeptLeaveId),
                        PositionId = long.Parse(upsert.stepOrgUpsert.PositionId),
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    insertStepOrgCount = await _workflowStepRepo.InsertWorkflowStepOrg(stepOrgEntity);
                }
                else if (upsert.Assignment.MatchEnum(Assignment.DeptUser))
                {
                    var stepDeptUserEntity = new WorkflowStepDeptUserEntity()
                    {
                        StepId = long.Parse(upsert.StepId),
                        DepartmentId = long.Parse(upsert.stepDeptUserUpsert.DepartmentId),
                        PositionId = long.Parse(upsert.stepDeptUserUpsert.PositionId),
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    insertStepDeptUserCount = await _workflowStepRepo.InsertWorkflowStepDeptUser(stepDeptUserEntity);
                }
                else if (upsert.Assignment.MatchEnum(Assignment.User))
                {
                    var stepUserEntity = new WorkflowStepUserEntity()
                    {
                        StepId = long.Parse(upsert.StepId),
                        DepartmentId = long.Parse(upsert.stepUserUpsert.DepartmentId),
                        UserId = long.Parse(upsert.stepUserUpsert.UserId),
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    insertStepUserCount = await _workflowStepRepo.InsertWorkflowStepUser(stepUserEntity);
                }
                else if (upsert.Assignment.MatchEnum(Assignment.Custom))
                {
                    var stepCustomEntity = new WorkflowStepCustomEntity()
                    {
                        StepId = long.Parse(upsert.StepId),
                        HandlerKey = upsert.stepCustomUpsert.HandlerKey,
                        LogicalExplanation = upsert.stepCustomUpsert.LogicalExplanation,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now
                    };
                    insertStepCustomCount = await _workflowStepRepo.InsertWorkflowStepCustom(stepCustomEntity);
                }
                await _db.CommitTranAsync();

                return updateStepCount >= 1 && (insertStepOrgCount >= 1 || insertStepDeptUserCount >= 1 || insertStepUserCount >= 1 || insertStepCustomCount >= 1)
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
        /// 查询步骤列表
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowStepListDto>>> GetWorkflowStepList(string formTypeId)
        {
            try
            {
                return await _workflowStepRepo.GetWorkflowStepList(formTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<WorkflowStepListDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询步骤实体
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowStepDto>> GetWorkflowStepEntity(string stepId)
        {
            try
            {
                var entity = await _workflowStepRepo.GetWorkflowStepEntity(long.Parse(stepId));

                entity.workflowStepOrgDto = await _workflowStepRepo.GetWorkflowStepOrgEntity(long.Parse(stepId));
                entity.workflowStepDeptUserDto = await _workflowStepRepo.GetWorkflowStepDeptUserEntity(long.Parse(stepId));
                entity.workflowStepUserDto = await _workflowStepRepo.GetWorkflowStepUserEntity(long.Parse(stepId));
                entity.workflowStepCustomDto = await _workflowStepRepo.GetWorkflowStepCustomEntity(long.Parse(stepId));

                return Result<WorkflowStepDto>.Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowStepDto>.Failure(500, ex.Message);
            }
        }
    }
}
