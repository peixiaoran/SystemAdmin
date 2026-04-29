using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class WorkflowRuleStepService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowRuleStepService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowRuleStepRepository _workflowRuleStepRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowRuleStep";

        public WorkflowRuleStepService(CurrentUser loginuser, ILogger<WorkflowRuleStepService> logger, SqlSugarScope db, WorkflowRuleStepRepository workflowRuleStepRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowRuleStepRepo = workflowRuleStepRepo;
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
                var drop = await _workflowRuleStepRepo.GetFormGroupDrop();
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
                var drop = await _workflowRuleStepRepo.GetFormTypeDrop(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 规则下拉
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowRuleDropDto>>> GetWorkflowRuleDrop(string formTypeId)
        {
            try
            {
                var drop = await _workflowRuleStepRepo.GetWorkflowRuleDrop(long.Parse(formTypeId));
                return Result<List<WorkflowRuleDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<WorkflowRuleDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增规则步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowRuleStep(WorkflowRuleStepUpsert upsert)
        {
            try
            {
                // 规则是否重复配置
                var isRepat = await _workflowRuleStepRepo.RuleStepIsRepeat(long.Parse(upsert.RuleId), long.Parse(upsert.CurrentStepId), long.Parse(upsert.NextStepId));
                if (isRepat)
                {
                    return Result<int>.Failure(400, _localization.ReturnMsg($"{_this}Repat"));
                }
                else
                {
                    var entity = new WorkflowRuleStepEntity()
                    {
                        RuleId = long.Parse(upsert.RuleId),
                        CurrentStepId = long.Parse(upsert.CurrentStepId),
                        NextStepId = long.Parse(upsert.NextStepId),
                        SortOrder = upsert.SortOrder,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now,
                    };

                    await _db.BeginTranAsync();
                    var count = await _workflowRuleStepRepo.InsertWorkflowRuleStep(entity);
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
        /// 删除规则步骤
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowRuleStep(string ruleId, string currentStepId)
        {
            try
            {
                await _db.BeginTranAsync();
                var count = await _workflowRuleStepRepo.DeleteWorkflowRuleStep(long.Parse(ruleId), long.Parse(currentStepId));
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
        /// 修改规则步骤
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowRuleStep(WorkflowRuleStepUpsert upsert)
        {
            try
            {
                var isRepat = await _workflowRuleStepRepo.RuleStepIsRepeat(long.Parse(upsert.RuleId), long.Parse(upsert.NextStepId));
                if (isRepat)
                {
                    return Result<int>.Failure(400, _localization.ReturnMsg($"{_this}Repat"));
                }
                else
                {
                    var entity = new WorkflowRuleStepEntity()
                    {
                        RuleId = long.Parse(upsert.RuleId),
                        CurrentStepId = long.Parse(upsert.CurrentStepId),
                        NextStepId = long.Parse(upsert.NextStepId),
                        SortOrder = upsert.SortOrder,
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now,
                    };

                    await _db.BeginTranAsync();
                    var count = await _workflowRuleStepRepo.UpdateWorkflowRuleStep(entity);
                    await _db.CommitTranAsync();

                    return count >= 1
                            ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
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
        /// 查询规则实体
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowRuleDto>> GetWorkflowRuleStepEntity(string ruleId, string currentStepId)
        {
            try
            {
                var entity = await _workflowRuleStepRepo.GetWorkflowRuleStepEntity(long.Parse(ruleId), long.Parse(currentStepId));
                return Result<WorkflowRuleDto>.Ok(entity);
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowRuleDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询规则列表
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowRuleStepDto>>> GetWorkflowRuleStepList(string ruleId)
        {
            try
            {
                return await _workflowRuleStepRepo.GetWorkflowRuleStepList(long.Parse(ruleId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<WorkflowRuleStepDto>>.Failure(500, ex.Message);
            }
        }
    }
}
