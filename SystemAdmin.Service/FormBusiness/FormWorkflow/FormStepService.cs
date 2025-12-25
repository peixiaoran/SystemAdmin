using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormProcessConfig.Queries;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class FormStepService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormStepService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormStepRepository _formStepRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.FormStep";

        public FormStepService(CurrentUser loginuser, ILogger<FormStepService> logger, SqlSugarScope db, FormStepRepository formStepRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formStepRepository = formStepRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增审批步骤信息
        /// </summary>
        /// <param name="formStepUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormStep(FormStepUpsert formStepUpsert)
        {
            try
            {
                FormStepEntity insertFormStep = new FormStepEntity
                {
                    StepId = SnowFlakeSingle.Instance.NextId(),
                    FormTypeId = long.Parse(formStepUpsert.FormTypeId),
                    StepNameCn = formStepUpsert.StepNameCn,
                    StepNameEn = formStepUpsert.StepNameEn,
                    Assignment = formStepUpsert.Assignment,
                    Description = formStepUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.BeginTranAsync();
                int insertFormStepCount = await _formStepRepository.InsertFormStep(insertFormStep);
                await _db.CommitTranAsync();

                return insertFormStepCount >= 1
                        ? Result<int>.Ok(insertFormStepCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 修改审批步骤信息
        /// </summary>
        /// <param name="formStepUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormStep(FormStepUpsert formStepUpsert)
        {
            try
            {
                FormStepEntity updateFormStep = new FormStepEntity
                {
                    StepId = long.Parse(formStepUpsert.StepId),
                    FormTypeId = long.Parse(formStepUpsert.FormTypeId),
                    StepNameCn = formStepUpsert.StepNameCn,
                    StepNameEn = formStepUpsert.StepNameEn,
                    Assignment = formStepUpsert.Assignment,
                    Description = formStepUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                await _db.BeginTranAsync();
                int updateFormStepCount = await _formStepRepository.UpdateFormStep(updateFormStep);
                await _db.CommitTranAsync();

                return updateFormStepCount >= 1
                        ? Result<int>.Ok(updateFormStepCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <returns></returns>
        public async Task<ResultPaged<FormStepPageDto>> GetFormStepPage(GetFormStepPage getFormStepPage)
        {
            try
            {
                return await _formStepRepository.GetFormStepPage(getFormStepPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormStepPageDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询审批步骤实体
        /// </summary>
        /// <returns></returns>
        public async Task<Result<FormStepEntityDto>> GetFormStepEntity(GetFormStepEntity getFormStepEntity)
        {
            try
            {
                var formStepEntity = await _formStepRepository.GetFormStepEntity(long.Parse(getFormStepEntity.StepId));
                formStepEntity.formStepOrgEntity = await _formStepRepository.GetFormStepOrgEntity(long.Parse(getFormStepEntity.StepId));
                formStepEntity.formStepDeptCriteriaEntity = await _formStepRepository.GetStepDeptCriteriaEntity(long.Parse(getFormStepEntity.StepId));
                formStepEntity.formStepUserEntity = await _formStepRepository.GetStepUserEntity(long.Parse(getFormStepEntity.StepId));
                formStepEntity.formStepApproverRuleEntity = await _formStepRepository.GetStepApproverRuleEntity(long.Parse(getFormStepEntity.StepId));
                return Result<FormStepEntityDto>.Ok(formStepEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<FormStepEntityDto>.Failure(500, ex.Message);
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
                var formGroupDropDown = await _formStepRepository.GetFormGroupDropDown();
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
                var formTypeDropDown = await _formStepRepository.GetFormTypeDropDown(long.Parse(getFormTypeDropDown.FormGroupId));
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
                var assigDropDown = await _formStepRepository.GetAssignmentDropDown();
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
                var deptDrop = await _formStepRepository.GetDepartmentDropDown();
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
                var deptLevelDrop = await _formStepRepository.GetDepartmentLevelDropDown();
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
                var userPositionDrop = await _formStepRepository.GetUserPositionDropDown();
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
                var laborDrop = await _formStepRepository.GetLaborDropDown();
                return Result<List<UserLaborDropDto>>.Ok(laborDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserLaborDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 用户信息分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<StepAssignUserInfoDto>> GetUserInfoPage(GetStepAssignUserInfoPage getUserInfoPage)
        {
            try
            {
                var userPage = await _formStepRepository.GetUserInfoPage(getUserInfoPage);
                return userPage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<StepAssignUserInfoDto>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
