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
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(string formGroupId)
        {
            try
            {
                var drop = await _workflowStepRepository.GetFormTypeDropDown(long.Parse(formGroupId));
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
        /// 步骤签核方式下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ApproveModeDropDto>>> GetApproveModeDropDown()
        {
            try
            {
                var drop = await _workflowStepRepository.GetApproveModeDropDown();
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
                    FormGroupId = long.Parse(upsert.FormGroupId),
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
                    int insertStepCount = await _workflowStepRepository.InsertWorkflowStep(stepEntity);
                    await _db.CommitTranAsync();
                    return insertStepCount >= 1
                            ? Result<int>.Ok(insertStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
                // 不是起始步骤，则新增审批步骤信息及对应的步骤指派规则
                else
                {
                    int insertStepCount = await _workflowStepRepository.InsertWorkflowStep(stepEntity);
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
                        insertOrgCount = await _workflowStepRepository.InsertWorkflowStepOrg(orgEntity);
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
                        insertDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(stepDeptUserEntity);
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
                        insertUserCount = await _workflowStepRepository.InsertWorkflowStepUser(userEntity);
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
                        insertCustomCount = await _workflowStepRepository.InsertWorkflowStepCustom(customEntity);
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
                // 删除所有步骤的配置
                int delStepCount = await _workflowStepRepository.DeleteWorkflowStep(long.Parse(stepId));
                int delOrgCount = await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(stepId));
                int delDeptUserCount = await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(stepId));
                int delUserCount = await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(stepId));
                int delCustomCount = await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(stepId));
                int delBranchCount = await _workflowStepRepository.DeleteWorkflowStepBranch(long.Parse(stepId));
                int updateStepBranchCount = await _workflowStepRepository.UpdateWorkflowStepBranch(long.Parse(stepId));

                return delStepCount >= 1 && (delOrgCount >= 1 || delDeptUserCount >= 1 || delUserCount >= 1 || delCustomCount >= 1 || delBranchCount >= 1 || updateStepBranchCount >= 1)
                        ? Result<int>.Ok(delStepCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
                    FormGroupId = long.Parse(upsert.FormGroupId),
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
                updateStepCount = await _workflowStepRepository.UpdateWorkflowStep(stepEntity);
                await _workflowStepRepository.DeleteWorkflowStepDeptUser(long.Parse(upsert.StepId));
                await _workflowStepRepository.DeleteWorkflowStepUser(long.Parse(upsert.StepId));
                await _workflowStepRepository.DeleteWorkflowStepCustom(long.Parse(upsert.StepId));
                await _workflowStepRepository.DeleteWorkflowStepOrg(long.Parse(upsert.StepId));
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
                    insertStepOrgCount = await _workflowStepRepository.InsertWorkflowStepOrg(stepOrgEntity);
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
                    insertStepDeptUserCount = await _workflowStepRepository.InsertWorkflowStepDeptUser(stepDeptUserEntity);
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
                    insertStepUserCount = await _workflowStepRepository.InsertWorkflowStepUser(stepUserEntity);
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
                    insertStepCustomCount = await _workflowStepRepository.InsertWorkflowStepCustom(stepCustomEntity);
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
                return await _workflowStepRepository.GetWorkflowStepList(formTypeId);
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
                var entity = await _workflowStepRepository.GetWorkflowStepEntity(long.Parse(stepId));

                entity.workflowStepOrgDto = await _workflowStepRepository.GetWorkflowStepOrgEntity(long.Parse(stepId)) ?? new WorkflowStepOrgDto();
                entity.workflowStepDeptUserDto = await _workflowStepRepository.GetWorkflowStepDeptUserEntity(long.Parse(stepId)) ?? new WorkflowStepDeptUserDto();
                entity.workflowStepUserDto = await _workflowStepRepository.GetWorkflowStepUserEntity(long.Parse(stepId)) ?? new WorkflowStepUserDto();
                entity.workflowStepCustomDto = await _workflowStepRepository.GetWorkflowStepCustomEntity(long.Parse(stepId)) ?? new WorkflowStepCustomDto();

                return Result<WorkflowStepDto>.Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowStepDto>.Failure(500, ex.Message);
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
