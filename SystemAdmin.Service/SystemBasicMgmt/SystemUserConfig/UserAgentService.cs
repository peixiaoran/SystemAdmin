using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemUserConfig;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemUserConfig
{
    public class UserAgentService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserAgentService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserAgentRepository _userAgentRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemUserConfig_UserAgent_";

        public UserAgentService(CurrentUser loginuser, ILogger<UserAgentService> logger, SqlSugarScope db, UserAgentRepository userAgentRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _userAgentRepository = userAgentRepository;
            _localization = localization;
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getUserAgentPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentDto>> GetUserInfoPage(GetUserAgentPage getUserAgentPage)
        {
            try
            {
                return await _userAgentRepository.GetUserInfoPage(getUserAgentPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserAgentDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询可代理其他员工分页
        /// </summary>
        /// <param name="getUserAgentViewPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentViewDto>> GetUserInfoAgentView(GetUserAgentViewPage getUserAgentViewPage)
        {
            try
            {
                return await _userAgentRepository.GetUserInfoAgentView(getUserAgentViewPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ResultPaged<UserAgentViewDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增员工代理人
        /// </summary>
        /// <param name="userAgentUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserAgent(UserAgentUpsert userAgentUpsert)
        {
            try
            {
                // 检查被代理员工是否与代理员工一致
                if (userAgentUpsert.SubstituteUserId == userAgentUpsert.AgentUserId)
                {
                    // 被代理员工不能和代理员工相同
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}SubAgentSameAsAgent"));
                }

                await _db.BeginTranAsync();
                // 检查被代理员工和代理员工是否已有代理关系
                bool subAgentIsExist = await _userAgentRepository.GetSubAgentIsExist(long.Parse(userAgentUpsert.SubstituteUserId), long.Parse(userAgentUpsert.AgentUserId));
                if (subAgentIsExist)
                {
                    // 该代理关系已存在
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}RelationAlreadyExist"));
                }

                // 查询被代理员工是否代理了其他员工
                bool subAgentIsAgent = await _userAgentRepository.GetSubAgentIsAgent(long.Parse(userAgentUpsert.SubstituteUserId));
                if (subAgentIsAgent)
                {
                    // 被代理员工已是其他员工的代理
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}SubAgentAlreadyAgent"));
                }

                // 查询代理员工是否被代理
                bool agentIsSubAgent = await _userAgentRepository.GetAgentIsSubAgent(long.Parse(userAgentUpsert.AgentUserId));
                if (agentIsSubAgent)
                {
                    // 代理员工已被其他员工代理
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}AgentAlreadySubAgent"));
                }
                else
                {
                    // 重新配置代理人
                    UserAgentEntity userAgentEntity = new UserAgentEntity
                    {
                        SubstituteUserId = long.Parse(userAgentUpsert.SubstituteUserId),
                        AgentUserId = long.Parse(userAgentUpsert.AgentUserId),
                        StartTime = userAgentUpsert.StartTime,
                        EndTime = userAgentUpsert.EndTime,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    // 新增员工代理人配置
                    int insertUserAgentCount = await _userAgentRepository.InsertUserAgent(userAgentEntity);
                    // 更新员工代理状态
                    var updateUserAgentCount = await _userAgentRepository.UpdateUserAgent(long.Parse(userAgentUpsert.AgentUserId), 1);
                    await _db.CommitTranAsync();

                    return insertUserAgentCount >= 1 && updateUserAgentCount >= 1
                           ? Result<int>.Ok(insertUserAgentCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                           : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 删除员工代理关系（被动）
        /// </summary>
        /// <param name="userAgentDel"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserAgent(UserAgentDel userAgentDel)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工代理配置
                var delSubAgentCount = await _userAgentRepository.DeleteUserAgent(long.Parse(userAgentDel.SubstituteUserId), long.Parse(userAgentDel.AgentUserId));
                var updateUserAgentCount = await _userAgentRepository.UpdateUserAgent(long.Parse(userAgentDel.AgentUserId), 0);
                if (delSubAgentCount >= 1 && updateUserAgentCount >= 1)
                {
                    await _db.CommitTranAsync();
                    return Result<int>.Ok(delSubAgentCount, _localization.ReturnMsg($"{_this}DeleteSuccess"));
                }
                else
                {
                    await _db.CommitTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
                }
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询此员工代理了哪些人列表
        /// </summary>
        /// <param name="getUserAgentProactiveList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentProactiveDto>>> GetUserAgentProactiveList(GetUserAgentProactiveList getUserAgentProactiveList)
        {
            try
            {
                return await _userAgentRepository.GetUserAgentProactiveList(getUserAgentProactiveList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserAgentProactiveDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询此员工被哪些人代理列表
        /// </summary>
        /// <param name="getUserAgentPassiveList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentPassiveDto>>> GetUserAgentPassiveList(GetUserAgentPassiveList getUserAgentPassiveList)
        {
            try
            {
                return await _userAgentRepository.GetUserAgentPassiveList(getUserAgentPassiveList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserAgentPassiveDto>>.Failure(500, ex.Message.ToString());
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
                var deptDrop = await _userAgentRepository.GetDepartmentDropDown();
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
