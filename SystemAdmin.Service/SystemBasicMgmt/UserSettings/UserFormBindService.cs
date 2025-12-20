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
        /// <param name="getUserFormBindPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserFormBindDto>> GetUserInfoPage(GetUserFormBindPage getUserFormBindPage)
        {
            try
            {
                return await _userFormBindRepository.GetUserInfoPage(getUserFormBindPage);
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
        /// <param name="getUserFormBindViewTree"></param>
        /// <returns></returns>
        public async Task<Result<List<UserFormBindViewTreeDto>>> GetUserFormBindViewTree(GetUserFormBindViewTree getUserFormBindViewTree)
        {
            try
            {
                var userFormBindTree = await _userFormBindRepository.GetUserFormBindViewTree(long.Parse(getUserFormBindViewTree.UserId));
                return Result<List<UserFormBindViewTreeDto>>.Ok(userFormBindTree, "");
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
        /// <param name="userFormBindUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateUserFormBind(UserFormBindUpsert userFormBindUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                var delUserFormCount = await _userFormBindRepository.DeleteUserFormBind(long.Parse(userFormBindUpsert.UserId));
                var insertUserFormBindEntity = userFormBindUpsert.FormGroupTypeId
                                             .Select(id => new UserFormBindEntity
                                             {
                                                 UserId = long.Parse(userFormBindUpsert.UserId),
                                                 FormGroupTypeId = long.Parse(id),
                                                 CreatedBy = _loginuser.UserId,
                                                 CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                             }).ToList();

                insertUserFormBindEntity.ForEach(userform =>
                {
                    userform.CreatedBy = _loginuser.UserId;
                    userform.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    userform.ModifiedBy = _loginuser.UserId;
                    userform.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                });
                var insertUserFormCount = await _userFormBindRepository.InsertUserFormBind(insertUserFormBindEntity);
                await _db.CommitTranAsync();

                return Result<int>.Ok(insertUserFormCount, _localization.ReturnMsg($"{_this}InsertSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var deptDrop = await _userFormBindRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(deptDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
