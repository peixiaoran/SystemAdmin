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
    public class WorkflowBranchService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<WorkflowBranchService> _logger;
        private readonly SqlSugarScope _db;
        private readonly WorkflowBranchRepository _workflowBranchRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.WorkflowBranch";

        public WorkflowBranchService(CurrentUser loginuser, ILogger<WorkflowBranchService> logger, SqlSugarScope db, WorkflowBranchRepository workflowBranchRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _workflowBranchRepository = workflowBranchRepository;
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
                var drop = await _workflowBranchRepository.GetFormGroupDropDown();
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
                var drop = await _workflowBranchRepository.GetFormTypeDropDown(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增流程分支
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertWorkflowBranch(WorkflowBranchUpsert upsert)
        {
            try
            {
                var entity = new WorkflowBranchEntity()
                {
                    BranchId = SnowFlakeSingle.Instance.NextId(),
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    BranchNameCn = upsert.BranchNameCn,
                    BranchNameEn = upsert.BranchNameEn,
                    HandlerKey = upsert.HandlerKey,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowBranchRepository.InsertWorkflowBranch(entity);
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
        /// 删除流程分支
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteWorkflowBranch(string branchId)
        {
            try
            {
                var canDel = await _workflowBranchRepository.GetWorkflowStepBranchByCon(long.Parse(branchId));
                if (canDel)
                {
                    return Result<int>.Ok(0, _localization.ReturnMsg($"{_this}NotDelete"));
                }

                await _db.BeginTranAsync();
                var count = await _workflowBranchRepository.DeleteWorkflowBranch(long.Parse(branchId));
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
        /// 修改流程分支
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateWorkflowBranch(WorkflowBranchUpsert upsert)
        {
            try
            {
                var entity = new WorkflowBranchEntity()
                {
                    BranchId = long.Parse(upsert.BranchId),
                    BranchNameCn = upsert.BranchNameCn,
                    BranchNameEn = upsert.BranchNameEn,
                    HandlerKey = upsert.HandlerKey,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                var count = await _workflowBranchRepository.UpdateWorkflowBranch(entity);
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
        /// 查询流程分支实体
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<Result<WorkflowBranchDto>> GetWorkflowBranchEntity(string branchId)
        {
            try
            {
                var entity = await _workflowBranchRepository.GetWorkflowBranchEntity(long.Parse(branchId));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<WorkflowBranchDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询流程分支分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowBranchDto>> GetWorkflowBranchPage(GetWorkflowBranchPage getPage)
        {
            try
            {
                return await _workflowBranchRepository.GetWorkflowBranchPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<WorkflowBranchDto>.Failure(500, ex.Message);
            }
        }
    }
}
