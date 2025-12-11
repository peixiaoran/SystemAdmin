using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemSettings
{
    public class UserLoginLogRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserLoginLogRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询员工登录日志分页
        /// </summary>
        /// <param name="getUserLoginLogPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLogOutDto>> GetUserLoginLogPage(GetUserLoginLogPage getUserLoginLogPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserLogOutEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<UserInfoEntity>((userloginlog, userinfo) => userloginlog.UserId == userinfo.UserId)
                           .LeftJoin<DictionaryInfoEntity>((userloginlog, userinfo, loginbehaviordic) => userloginlog.StatusId == loginbehaviordic.DicCode && loginbehaviordic.DicType == "LoginBehavior")
                           .OrderByDescending((userloginlog, userinfo, loginbehaviordic) => new { userloginlog.LoginDate});

            // IP
            if (!string.IsNullOrEmpty(getUserLoginLogPage.IP))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => userloginlog.IP.Contains(getUserLoginLogPage.IP));
            }
            // 员工工号
            if (!string.IsNullOrEmpty(getUserLoginLogPage.UserNo))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => userinfo.UserNo.Contains(getUserLoginLogPage.UserNo));
            }
            // 开始时间
            if (!string.IsNullOrEmpty(getUserLoginLogPage.StartTime))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => Convert.ToDateTime(userloginlog.LoginDate) >= Convert.ToDateTime(getUserLoginLogPage.StartTime));
            }
            // 结束时间
            if (!string.IsNullOrEmpty(getUserLoginLogPage.EndTime))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => Convert.ToDateTime(userloginlog.LoginDate) <= Convert.ToDateTime(getUserLoginLogPage.EndTime));
            }

            var userLoginLogPage = await query.Select((userloginlog, userinfo, loginbehaviordic) => new UserLogOutDto
            {
                UserId = userloginlog.UserId,
                UserNo = userinfo.UserNo,
                UserNameCn = userinfo.UserNameCn,
                UserNameEn = userinfo.UserNameEn,
                IP = userloginlog.IP,
                StatusId = userloginlog.StatusId,
                StatusName = _lang.Locale == "zh-cn"
                             ? loginbehaviordic.DicNameCn
                             : loginbehaviordic.DicNameEn,
                LoginDate = userloginlog.LoginDate,
            }).ToPageListAsync(getUserLoginLogPage.PageIndex, getUserLoginLogPage.PageSize, totalCount);
            return ResultPaged<UserLogOutDto>.Ok(userLoginLogPage, totalCount, "");
        }
    }
}
