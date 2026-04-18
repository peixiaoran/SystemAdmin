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
        private readonly string _this = "FormBusiness.FormBasicInfo.FormType";

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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormTypeInfo(FormTypeUpsert upsert)
        {
            try
            {
                var entity = new FormTypeEntity()
                {
                    FormTypeId = SnowFlakeSingle.Instance.NextId(),
                    FormGroupId = long.Parse(upsert.FormGroupId),
                    FormTypeNameCn = upsert.FormTypeNameCn,
                    FormTypeNameEn = upsert.FormTypeNameEn,
                    Prefix = upsert.Prefix.Trim(),
                    ApprovalPath = upsert.ApprovalPath,
                    ViewPath = upsert.ViewPath,
                    SortOrder = upsert.SortOrder,
                    DescriptionCn = upsert.DescriptionCn,
                    DescriptionEn = upsert.DescriptionEn,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                await _db.BeginTranAsync();
                int count = await _formTypeRepository.InsertFormTypeInfo(entity);
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
        /// 删除表单类别
        /// </summary>
        /// <param name="fromTypeId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFormTypeInfo(string fromTypeId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除表单类别
                int delFormTypeCount = await _formTypeRepository.DeleteFormTypeInfo(long.Parse(fromTypeId));
                // 删除员工表单绑定
                int delUserTypeBindCount = await _formTypeRepository.DeleteUserFormTypeBind(long.Parse(fromTypeId));
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormTypeInfo(FormTypeUpsert upsert)
        {
            try
            {
                var entity = new FormTypeEntity()
                {
                    FormTypeId = long.Parse(upsert.FormTypeId),
                    FormGroupId = long.Parse(upsert.FormGroupId),
                    FormTypeNameCn = upsert.FormTypeNameCn,
                    FormTypeNameEn = upsert.FormTypeNameEn,
                    Prefix = upsert.Prefix,
                    ApprovalPath = upsert.ApprovalPath,
                    ViewPath = upsert.ViewPath,
                    SortOrder = upsert.SortOrder,
                    DescriptionCn = upsert.DescriptionCn,
                    DescriptionEn = upsert.DescriptionEn,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                await _db.BeginTranAsync();
                int count = await _formTypeRepository.UpdateFormTypeInfo(entity);
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
        /// 查询表单类别实体
        /// </summary>
        /// <param name="fromTypeId"></param>
        /// <returns></returns>
        public async Task<Result<FormTypeDto>> GetFormTypeEntity(string fromTypeId)
        {
            try
            {
                var entity = await _formTypeRepository.GetFormTypeEntity(long.Parse(fromTypeId));
                return Result<FormTypeDto>.Ok(entity, "");
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormTypeDto>> GetFormTypePage(GetFormTypePage getPage)
        {
            try
            {
                return await _formTypeRepository.GetFormTypePage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormTypeDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDrop()
        {
            try
            {
                var drop = await _formTypeRepository.GetFormGroupDrop();
                return Result<List<FormGroupDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }
    }
}
