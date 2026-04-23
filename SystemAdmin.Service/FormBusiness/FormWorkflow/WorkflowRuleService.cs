using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class WorkflowRuleService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowRuleService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowRuleRepository _workflowRule;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowRule";

        public WorkflowRuleService(CurrentUser loginuser, ILogger<WorkflowRuleService> logger, SqlSugarScope db, WorkflowRuleRepository workflowRule, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowRule = workflowRule;
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
                var drop = await _workflowRule.GetFormGroupDrop();
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
                var drop = await _workflowRule.GetFormTypeDrop(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
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
                var drop = await _workflowRule.GetPositionDrop();
                return Result<List<PositionDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<PositionDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增规则
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowRule(WorkflowRuleUpsert upsert)
        {
            try
            {
                // 规则是否重复配置
                var isRepat = await _workflowRule.RuleIsRepeat(long.Parse(upsert.FormTypeId), long.Parse(upsert.PositionId), upsert.Guidance);
                if (isRepat)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}Repat"));
                }
                else
                {
                    var entity = new WorkflowRuleEntity()
                    {
                        RuleId = SnowFlakeSingle.Instance.NextId(),
                        FormTypeId = long.Parse(upsert.FormTypeId),
                        RuleNameCn = upsert.RuleNameCn,
                        RuleNameEn = upsert.RuleNameEn,
                        PositionId = long.Parse(upsert.PositionId),
                        Guidance = upsert.Guidance,
                        SortOrder = upsert.SortOrder,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now,
                    };

                    await _db.BeginTranAsync();
                    var count = await _workflowRule.InsertWorkflowRule(entity);
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
        /// 删除规则
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowRule(string ruleId)
        {
            try
            {
                var isExistStep = await _workflowRule.GetWorkflowRuleStepIsExist(long.Parse(ruleId));
                if (isExistStep)
                {
                    return Result<int>.Failure(400, _localization.ReturnMsg($"{_this}Exist"));
                }
                else
                {
                    await _db.BeginTranAsync();
                    var count = await _workflowRule.DeleteWorkflowRule(long.Parse(ruleId));
                    await _db.CommitTranAsync();

                    return count >= 1
                            ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改规则
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowRule(WorkflowRuleUpsert upsert)
        {
            try
            {
                var entity = new WorkflowRuleEntity()
                {
                    RuleId = long.Parse(upsert.RuleId),
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    RuleNameCn = upsert.RuleNameCn,
                    RuleNameEn = upsert.RuleNameEn,
                    PositionId = long.Parse(upsert.PositionId),
                    Guidance = upsert.Guidance,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowRule.UpdateWorkflowRule(entity);
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
        /// 查询规则实体
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowRuleDto>> GetWorkflowRuleEntity(string ruleId)
        {
            try
            {
                var entity = await _workflowRule.GetWorkflowRuleEntity(long.Parse(ruleId));
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
        /// 查询规则分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowRuleDto>> GetWorkflowRulePage(GetWorkflowRulePage getPage)
        {
            try
            {
                return await _workflowRule.GetWorkflowRulePage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<WorkflowRuleDto>.Failure(500, ex.Message);
            }
        }
    }
}
