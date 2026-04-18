using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.UserSettings;

namespace SystemAdmin.Service.SystemBasicMgmt.UserSettings
{
    public class UserFormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserFormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserFormRepository _userFormBindRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.UserSettings.UserForm";

        public UserFormService(CurrentUser loginuser, ILogger<UserFormService> logger, SqlSugarScope db, UserFormRepository userFormBindRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userFormBindRepository = userFormBindRepository;
            _localization = localization;
        }

        /// <summary>
        /// 部门下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            try
            {
                var drop = await _userFormBindRepository.GetDepartmentDrop();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserFormDto>> GetUserInfoPage(GetUserFormPage getPage)
        {
            try
            {
                return await _userFormBindRepository.GetUserInfoPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserFormDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工绑定表单树
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Result<List<UserFormViewTreeDto>>> GetUserFormViewTree(string userId)
        {
            try
            {
                var treeList = await _userFormBindRepository.GetUserFormViewTree(long.Parse(userId));
                return Result<List<UserFormViewTreeDto>>.Ok(treeList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserFormViewTreeDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 更新员工表单绑定
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserForm(UserFormUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delCount = await _userFormBindRepository.DeleteUserForm(long.Parse(upsert.UserId));
                var entity = upsert.FormGroupTypeId
                                              .Select(id => new UserFormEntity
                                              {
                                                  UserId = long.Parse(upsert.UserId),
                                                  FormGroupTypeId = long.Parse(id),
                                                  CreatedBy = _loginuser.UserId,
                                                  CreatedDate = DateTime.Now
                                              }).ToList();

                entity.ForEach(userform =>
                {
                    userform.CreatedBy = _loginuser.UserId;
                    userform.CreatedDate = DateTime.Now;
                    userform.ModifiedBy = _loginuser.UserId;
                    userform.ModifiedDate = DateTime.Now;
                });
                var count = await _userFormBindRepository.InsertUserForm(entity);
                await _db.CommitTranAsync();

                return Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }
    }
}
