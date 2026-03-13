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
    public class UserAgentService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<UserAgentService> _logger;
        private readonly SqlSugarScope _db;
        private readonly UserAgentRepository _userAgentRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.UserSettings.UserAgent";

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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentDto>> GetUserInfoPage(GetUserAgentPage getPage)
        {
            try
            {
                return await _userAgentRepository.GetUserInfoPage(getPage);
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentViewDto>> GetUserInfoAgentView(GetUserAgentViewPage getPage)
        {
            try
            {
                return await _userAgentRepository.GetUserInfoAgentView(getPage);
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
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertUserAgent(UserAgentUpsert upsert)
        {
            try
            {
                // 检查被代理员工是否与代理员工一致
                if (upsert.SubstituteUserId == upsert.AgentUserId)
                {
                    // 被代理员工不能和代理员工相同
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}AgentSameEmployee"));
                }

                // 查询被代理员工已代理其他员工
                bool subAgentIsAgent = await _userAgentRepository.GetSubAgentIsAgent(long.Parse(upsert.SubstituteUserId));
                if (subAgentIsAgent)
                {
                    // 被代理员工已代理其他员工，不能嵌套代理
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}TargetHasAgentRole"));
                }

                // 查询被代理员工已被其他员工代理
                bool subAgentIsSubAgent = await _userAgentRepository.GetSubAgentIsSubAgent(long.Parse(upsert.SubstituteUserId));
                if (subAgentIsSubAgent)
                {
                    // 被代理员工已被其他员工代理，不可多人员代理
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}TargetAlreadyAgented"));
                }

                // 查询代理员工已被其他员工代理
                bool agentIsSubAgent = await _userAgentRepository.GetAgentIsSubAgent(long.Parse(upsert.AgentUserId));
                if (agentIsSubAgent)
                {
                    // 代理员工已被其他员工代理，不能作为代理员工
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}AgentAlreadyAgented"));
                }
                // 查询代理员工已代理其他员工
                bool agentIsAgent = await _userAgentRepository.GetAgentIsAgent(long.Parse(upsert.AgentUserId));
                if (agentIsAgent)
                {
                    // 代理员工已代理其他员工，不可多人员代理
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}AgentHasMultipleTargets"));
                }
                else
                {
                    // 重新配置代理人
                    var insertUserAgent = new UserAgentEntity
                    {
                        SubstituteUserId = long.Parse(upsert.SubstituteUserId),
                        AgentUserId = long.Parse(upsert.AgentUserId),
                        StartTime = upsert.StartTime,
                        EndTime = upsert.EndTime,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now,
                    };

                    await _db.BeginTranAsync();
                    // 新增员工代理人配置
                    int insertUserAgentCount = await _userAgentRepository.InsertUserAgent(insertUserAgent);
                    // 更新员工代理状态
                    var updateUserAgentCount = await _userAgentRepository.UpdateUserAgent(long.Parse(upsert.AgentUserId), 1);
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
        /// 删除员工代理关系
        /// </summary>
        /// <param name="upsertdel"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteUserAgent(UserAgentDel upsertdel)
        {
            try
            {
                await _db.BeginTranAsync();
                // 删除员工代理配置
                var delSubAgentCount = await _userAgentRepository.DeleteUserAgent(long.Parse(upsertdel.SubstituteUserId), long.Parse(upsertdel.AgentUserId));
                var updateUserAgentCount = await _userAgentRepository.UpdateUserAgent(long.Parse(upsertdel.AgentUserId), 0);
                await _db.CommitTranAsync();

                return delSubAgentCount >= 1 && updateUserAgentCount >= 1
                            ? Result<int>.Ok(delSubAgentCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询此员工代理的员工列表
        /// </summary>
        /// <param name="getList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentProactiveDto>>> GetUserAgentProactiveList(GetUserAgentProactiveList getList)
        {
            try
            {
                return await _userAgentRepository.GetUserAgentProactiveList(getList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserAgentProactiveDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询此员工被哪个员工代理列表
        /// </summary>
        /// <param name="getList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentPassiveDto>>> GetUserAgentPassiveList(GetUserAgentPassiveList getList)
        {
            try
            {
                return await _userAgentRepository.GetUserAgentPassiveList(getList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserAgentPassiveDto>>.Failure(500, ex.Message.ToString());
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
                var drop = await _userAgentRepository.GetDepartmentDropDown();
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
