using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.UserSettings
{
    public class UserPartTimeRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserPartTimeRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询员工兼任分页
        /// </summary>
        /// <param name="getUserPartTimePage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getUserPartTimePage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<UserPartTimeEntity>((user, userpart) => user.UserId == userpart.UserId)
                           .LeftJoin<DictionaryInfoEntity>((user, userpart, approvaldic) =>
                            approvaldic.DicType == "ApprovalState" && user.IsApproval == approvaldic.DicCode)
                           .LeftJoin<DepartmentInfoEntity>((user, userpart, approvaldic, dept) => user.DepartmentId == dept.DepartmentId)
                           .LeftJoin<UserPositionEntity>((user, userpart, approvaldic, dept, userpos) => user.PositionId == userpos.PositionId)
                           .LeftJoin<DepartmentInfoEntity>((user, userpart, approvaldic, dept, userpos, p_userdept) => userpart.PartTimeDeptId == p_userdept.DepartmentId)
                           .LeftJoin<UserPositionEntity>((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos) => userpart.PartTimePositionId == p_userpos.PositionId)
                           .LeftJoin<UserLaborEntity>((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) => p_userlabor.LaborId == userpart.PartTimeLaborId)
                           .Where((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserPartTimePage.UserNo))
            {
                query = query.Where((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) =>
                    user.UserNo.Contains(getUserPartTimePage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserPartTimePage.UserName))
            {
                query = query.Where((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) =>
                    user.UserNameCn.Contains(getUserPartTimePage.UserName) ||
                    user.UserNameEn.Contains(getUserPartTimePage.UserName));
            }
            // 部门 Id（仅在工号与姓名都为空时）
            if (!string.IsNullOrEmpty(getUserPartTimePage.DepartmentId)
                && string.IsNullOrEmpty(getUserPartTimePage.UserNo)
                && string.IsNullOrEmpty(getUserPartTimePage.UserName))
            {
                query = query.Where((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) => p_userdept.DepartmentId == long.Parse(getUserPartTimePage.DepartmentId));
            }

            var userPartTimePage = await query.OrderBy((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) => new { UserPositionOrder = userpos.PositionOrderBy, user.HireDate, PartTimePositionOrder = p_userpos.PositionOrderBy })
            .Select((user, userpart, approvaldic, dept, userpos, p_userdept, p_userpos, p_userlabor) => new UserPartTimeDto
            {
                UserId = user.UserId,
                UserNo = user.UserNo,
                // 员工姓名
                UserName = _lang.Locale == "zh-CN"
                           ? user.UserNameCn
                           : user.UserNameEn,
                // 部门名称
                DepartmentName = _lang.Locale == "zh-CN"
                           ? dept.DepartmentNameCn
                           : dept.DepartmentNameEn,
                // 职级名称
                PositionName = _lang.Locale == "zh-CN"
                           ? userpos.PositionNameCn
                           : userpos.PositionNameEn,
                // 是否签核
                IsApproval = user.IsApproval,
                IsApprovalName = _lang.Locale == "zh-CN"
                           ? approvaldic.DicNameCn
                           : approvaldic.DicNameEn,
                // 兼任部门名称
                PartTimeDeptId = userpart.PartTimeDeptId,
                PartTimeDeptName = _lang.Locale == "zh-CN"
                           ? p_userdept.DepartmentNameCn
                           : p_userdept.DepartmentNameEn,
                // 兼任职级名称
                PartTimePositionId = userpart.PartTimePositionId,
                PartTimePositionName = _lang.Locale == "zh-CN"
                           ? p_userpos.PositionNameCn
                           : p_userpos.PositionNameEn,
                // 兼任职业名称
                PartTimeLaborId = userpart.PartTimeLaborId,
                PartTimeLaborName = _lang.Locale == "zh-CN"
                           ? p_userlabor.LaborNameCn
                           : p_userlabor.LaborNameEn,
                StartTime = userpart.StartTime,
                EndTime = userpart.EndTime,
            }).ToPageListAsync(getUserPartTimePage.PageIndex, getUserPartTimePage.PageSize, totalCount);
            return ResultPaged<UserPartTimeDto>.Ok(userPartTimePage, totalCount, "");
        }

        /// <summary>
        /// 查询员工兼任职业分页
        /// </summary>
        /// <param name="getUserPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView(GetUserInfoPage getUserPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .LeftJoin<UserPositionEntity>((user, dept, userpos) => user.PositionId == userpos.PositionId)
                           .LeftJoin<NationalityInfoEntity>((user, dept, userpos, nation) =>
                            user.Nationality == nation.NationId)
                           .LeftJoin<UserLaborEntity>((user, dept, userpos, nation, userlabor) => user.LaborId == userlabor.LaborId)
                           .LeftJoin<DictionaryInfoEntity>((user, dept, userpos, nation, userlabor, approvaldic) =>
                            approvaldic.DicType == "ApprovalState" && user.IsApproval == approvaldic.DicCode)
                           .Where((user, dept, userpos, nation, userlabor, approvaldic) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserPage.UserNo))
            {
                query = query.Where((user, dept, userpos, nation, userlabor, approvaldic) =>
                    user.UserNo == getUserPage.UserNo);
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserPage.UserName))
            {
                query = query.Where((user, dept, userpos, nation, userlabor, approvaldic) =>
                    user.UserNameCn.Contains(getUserPage.UserName) ||
                    user.UserNameEn.Contains(getUserPage.UserName));
            }
            // 部门 Id（仅在工号与姓名均为空时筛选）
            if (!string.IsNullOrEmpty(getUserPage.DepartmentId)
                && string.IsNullOrEmpty(getUserPage.UserNo)
                && string.IsNullOrEmpty(getUserPage.UserName))
            {
                query = query.Where((user, dept, userpos, nation, userlabor, approvaldic) =>
                    user.DepartmentId == long.Parse(getUserPage.DepartmentId));
            }

            var userPartTimeViewPage = await query.OrderBy((user, dept, userpos, nation, userlabor, approvaldic) => new { userpos.PositionOrderBy, user.HireDate })
            .Select((user, dept, userpos, nation, userlabor, approvaldic) => new UserPartTimeViewDto
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
                 NationalityName = _lang.Locale == "zh-CN"
                            ? nation.NationNameCn
                            : nation.NationNameEn,
                 LaborName = _lang.Locale == "zh-CN"
                            ? userlabor.LaborNameCn
                            : userlabor.LaborNameEn,
                 IsApproval = user.IsApproval,
                 IsApprovalName = _lang.Locale == "zh-CN"
                            ? approvaldic.DicNameCn
                            : approvaldic.DicNameEn,
            }).ToPageListAsync(getUserPage.PageIndex, getUserPage.PageSize, totalCount);
            return ResultPaged<UserPartTimeViewDto>.Ok(userPartTimeViewPage, totalCount, "");
        }

        /// <summary>
        /// 新增员工兼任
        /// </summary>
        /// <param name="userPartTimeEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserPartTime(UserPartTimeEntity userPartTimeEntity)
        {
            return await _db.Insertable(userPartTimeEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工兼任
        /// </summary>
        /// <param name="userPartTimeUpdateDel"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserPartTime(UserPartTimeUpdateDel userPartTimeUpdateDel)
        {
            return await _db.Deleteable<UserPartTimeEntity>()
                            .Where(userparttime => userparttime.UserId == long.Parse(userPartTimeUpdateDel.Old_UserId) && userparttime.PartTimeDeptId == long.Parse(userPartTimeUpdateDel.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(userPartTimeUpdateDel.Old_PartTimePositionId) && userparttime.PartTimeLaborId == long.Parse(userPartTimeUpdateDel.Old_PartTimeLaborId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询员工是否还有兼任
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> GetUserPartTimeIsExist(long userId)
        {
            return await _db.Queryable<UserPartTimeEntity>()
                            .Where(userparttime => userparttime.UserId == userId)
                            .AnyAsync();
        }

        /// <summary>
        /// 修改员工兼任状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isPartTime"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserPartTime(long userId, int isPartTime)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .Where(user => user.UserId == userId)
                            .SetColumns(user => user.IsPartTime == isPartTime)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询员工兼任是否有重复（按照员工Id、兼任部门、兼任职级、兼任职业）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="partTimeDeptId"></param>
        /// <param name="partTimePositionId"></param>
        /// <param name="partTimeLaborId"></param>
        /// <returns></returns>
        public async Task<int> GetUserPartTimeCount(long userId, long partTimeDeptId, long partTimePositionId, long partTimeLaborId)
        {
            return await _db.Queryable<UserPartTimeEntity>()
                            .Where(userparttime => userparttime.UserId == userId && userparttime.PartTimeDeptId == partTimeDeptId && userparttime.PartTimePositionId == partTimePositionId && userparttime.PartTimeLaborId == partTimeLaborId)
                            .CountAsync();
        }

        /// <summary>
        /// 查询员工兼任实体
        /// </summary>
        /// <param name="getUserPartTimeEntity"></param>
        /// <returns></returns>
        public async Task<UserPartTimeDto> GetUserPartTimeList(GetUserPartTimeEntity getUserPartTimeEntity)
        {
            var userPartTimeEntity = await _db.Queryable<UserPartTimeEntity>()
                                              .Where(userparttime => userparttime.UserId == long.Parse(getUserPartTimeEntity.UserId) && userparttime.PartTimeDeptId == long.Parse(getUserPartTimeEntity.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(getUserPartTimeEntity.Old_PartTimePositionId) && userparttime.PartTimeLaborId == long.Parse(getUserPartTimeEntity.Old_PartTimeLaborId))
                                              .FirstAsync();
            return userPartTimeEntity.Adapt<UserPartTimeDto>();
        }

        /// <summary>
        /// 修改员工兼任
        /// </summary>
        /// <param name="userPartTimeUpdateDel"></param>
        /// <param name="userPartTimeEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserPartTime(UserPartTimeUpdateDel userPartTimeUpdateDel, UserPartTimeEntity userPartTimeEntity)
        {
            return await _db.Updateable(userPartTimeEntity)
                            .IgnoreColumns(userparttime => new
                            {
                                userparttime.CreatedBy,
                                userparttime.CreatedDate,
                            }).Where(userparttime => userparttime.UserId == long.Parse(userPartTimeUpdateDel.Old_UserId) && userparttime.PartTimeDeptId == long.Parse(userPartTimeUpdateDel.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(userPartTimeUpdateDel.Old_PartTimePositionId) && userparttime.PartTimeLaborId == long.Parse(userPartTimeUpdateDel.Old_PartTimeLaborId)).ExecuteCommandAsync();
        }

        // <summary>
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDropDown()
        {
            return await _db.Queryable<UserLaborEntity>()
                            .With(SqlWith.NoLock)
                            .Select(userlabor => new UserLaborDropDto
                            {
                                LaborId = userlabor.LaborId,
                                LaborName = _lang.Locale == "zh-CN"
                                            ? userlabor.LaborNameCn
                                            : userlabor.LaborNameEn
                            }).ToListAsync();
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
                                Disabled = dept.IsEnabled == 0
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDropDto>> GetUserPositionDropDown()
        {
            return await _db.Queryable<UserPositionEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(userpos => userpos.CreatedDate)
                            .Select((userpos) => new UserPositionDropDto
                            {
                                PositionId = userpos.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? userpos.PositionNameCn
                                               : userpos.PositionNameEn
                            }).ToListAsync();
        }
    }
}
