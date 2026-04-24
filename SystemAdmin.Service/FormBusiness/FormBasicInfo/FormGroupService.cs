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
        private readonly FormGroupRepository _formGroupRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormBasicInfo.FormGroup";

        public FormGroupService(CurrentUser loginuser, ILogger<FormGroupService> logger, SqlSugarScope db, FormGroupRepository formGroupRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formGroupRepo = formGroupRepo;
            _localization = localization;
        }

        /// <summary>
        /// 新增表单组别
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormGroupInfo(FormGroupUpsert upsert)
        {
            try
            {
                var entity = new FormGroupEntity()
                {
                    FormGroupId = SnowFlakeSingle.Instance.NextId(),
                    FormGroupNameCn = upsert.FormGroupNameCn,
                    FormGroupNameEn = upsert.FormGroupNameEn,
                    SortOrder = upsert.SortOrder,
                    DescriptionCn = upsert.DescriptionCn,
                    DescriptionEn = upsert.DescriptionEn,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                await _db.BeginTranAsync();
                int count = await _formGroupRepo.InsertFormGroupInfo(entity);
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
        /// 删除表单组别
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteFormGroupInfo(string formGroupId)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除表单组别
                int delFormGroupCount = await _formGroupRepo.DeleteFormGroupInfo(long.Parse(formGroupId));
                // 删除员工表单组别绑定
                int delUserGroupBindCount = await _formGroupRepo.DeleteUserFormTypeBind(long.Parse(formGroupId));
                // 删除表单组别下的表单类别
                int delFormTypeCount = await _formGroupRepo.DeleteFormTypeInfo(long.Parse(formGroupId));
                // 获取被删除表单组别下的表单类别Id
                var delformTypeList = await _formGroupRepo.GetFormTypeIds(long.Parse(formGroupId));
                // 删除员工组别下的员工表单类别绑定
                int delFormTypeBindCount = await _formGroupRepo.DeleteUserFromType(delformTypeList);
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateFormGroupInfo(FormGroupUpsert upsert)
        {
            try
            {
                var entity = new FormGroupEntity()
                {
                    FormGroupId = long.Parse(upsert.FormGroupId),
                    FormGroupNameCn = upsert.FormGroupNameCn,
                    FormGroupNameEn = upsert.FormGroupNameEn,
                    SortOrder = upsert.SortOrder,
                    DescriptionCn = upsert.DescriptionCn,
                    DescriptionEn = upsert.DescriptionEn,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                await _db.BeginTranAsync();
                int count = await _formGroupRepo.UpdateFormGroupInfo(entity);
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
        /// 查询表单组别实体
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<FormGroupDto>> GetFormGroupEntity(string formGroupId)
        {
            try
            {
                var entity = await _formGroupRepo.GetFormGroupEntity(long.Parse(formGroupId));
                return Result<FormGroupDto>.Ok(entity, "");
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormGroupDto>> GetFormGroupPage(GetFormGroupPage getPage)
        {
            try
            {
                return await _formGroupRepo.GetFormGroupPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<FormGroupDto>.Failure(500, ex.Message);
            }
        }
    }
}
