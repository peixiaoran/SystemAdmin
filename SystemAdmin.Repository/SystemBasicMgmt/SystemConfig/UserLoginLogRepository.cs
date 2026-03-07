using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemConfig
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
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserLogOutDto>> GetUserLoginLogPage(GetUserLoginLogPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserLogOutEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<UserInfoEntity>((userloginlog, userinfo) => userloginlog.UserId == userinfo.UserId)
                           .LeftJoin<DictionaryInfoEntity>((userloginlog, userinfo, loginbehaviordic) => userloginlog.LoginType == loginbehaviordic.DicCode && loginbehaviordic.DicType == "LoginBehavior")
                           .OrderByDescending((userloginlog, userinfo, loginbehaviordic) => new { userloginlog.LoginDate});

            // IP
            if (!string.IsNullOrEmpty(getPage.IP))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => userloginlog.IP.Contains(getPage.IP));
            }
            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => userinfo.UserNo.Contains(getPage.UserNo));
            }
            // 开始时间
            if (!string.IsNullOrEmpty(getPage.StartTime))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => Convert.ToDateTime(userloginlog.LoginDate) >= Convert.ToDateTime(getPage.StartTime));
            }
            // 结束时间
            if (!string.IsNullOrEmpty(getPage.EndTime))
            {
                query = query.Where((userloginlog, userinfo, loginbehaviordic) => Convert.ToDateTime(userloginlog.LoginDate) <= Convert.ToDateTime(getPage.EndTime));
            }

            var page = await query.Select((userloginlog, userinfo, loginbehaviordic) => new UserLogOutDto
            {
                UserNo = userinfo.UserNo,
                UserName = _lang.Locale == "zh-CN"
                           ? userinfo.UserNameCn
                           : userinfo.UserNameEn,
                IP = userloginlog.IP,
                LoginType = userloginlog.LoginType,
                LoginTypeName = _lang.Locale == "zh-CN"
                           ? loginbehaviordic.DicNameCn
                           : loginbehaviordic.DicNameEn,
                LoginDate = userloginlog.LoginDate,
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserLogOutDto>.Ok(page, totalCount, "");
        }
    }
}
