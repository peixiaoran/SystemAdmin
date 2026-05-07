using Mapster;
using SqlSugar;
using System.Data;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData
{
    public class UserInfoRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserInfoRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 国籍下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<NationalityDropDto>> GetNationalityDrop()
        {
            return await _db.Queryable<NationalityInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Select(nation => new NationalityDropDto
                            {
                                NationId = nation.NationId,
                                NationName = _lang.Locale == "zh-CN"
                                             ? nation.NationNameCn
                                             : nation.NationNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<PositionDropDto>> GetPositionDrop()
        {
            return await _db.Queryable<PositionInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(position => position.CreatedDate)
                            .Select((position) => new PositionDropDto
                            {
                                PositionId = position.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? position.PositionNameCn
                                               : position.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
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
                            .OrderBy(dept => dept.SortOrder)
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
        /// 角色下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleInfoDropDto>> GetRoleDrop()
        {
            return await _db.Queryable<RoleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Select(role => new RoleInfoDropDto
                            {
                                RoleId = role.RoleId,
                                RoleName = _lang.Locale == "zh-CN"
                                           ? role.RoleNameCn
                                           : role.RoleNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserInfo(UserInfoEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增员工角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertUserRole(UserRoleEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserInfo(long userId)
        {
            return await _db.Deleteable<UserInfoEntity>()
                            .Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工角色对照信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserRoleInfo(long userId)
        {
            return await _db.Deleteable<UserRoleEntity>()
                            .Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工代理
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAgent(long userId)
        {
            return await _db.Deleteable<UserAgentEntity>()
                            .Where(useragent => useragent.AgentUserId == userId || useragent.SubstituteUserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工兼任
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserPartTime(long userId)
        {
            return await _db.Deleteable<UserPartTimeEntity>()
                            .Where(useragent => useragent.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工表单绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserForm(long userId)
        {
            return await _db.Deleteable<UserFormEntity>()
                            .Where(formbind => formbind.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工账号锁定记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserLock(long userId)
        {
            return await _db.Deleteable<UserLockEntity>()
                            .Where(formbind => formbind.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserInfo(UserInfoEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(user => new
                            {
                                user.UserId,
                                user.CreatedBy,
                                user.CreatedDate,
                                user.IsAgent,
                                user.IsPartTime,
                            }).Where(user => user.UserId == entity.UserId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改员工角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserRoleInfo(UserRoleEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(userrole => new
                            {
                                userrole.UserId,
                                userrole.CreatedBy,
                                userrole.CreatedDate,
                            }).Where(userrole => userrole.UserId == entity.UserId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改员工头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userAvatar"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAvatar(long userId, string userAvatar)
        {
            return await _db.Updateable<UserInfoEntity>()
                            .SetColumns(user => user.AvatarAddress == userAvatar)
                            .Where(user => user.UserId == userId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询员工实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoEntityDto> GetUserInfoEntity(long userId)
        {
            var entity = await _db.Queryable<UserInfoEntity>()
                                  .With(SqlWith.NoLock)
                                  .InnerJoin<UserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                                  .InnerJoin<RoleInfoEntity>((user, userrole, role) => userrole.RoleId == role.RoleId)
                                  .Where(user => user.UserId == userId)
                                  .Select((user, userrole, role) => new UserInfoEntityDto
                                  {
                                      UserId = user.UserId,
                                      UserNo = user.UserNo,
                                      UserNameCn = user.UserNameCn,
                                      UserNameEn = user.UserNameEn,
                                      Gender = user.Gender,
                                      LoginNo = user.LoginNo,
                                      DepartmentId = user.DepartmentId,
                                      LaborId = user.LaborId,
                                      PositionId = user.PositionId,
                                      RoleId = role.RoleId,
                                      HireDate = Convert.ToDateTime(user.HireDate).ToString("yyyy-MM-dd"),
                                      Nationality = user.Nationality,
                                      AvatarAddress = user.AvatarAddress,
                                      Email = user.Email,
                                      PhoneNumber = user.PhoneNumber,
                                      IsEmployed = user.IsEmployed,
                                      IsApproval = user.IsApproval,
                                      IsRealtimeNotification = user.IsRealtimeNotification,
                                      IsScheduledNotification = user.IsScheduledNotification,
                                      IsAgent = user.IsAgent,
                                      IsPartTime = user.IsPartTime,
                                      IsFreeze = user.IsFreeze,
                                      ExpirationDays = user.ExpirationDays,
                                      ExpirationTime = user.ExpirationTime
                                  }).FirstAsync();
            return entity.Adapt<UserInfoEntityDto>();
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserInfoPageDto>> GetUserInfoPage(GetUserInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<UserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                           .InnerJoin<DepartmentInfoEntity>((user, userrole, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<PositionInfoEntity>((user, userrole, dept, position) => user.PositionId == position.PositionId)
                           .InnerJoin<NationalityInfoEntity>((user, userrole, dept, position, nation) => user.Nationality == nation.NationId);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where(user =>
                    user.UserNo.Contains(getPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where(user =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门Id
            if (getPage.DepartmentId != "-1")
            {
                query = query.Where(user => user.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, userrole, dept, position, nation) => new { position.SortOrder, user.HireDate });

            var page = await query.Select((user, userrole, dept, position, nation) => new UserInfoPageDto
            {
                UserId = user.UserId,
                DepartmentId = user.DepartmentId,
                DepartmentName = _lang.Locale == "zh-CN"
                                                       ? dept.DepartmentNameCn
                                                       : dept.DepartmentNameEn,
                UserNo = user.UserNo,
                UserNameCn = user.UserNameCn,
                UserNameEn = user.UserNameEn,
                PositionName = _lang.Locale == "zh-CN"
                                                       ? position.PositionNameCn
                                                       : position.PositionNameEn,
                Gender = user.Gender,
                IsEmployed = user.IsEmployed,
                IsApproval = user.IsApproval,
                IsFreeze = user.IsFreeze,
                Remark = user.Remark
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserInfoPageDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询员工密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserPasswordAndSalt(long userId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(user => user.UserId == userId)
                            .Select(user => new UserInfoEntity
                            {
                                PassWord = user.PassWord,
                                PwdSalt = user.PwdSalt
                            }).FirstAsync();
        }

        /// <summary>
        /// 工号是否重复
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="loginNo"></param>
        /// <returns></returns>
        public async Task<bool> UserNoIsExist(string userNo, string loginNo)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(user => user.UserNo == userNo || user.LoginNo == loginNo)
                            .AnyAsync();
        }

        /// <summary>
        /// 查询员工信息列表（导出Excel）
        /// </summary>
        /// <param name="getUserExcel"></param>
        /// <returns></returns>
        public async Task<DataTable> GetUserInfoExcel(GetUserInfoExcel getUserExcel)
        {
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<UserRoleEntity>((user, userrole) => user.UserId == userrole.UserId)
                           .InnerJoin<DepartmentInfoEntity>((user, userrole, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<PositionInfoEntity>((user, userrole, dept, position) => user.PositionId == position.PositionId)
                           .InnerJoin<NationalityInfoEntity>((user, userrole, dept, position, nation) => user.Nationality == nation.NationId);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserExcel.UserNo))
            {
                query = query.Where(user =>
                    user.UserNo.Contains(getUserExcel.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserExcel.UserName))
            {
                query = query.Where(user =>
                    user.UserNameCn.Contains(getUserExcel.UserName) ||
                    user.UserNameEn.Contains(getUserExcel.UserName));
            }
            // 部门Id
            if (getUserExcel.DepartmentId != "-1")
            {
                query = query.Where(user => user.DepartmentId == long.Parse(getUserExcel.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, userrole, dept, position, nation) => new { position.SortOrder, user.HireDate });

            return await query.Select((user, userrole, dept, position, nation) =>
                               new UserInfoExcelDto
                               {
                                   DepartmentName = _lang.Locale == "zh-CN"
                                                    ? dept.DepartmentNameCn
                                                    : dept.DepartmentNameEn,
                                   UserNo = user.UserNo,
                                   UserNameCn = user.UserNameCn,
                                   UserNameEn = user.UserNameEn,
                                   PositionName = _lang.Locale == "zh-CN"
                                                    ? position.PositionNameCn
                                                    : position.PositionNameEn,
                                   HireDate = user.HireDate,
                                   GenderName = _lang.Locale == "zh-CN"
                                                    ? (user.Gender == 1 ? "男" : "女")
                                                    : (user.Gender == 1 ? "Male" : "Female"),
                                   NationalityName = _lang.Locale == "zh-CN"
                                                       ? nation.NationNameCn
                                                       : nation.NationNameEn,
                                   Email = user.Email,
                                   PhoneNumber = user.PhoneNumber,
                                   IsEmployedName = _lang.Locale == "zh-CN"
                                                    ? (user.IsEmployed == 1 ? "在职" : "离职")
                                                    : (user.IsEmployed == 1 ? "Yes" : "No"),
                                   IsApprovalName = _lang.Locale == "zh-CN"
                                                    ? (user.IsApproval == 1 ? "需要签核" : "无需签核")
                                                    : (user.IsApproval == 1 ? "Yes" : "No"),
                                   IsFreezeName = _lang.Locale == "zh-CN"
                                                    ? (user.IsFreeze == 1 ? "已冻结" : "未冻结")
                                                    : (user.IsFreeze == 1 ? "Yes" : "No"),
                               }).ToDataTableAsync();
        }
    }
}
