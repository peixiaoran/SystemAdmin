using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class WorkflowBranchStepService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowBranchStepService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowBranchStepRepository _workflowBranchStep;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowBranchStep";

        public WorkflowBranchStepService(CurrentUser loginuser, ILogger<WorkflowBranchStepService> logger, SqlSugarScope db, WorkflowBranchStepRepository workflowBranchStep, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowBranchStep = workflowBranchStep;
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
                var drop = await _workflowBranchStep.GetFormGroupDropDown();
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
                var drop = await _workflowBranchStep.GetFormTypeDropDown(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增分支步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowBranchStep(WorkflowBranchStepUpsert upsert)
        {
            try
            {
                // 分支步骤是否重复配置
                var isRepat = await _workflowBranchStep.BranchStepIsRepeat(long.Parse(upsert.BranchId), long.Parse(upsert.StepId));
                if (isRepat)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}BranchStepIsRepat"));
                }
                else
                {
                    var entity = new WorkflowBranchStepEntity()
                    {
                        BranchId = SnowFlakeSingle.Instance.NextId(),
                        StepId = long.Parse(upsert.StepId),
                        NextStepId = long.Parse(upsert.NextStepId),
                        SortOrder = upsert.SortOrder,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now,
                    };

                    await _db.BeginTranAsync();
                    var count = await _workflowBranchStep.InsertWorkflowBranchStep(entity);
                    await _db.CommitTranAsync();

                    return count >= 1
                            ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除分支步骤
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowBranchStep(string branchId, string stepId)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _workflowBranchStep.DeleteWorkflowBranchStep(long.Parse(branchId), long.Parse(stepId));
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改分支步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowBranchStep(WorkflowBranchStepUpsert upsert)
        {
            try
            {
                var entity = new WorkflowBranchStepEntity()
                {
                    BranchId = long.Parse(upsert.BranchId),
                    StepId = long.Parse(upsert.StepId),
                    NextStepId = long.Parse(upsert.NextStepId),
                    SortOrder = upsert.SortOrder,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowBranchStep.UpdateWorkflowBranchStep(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询分支步骤实体
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowBranchStepDto>> GetWorkflowBranchStepEntity(string branchId, string stepId)
        {
            try
            {
                var entity = await _workflowBranchStep.GetWorkflowBranchStepEntity(long.Parse(branchId), long.Parse(stepId));
                return Result<WorkflowBranchStepDto>.Ok(entity);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowBranchStepDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询分支步骤分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowBranchStepDto>> GetWorkflowBranchStepPage(GetWorkflowBranchStepPage getPage)
        {
            try
            {
                return await _workflowBranchStep.GetWorkflowBranchStepPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<WorkflowBranchStepDto>.Failure(500, ex.Message);
            }
        }
    }
}
