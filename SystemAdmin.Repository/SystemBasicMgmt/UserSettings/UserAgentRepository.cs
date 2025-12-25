using Mapster;
using SqlSugar;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;
using SystemAdmin.CommonSetup.Options;

namespace SystemAdmin.Repository.SystemBasicMgmt.UserSettings
{
    public class UserAgentRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserAgentRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getUserAgentPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentDto>> GetUserInfoPage(GetUserAgentPage getUserAgentPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<UserPositionEntity>((user, dept, userpos) => user.PositionId == userpos.PositionId)
                           .InnerJoin<UserLaborEntity>((user, dept, userpos, userlabor) => user.LaborId == userlabor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, dept, userpos, userlabor, nation) =>
                            user.Nationality == nation.NationId)
                           .InnerJoin<DictionaryInfoEntity>((user, dept, userpos, userlabor, nation, approvaldic) =>
                            approvaldic.DicType == "ApprovalState" && user.IsApproval == approvaldic.DicCode)
                           .InnerJoin<DictionaryInfoEntity>((user, dept, userpos, userlabor, nation, approvaldic, agentdic) =>
                            agentdic.DicType == "IsAgent" && user.IsAgent == agentdic.DicCode)
                           .Where((user, dept, userpos, userlabor, nation, approvaldic, agentdic) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserAgentPage.UserNo))
            {
                query = query.Where((user, dept, userpos, userlabor, nation, approvaldic, agentdic) =>
                    user.UserNo.Contains(getUserAgentPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserAgentPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation, approvaldic, agentdic) =>
                    user.UserNameCn.Contains(getUserAgentPage.UserName) ||
                    user.UserNameEn.Contains(getUserAgentPage.UserName));
            }
            // 部门Id（仅在工号与姓名都为空时）
            if (!string.IsNullOrEmpty(getUserAgentPage.DepartmentId)
                && string.IsNullOrEmpty(getUserAgentPage.UserNo)
                && string.IsNullOrEmpty(getUserAgentPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation, approvaldic, agentdic) =>
                    user.DepartmentId == long.Parse(getUserAgentPage.DepartmentId));
            }

            var userPage = await query.OrderBy((user, dept, userpos, userlabor, nation, approvaldic, agentdic) => new { userpos.PositionOrderBy, user.HireDate })
            .Select((user, dept, userpos, userlabor, nation, approvaldic, agentdic) => new UserAgentDto
            {
                UserId = user.UserId,
                UserNo = user.UserNo,
                UserName = _lang.Locale == "zh-CN"
                           ? user.UserNameCn
                           : user.UserNameEn,
                DepartmentName = _lang.Locale == "zh-CN"
                           ? dept.DepartmentNameCn
                           : dept.DepartmentNameEn,
                PositionName = _lang.Locale == "zh-CN"
                           ? userpos.PositionNameCn
                           : userpos.PositionNameEn,
                LaborName = _lang.Locale == "zh-CN"
                           ? userlabor.LaborNameCn
                           : userlabor.LaborNameEn,
                NationalityName = _lang.Locale == "zh-CN"
                           ? nation.NationNameCn
                           : nation.NationNameEn,
                IsAgent = user.IsAgent,
                IsAgentName = _lang.Locale == "zh-CN"
                           ? agentdic.DicNameCn
                           : agentdic.DicNameEn,
                IsApproval = user.IsApproval,
                IsApprovalName = _lang.Locale == "zh-CN"
                           ? approvaldic.DicNameCn
                           : approvaldic.DicNameEn
            }).ToPageListAsync(getUserAgentPage.PageIndex, getUserAgentPage.PageSize, totalCount);
            return ResultPaged<UserAgentDto>.Ok(userPage.Adapt<List<UserAgentDto>>(), totalCount, "");
        }

        /// <summary>
        /// 查询可代理其他员工分页
        /// </summary>
        /// <param name="getUserAgentViewPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserAgentViewDto>> GetUserInfoAgentView(GetUserAgentViewPage getUserAgentViewPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<DictionaryInfoEntity>((user, approvaldic) => approvaldic.DicType == "ApprovalState" && user.IsApproval == approvaldic.DicCode)
                           .InnerJoin<DepartmentInfoEntity>((user, approvaldic, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<UserPositionEntity>((user, approvaldic, dept, userpos) => user.PositionId == userpos.PositionId)
                           .InnerJoin<UserLaborEntity>((user, approvaldic, dept, userpos, userlabor) => user.LaborId == userlabor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, approvaldic, dept, userpos, userlabor, nation) => user.Nationality == nation.NationId)
                           .Where((user, approvaldic, dept, userpos, userlabor, nation) => user.IsAgent == 0 && user.UserId != long.Parse(getUserAgentViewPage.SubstituteUserId) && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserAgentViewPage.UserNo))
            {
                query = query.Where((user, approvaldic, dept, userpos, userlabor, nation) =>
                    user.UserNo == getUserAgentViewPage.UserNo);
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserAgentViewPage.UserName))
            {
                query = query.Where((user, approvaldic, dept, userpos, userlabor, nation) =>
                    user.UserNameCn.Contains(getUserAgentViewPage.UserName) ||
                    user.UserNameEn.Contains(getUserAgentViewPage.UserName));
            }
            // 部门 Id（仅在员工工号与姓名均为空时）
            if (!string.IsNullOrEmpty(getUserAgentViewPage.DepartmentId)
                && string.IsNullOrEmpty(getUserAgentViewPage.UserNo)
                && string.IsNullOrEmpty(getUserAgentViewPage.UserName))
            {
                query = query.Where((user, approvaldic, dept, userpos, userlabor, nation) =>
                    user.DepartmentId == long.Parse(getUserAgentViewPage.DepartmentId));
            }

            var userAgentPage = await query.OrderBy((user, approvaldic, dept, userpos, userlabor, nation) => user.UserId)
            .Select((user, approvaldic, dept, userpos, userlabor, nation) => new UserAgentViewDto
            {
                UserId = user.UserId,
                UserNo = user.UserNo,
                UserName = _lang.Locale == "zh-CN"
                           ? user.UserNameCn
                           : user.UserNameEn,
                IsApprovalName = _lang.Locale == "zh-CN"
                           ? approvaldic.DicNameCn
                           : approvaldic.DicNameEn,
                DepartmentName = _lang.Locale == "zh-CN"
                           ? dept.DepartmentNameCn
                           : dept.DepartmentNameEn,
                PositionName = _lang.Locale == "zh-CN"
                           ? userpos.PositionNameCn
                           : userpos.PositionNameEn,
                LaborName = _lang.Locale == "zh-CN"
                           ? userlabor.LaborNameCn
                           : userlabor.LaborNameEn,
                NationalityName = _lang.Locale == "zh-CN"
                           ? nation.NationNameCn
                           : nation.NationNameEn,
            }).ToPageListAsync(getUserAgentViewPage.PageIndex, getUserAgentViewPage.PageSize, totalCount);
            return ResultPaged<UserAgentViewDto>.Ok(userAgentPage, totalCount, "");
        }

        /// <summary>
        /// 新增员工代理人
        /// </summary>
        /// <param name="userAgentEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserAgent(UserAgentEntity userAgentEntity)
        {
            return await _db.Insertable(userAgentEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工代理人
        /// </summary>
        /// <param name="substituteUserId"></param>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAgent(long substituteUserId, long agentUserId)
        {
            return await _db.Deleteable<UserAgentEntity>()
                            .Where(useragent => useragent.SubstituteUserId == substituteUserId && useragent.AgentUserId == agentUserId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改员工代理状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isAgent"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAgent(long userId, int isAgent)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(userinfo => userinfo.IsAgent == isAgent)
                            .Where(userinfo => userinfo.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询员工代理了哪些人列表
        /// </summary>
        /// <param name="getUserAgentProactiveList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentProactiveDto>>> GetUserAgentProactiveList(GetUserAgentProactiveList getUserAgentProactiveList)
        {
            var userAgentProactiveList = await _db.Queryable<UserAgentEntity>()
                                                  .With(SqlWith.NoLock)
                                                  .LeftJoin<UserInfoEntity>((useragent, agentuser) => useragent.AgentUserId == agentuser.UserId)
                                                  .LeftJoin<UserInfoEntity>((useragent, agentuser, substituteuser) => useragent.SubstituteUserId == substituteuser.UserId)
                                                  .Where((useragent, agentuser, substituteuser) => useragent.AgentUserId == long.Parse(getUserAgentProactiveList.UserId))
                                                  .Select((useragent, agentuser, substituteuser) => new UserAgentProactiveDto
                                                  {
                                                      AgentUserId = useragent.AgentUserId,
                                                      SubstituteUserId = substituteuser.UserId,
                                                      SubstituteUserNo = substituteuser.UserNo,
                                                      SubstituteUserName = _lang.Locale == "zh-CN"
                                                                           ? substituteuser.UserNameCn
                                                                           : substituteuser.UserNameEn,
                                                      StartTime = useragent.StartTime,
                                                      EndTime = useragent.EndTime
                                                  }).ToListAsync();
            return Result<List<UserAgentProactiveDto>>.Ok(userAgentProactiveList.Adapt<List<UserAgentProactiveDto>>(), "");
        }

        /// <summary>
        /// 查询此员工被哪些人代理列表
        /// </summary>
        /// <param name="getUserAgentPassiveList"></param>
        /// <returns></returns>
        public async Task<Result<List<UserAgentPassiveDto>>> GetUserAgentPassiveList(GetUserAgentPassiveList getUserAgentPassiveList)
        {
            var userAgentList = await _db.Queryable<UserAgentEntity>()
                                         .With(SqlWith.NoLock)
                                         .LeftJoin<UserInfoEntity>((useragent, substituteuser) => useragent.SubstituteUserId == substituteuser.UserId)
                                         .LeftJoin<UserInfoEntity>((useragent, substituteuser, agentuser) => useragent.AgentUserId == agentuser.UserId)
                                         .Where((useragent, substituteuser, agentuser) => useragent.SubstituteUserId == long.Parse(getUserAgentPassiveList.SubstituteUserId))
                                         .Select((useragent, substituteuser, agentuser) => new UserAgentPassiveDto
                                         {
                                             SubstituteUserId = useragent.SubstituteUserId,
                                             AgentUserId = agentuser.UserId,
                                             AgentUserNo = agentuser.UserNo,
                                             AgentUserName = _lang.Locale == "zh-CN"
                                                             ? agentuser.UserNameCn
                                                             : agentuser.UserNameEn,
                                             StartTime = useragent.StartTime,
                                             EndTime = useragent.EndTime
                                         }).ToListAsync();
            return Result<List<UserAgentPassiveDto>>.Ok(userAgentList.Adapt<List<UserAgentPassiveDto>>(), "");
        }

        /// <summary>
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy((dept, deptlevel) => deptlevel.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 查询被代理员工和代理员工是否已有代理关系
        /// </summary>
        /// <param name="substituteUserId"></param>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        public async Task<bool> GetSubAgentIsExist(long substituteUserId, long agentUserId)
        {
            return await _db.Queryable<UserAgentEntity>()
                            .With(SqlWith.NoLock)
                            .AnyAsync(useragent => useragent.SubstituteUserId == substituteUserId && useragent.AgentUserId == agentUserId);
        }

        /// <summary>
        /// 查询被代理员工是否代理了其他员工
        /// </summary>
        /// <param name="substituteUserId"></param>
        /// <returns></returns>
        public async Task<bool> GetSubAgentIsAgent(long substituteUserId)
        {
            return await _db.Queryable<UserAgentEntity>()
                            .With(SqlWith.NoLock)
                            .AnyAsync(useragent => useragent.AgentUserId == substituteUserId);
        }

        /// <summary>
        /// 查询代理员工是否被代理
        /// </summary>
        /// <param name="agentUserId"></param>
        /// <returns></returns>
        public async Task<bool> GetAgentIsSubAgent(long agentUserId)
        {
            return await _db.Queryable<UserAgentEntity>()
                            .With(SqlWith.NoLock)
                            .AnyAsync(useragent => useragent.SubstituteUserId == agentUserId);
        }
    }
}
