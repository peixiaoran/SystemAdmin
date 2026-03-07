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
    public class UserFormBindService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserFormBindService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserFormBindRepository _userFormBindRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.UserSettings.UserFormBind";

        public UserFormBindService(CurrentUser loginuser, ILogger<UserFormBindService> logger, SqlSugarScope db, UserFormBindRepository userFormBindRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userFormBindRepository = userFormBindRepository;
            _localization = localization;
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserFormBindDto>> GetUserInfoPage(GetUserFormBindPage getPage)
        {
            try
            {
                return await _userFormBindRepository.GetUserInfoPage(getPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserFormBindDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询员工绑定表单树
        /// </summary>
        /// <param name="getTree"></param>
        /// <returns></returns>
        public async Task<Result<List<UserFormBindViewTreeDto>>> GetUserFormBindViewTree(GetUserFormBindViewTree getTree)
        {
            try
            {
                var treeList = await _userFormBindRepository.GetUserFormBindViewTree(long.Parse(getTree.UserId));
                return Result<List<UserFormBindViewTreeDto>>.Ok(treeList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserFormBindViewTreeDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 更新员工表单绑定
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserFormBind(UserFormBindUpsert upsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delCount = await _userFormBindRepository.DeleteUserFormBind(long.Parse(upsert.UserId));
                var entity = upsert.FormGroupTypeId
                                              .Select(id => new UserFormBindEntity
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
                var count = await _userFormBindRepository.InsertUserFormBind(entity);
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

        /// <summary>
        /// 部门下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var drop = await _userFormBindRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
