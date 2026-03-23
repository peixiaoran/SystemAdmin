using Mapster;
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
    public class WorkflowConditionService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowConditionService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowConditionRepository _workflowConditionRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowCondition";

        public WorkflowConditionService(CurrentUser loginuser, ILogger<WorkflowConditionService> logger, SqlSugarScope db, WorkflowConditionRepository workflowConditionRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowConditionRepository = workflowConditionRepository;
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
                var drop = await _workflowConditionRepository.GetFormGroupDropDown();
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
                var drop = await _workflowConditionRepository.GetFormTypeDropDown(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增流程条件
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowCondition(WorkflowConditionUpsert upsert)
        {
            try
            {
                var entity = new WorkflowConditionEntity()
                {
                    ConditionId = SnowFlakeSingle.Instance.NextId(),
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    ConditionNameCn = upsert.ConditionNameCn,
                    ConditionNameEn = upsert.ConditionNameEn,
                    HandlerKey = upsert.HandlerKey,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowConditionRepository.InsertWorkflowCondition(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除流程条件
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowCondition(string conditionId)
        {
            try
            {
                var canDel = await _workflowConditionRepository.GetWorkflowStepBranchByCon(long.Parse(conditionId));
                if (canDel)
                {
                    return Result<int>.Ok(0, _localization.ReturnMsg($"{_this}NotDelete"));
                }

                await _db.BeginTranAsync();
                var count = await _workflowConditionRepository.DeleteWorkflowCondition(long.Parse(conditionId));
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
        /// 修改流程条件
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowCondition(WorkflowConditionUpsert upsert)
        {
            try
            {
                var entity = new WorkflowConditionEntity()
                {
                    ConditionId = long.Parse(upsert.ConditionId),
                    ConditionNameCn = upsert.ConditionNameCn,
                    ConditionNameEn = upsert.ConditionNameEn,
                    HandlerKey = upsert.HandlerKey,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowConditionRepository.UpdateWorkflowCondition(entity);
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
        /// 查询流程条件实体
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowConditionDto>> GetWorkflowConditionEntity(string conditionId)
        {
            try
            {
                var entity = await _workflowConditionRepository.GetWorkflowConditionEntity(long.Parse(conditionId));
                return Result<WorkflowConditionDto>.Ok(entity.Adapt<WorkflowConditionDto>(), "");
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowConditionDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询流程条件分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowConditionDto>> GetWorkflowConditionPage(GetWorkflowConditionPage getPage)
        {
            try
            {
                return await _workflowConditionRepository.GetWorkflowConditionPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<WorkflowConditionDto>.Failure(500, ex.Message);
            }
        }
    }
}
