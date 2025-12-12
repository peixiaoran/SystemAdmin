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
    public class FormTypeService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormTypeService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormTypeRepository _formTypeRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness_FormBasicInfo_FormType_";

        public FormTypeService(CurrentUser loginuser, ILogger<FormTypeService> logger, SqlSugarScope db, FormTypeRepository formTypeRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formTypeRepository = formTypeRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增表单类别
        /// </summary>
        /// <param name="formTypeUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormTypeInfo(FormTypeUpsert formTypeUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                FormTypeEntity insertFromGroupEntity = new FormTypeEntity()
                {
                    FormTypeId = SnowFlakeSingle.Instance.NextId(),
                    FormGroupId = long.Parse(formTypeUpsert.FormGroupId),
                    FormTypeNameCn = formTypeUpsert.FormTypeNameCn,
                    FormTypeNameEn = formTypeUpsert.FormTypeNameEn,
                    Prefix = formTypeUpsert.Prefix.Trim(),
                    ApprovalPath = formTypeUpsert.ApprovalPath,
                    ViewPath = formTypeUpsert.ViewPath,
                    SortOrder = formTypeUpsert.SortOrder,
                    DescriptionCn = formTypeUpsert.DescriptionCn,
                    DescriptionEn = formTypeUpsert.DescriptionEn,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int insertFormTypeCount = await _formTypeRepository.InsertFormTypeInfo(insertFromGroupEntity);
                await _db.CommitTranAsync();

                return insertFormTypeCount >= 1
                        ? Result<int>.Ok(insertFormTypeCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除表单类别
        /// </summary>
        /// <param name="formTypeUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFormTypeInfo(FormTypeUpsert formTypeUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int delFormTypeCount = await _formTypeRepository.DeleteFormTypeInfo(long.Parse(formTypeUpsert.FormTypeId));
                int delUserTypeBindCount = await _formTypeRepository.DeleteUserFormTypeBind(long.Parse(formTypeUpsert.FormTypeId));
                await _db.CommitTranAsync();

                return delFormTypeCount >= 1
                    ? Result<int>.Ok(delFormTypeCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// 修改表单类别
        /// </summary>
        /// <param name="formTypeUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormTypeInfo(FormTypeUpsert formTypeUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                FormTypeEntity updateFormTypeEntity = new FormTypeEntity()
                {
                    FormTypeId = long.Parse(formTypeUpsert.FormTypeId),
                    FormGroupId = long.Parse(formTypeUpsert.FormGroupId),
                    FormTypeNameCn = formTypeUpsert.FormTypeNameCn,
                    FormTypeNameEn = formTypeUpsert.FormTypeNameEn,
                    Prefix = formTypeUpsert.Prefix,
                    ApprovalPath = formTypeUpsert.ApprovalPath,
                    ViewPath = formTypeUpsert.ViewPath,
                    SortOrder = formTypeUpsert.SortOrder,
                    DescriptionCn = formTypeUpsert.DescriptionCn,
                    DescriptionEn = formTypeUpsert.DescriptionEn,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int updateFormTypeCount = await _formTypeRepository.UpdateFormTypeInfo(updateFormTypeEntity);
                await _db.CommitTranAsync();

                return updateFormTypeCount >= 1
                        ? Result<int>.Ok(updateFormTypeCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// 查询表单类别实体
        /// </summary>
        /// <param name="getFormTypeEntity"></param>
        /// <returns></returns>
        public async Task<Result<FormTypeDto>> GetFormTypeEntity(GetFormTypeEntity getFormTypeEntity)
        {
            try
            {
                var formTypeEntity = await _formTypeRepository.GetFormTypeEntity(long.Parse(getFormTypeEntity.FormTypeId));
                return Result<FormTypeDto>.Ok(formTypeEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<FormTypeDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单类别分页
        /// </summary>
        /// <param name="getFormTypePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormTypeDto>> GetFormTypePage(GetFormTypePage getFormTypePage)
        {
            try
            {
                return await _formTypeRepository.GetFormTypePage(getFormTypePage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormTypeDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单类别分页
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            try
            {
                var formGroupDrop = await _formTypeRepository.GetFormGroupDropDown();
                return Result<List<FormGroupDropDto>>.Ok(formGroupDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }
    }
}
