using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
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

        // <summary>
        /// 职业下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDrop()
        {
            var query = _db.Queryable<UserLaborEntity>().With(SqlWith.NoLock);
            if (_lang.Locale == "zh-CN")
            {
                query = query.OrderBy(labor => labor.LaborNameCn);
            }
            else
            {
                query = query.OrderBy(labor => labor.LaborNameEn);
            }

            return await query.Select(labor => new UserLaborDropDto
            {
                LaborId = labor.LaborId,
                LaborName = _lang.Locale == "zh-CN"
                            ? labor.LaborNameCn
                            : labor.LaborNameEn
            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDrop()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
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
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<PositionInfoDropDto>> GetPositionInfoDrop()
        {
            return await _db.Queryable<PositionInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(position => position.CreatedDate)
                            .Select((position) => new PositionInfoDropDto
                            {
                                PositionId = position.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? position.PositionNameCn
                                               : position.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询员工兼任分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeDto>> GetUserPartTimePage(GetUserPartTimePage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<UserPartTimeEntity>((user, userpart) => user.UserId == userpart.UserId)
                           .LeftJoin<DepartmentInfoEntity>((user, userpart, dept) => user.DepartmentId == dept.DepartmentId)
                           .LeftJoin<PositionInfoEntity>((user, userpart, dept, position) => user.PositionId == position.PositionId)
                           .LeftJoin<DepartmentInfoEntity>((user, userpart, dept, position, p_userdept) => userpart.PartTimeDeptId == p_userdept.DepartmentId)
                           .LeftJoin<PositionInfoEntity>((user, userpart, dept, position, p_userdept, p_userpos) => userpart.PartTimePositionId == p_userpos.PositionId)
                           .Where((user, userpart, dept, position, p_userdept, p_userpos) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((user, userpart, dept, position, p_userdept, p_userpos) =>
                    user.UserNo.Contains(getPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where((user, userpart, dept, position, p_userdept, p_userpos) =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门Id
            if (!string.IsNullOrEmpty(getPage.DepartmentId) && long.Parse(getPage.DepartmentId) > -1)
            {
                query = query.Where((user, userpart, dept, position, p_userdept, p_userpos) => p_userdept.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, userpart, dept, position, p_userdept, p_userpos) => new { PositionInfoOrder = position.SortOrder, user.HireDate, PartTimePositionOrder = p_userpos.SortOrder });

            var page = await query.Select((user, userpart, dept, position, p_userdept, p_userpos) => new UserPartTimeDto
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
                                                 ? position.PositionNameCn
                                                 : position.PositionNameEn,
                                      // 是否签核
                                      IsApproval = user.IsApproval,
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
                                      StartTime = userpart.StartTime,
                                      EndTime = userpart.EndTime,
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserPartTimeDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询员工兼任职业分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserPartTimeViewDto>> GetUserPartTimeView(GetUserInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .LeftJoin<PositionInfoEntity>((user, dept, position) => user.PositionId == position.PositionId)
                           .LeftJoin<NationalityInfoEntity>((user, dept, position, nation) =>
                            user.Nationality == nation.NationId)
                           .LeftJoin<UserLaborEntity>((user, dept, position, nation, labor) => user.LaborId == labor.LaborId)
                           .Where((user, dept, position, nation, labor) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((user, dept, position, nation, labor) =>
                    user.UserNo == getPage.UserNo);
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where((user, dept, position, nation, labor) =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门
            if (!string.IsNullOrEmpty(getPage.DepartmentId) && long.Parse(getPage.DepartmentId) > -1)
            {
                query = query.Where((user, dept, position, nation, labor) =>
                    user.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, position, nation, labor) => new { position.SortOrder, user.HireDate });

            var page = await query.Select((user, dept, position, nation, labor) => new UserPartTimeViewDto
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
                                                  ? position.PositionNameCn
                                                  : position.PositionNameEn,
                                       NationalityName = _lang.Locale == "zh-CN"
                                                  ? nation.NationNameCn
                                                  : nation.NationNameEn,
                                       LaborName = _lang.Locale == "zh-CN"
                                                  ? labor.LaborNameCn
                                                  : labor.LaborNameEn,
                                       IsApproval = user.IsApproval,
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserPartTimeViewDto>.Ok(page, totalCount, "");
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
        /// <param name="upsertdel"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserPartTime(UserPartTimeUpdateDel upsertdel)
        {
            return await _db.Deleteable<UserPartTimeEntity>()
                            .Where(userparttime => userparttime.UserId == long.Parse(upsertdel.Old_UserId) && userparttime.PartTimeDeptId == long.Parse(upsertdel.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(upsertdel.Old_PartTimePositionId))
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
        /// 查询员工兼任是否有重复（按照员工Id、兼任部门、兼任职级）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="partTimeDeptId"></param>
        /// <param name="partTimePositionId"></param>
        /// <returns></returns>
        public async Task<bool> GetUserPartTimeCount(long userId, long partTimeDeptId, long partTimePositionId)
        {
            return await _db.Queryable<UserPartTimeEntity>()
                            .Where(userparttime => userparttime.UserId == userId && userparttime.PartTimeDeptId == partTimeDeptId && userparttime.PartTimePositionId == partTimePositionId)
                            .AnyAsync();
        }

        /// <summary>
        /// 查询员工兼任实体
        /// </summary>
        /// <param name="getEntity"></param>
        /// <returns></returns>
        public async Task<UserPartTimeDto> GetUserPartTimeList(GetUserPartTimeEntity getEntity)
        {
            var entity = await _db.Queryable<UserPartTimeEntity>()
                                  .Where(userparttime => userparttime.UserId == long.Parse(getEntity.UserId) && userparttime.PartTimeDeptId == long.Parse(getEntity.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(getEntity.Old_PartTimePositionId))
                                  .FirstAsync();
            return entity.Adapt<UserPartTimeDto>();
        }

        /// <summary>
        /// 修改员工兼任
        /// </summary>
        /// <param name="upsertdel"></param>
        /// <param name="userPartTimeEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserPartTime(UserPartTimeUpdateDel upsertdel, UserPartTimeEntity userPartTimeEntity)
        {
            return await _db.Updateable(userPartTimeEntity)
                            .IgnoreColumns(userparttime => new
                            {
                                userparttime.CreatedBy,
                                userparttime.CreatedDate,
                            }).Where(userparttime => userparttime.UserId == long.Parse(upsertdel.Old_UserId) && userparttime.PartTimeDeptId == long.Parse(upsertdel.Old_PartTimeDeptId) && userparttime.PartTimePositionId == long.Parse(upsertdel.Old_PartTimePositionId)).ExecuteCommandAsync();
        }
    }
}
