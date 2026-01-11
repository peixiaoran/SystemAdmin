using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Commands;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Repository.FormBusiness.FormBasicInfo;

namespace SystemAdmin.Service.FormBusiness.FormBasicInfo
{
    public class FormGroupService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormGroupService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormGroupRepository _formGroupRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormBasicInfo.FormGroup";

        public FormGroupService(CurrentUser loginuser, ILogger<FormGroupService> logger, SqlSugarScope db, FormGroupRepository formGroupRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formGroupRepository = formGroupRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增表单组别
        /// </summary>
        /// <param name="formGroupUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormGroupInfo(FormGroupUpsert formGroupUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                FormGroupEntity insertFromGroup = new FormGroupEntity()
                {
                    FormGroupId = SnowFlakeSingle.Instance.NextId(),
                    FormGroupNameCn = formGroupUpsert.FormGroupNameCn,
                    FormGroupNameEn = formGroupUpsert.FormGroupNameEn,
                    SortOrder = formGroupUpsert.SortOrder,
                    DescriptionCn = formGroupUpsert.DescriptionCn,
                    DescriptionEn = formGroupUpsert.DescriptionEn,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int insertFormGroupCount = await _formGroupRepository.InsertFormGroupInfo(insertFromGroup);
                await _db.CommitTranAsync();

                return insertFormGroupCount >= 1
                        ? Result<int>.Ok(insertFormGroupCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除表单组别
        /// </summary>
        /// <param name="formGroupUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFormGroupInfo(FormGroupUpsert formGroupUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除表单组别
                int delFormGroupCount = await _formGroupRepository.DeleteFormGroupInfo(long.Parse(formGroupUpsert.FormGroupId));
                // 删除员工表单组别绑定
                int delUserGroupBindCount = await _formGroupRepository.DeleteUserFormTypeBind(long.Parse(formGroupUpsert.FormGroupId));
                // 删除表单组别下的表单类别
                int delFormTypeCount = await _formGroupRepository.DeleteFormTypeInfo(long.Parse(formGroupUpsert.FormGroupId));
                // 获取被删除表单组别下的表单类别Id
                var delformTypeList = await _formGroupRepository.GetFormTypeIds(long.Parse(formGroupUpsert.FormGroupId));
                // 删除员工组别下的员工表单类别绑定
                int delFormTypeBindCount = await _formGroupRepository.DeleteUserFromType(delformTypeList);
                await _db.CommitTranAsync();

                return delFormGroupCount >= 1
                        ? Result<int>.Ok(delFormGroupCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改表单组别
        /// </summary>
        /// <param name="formGroupUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormGroupInfo(FormGroupUpsert formGroupUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                FormGroupEntity updateFormGroup = new FormGroupEntity()
                {
                    FormGroupId = long.Parse(formGroupUpsert.FormGroupId),
                    FormGroupNameCn = formGroupUpsert.FormGroupNameCn,
                    FormGroupNameEn = formGroupUpsert.FormGroupNameEn,
                    SortOrder = formGroupUpsert.SortOrder,
                    DescriptionCn = formGroupUpsert.DescriptionCn,
                    DescriptionEn = formGroupUpsert.DescriptionEn,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int updateFormGroupCount = await _formGroupRepository.UpdateFormGroupInfo(updateFormGroup);
                await _db.CommitTranAsync();

                return updateFormGroupCount >= 1
                        ? Result<int>.Ok(updateFormGroupCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询表单组别实体
        /// </summary>
        /// <param name="getFormGroupEntity"></param>
        /// <returns></returns>
        public async Task<Result<FormGroupDto>> GetFormGroupEntity(GetFormGroupEntity getFormGroupEntity)
        {
            try
            {
                var formGroupEntity = await _formGroupRepository.GetFormGroupEntity(long.Parse(getFormGroupEntity.FormGroupId));
                return Result<FormGroupDto>.Ok(formGroupEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<FormGroupDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单组别分页
        /// </summary>
        /// <param name="getFormGroupPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormGroupDto>> GetFormGroupPage(GetFormGroupPage getFormGroupPage)
        {
            try
            {
                return await _formGroupRepository.GetFormGroupPage(getFormGroupPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormGroupDto>.Failure(500, ex.Message);
            }
        }
    }
}
