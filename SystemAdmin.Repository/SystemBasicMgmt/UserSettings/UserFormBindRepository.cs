using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.UserSettings
{
    public class UserFormBindRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserFormBindRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getUserFormBindPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserFormBindDto>> GetUserInfoPage(GetUserFormBindPage getUserFormBindPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<UserPositionEntity>((user, dept, userpos) => user.PositionId == userpos.PositionId)
                           .InnerJoin<UserLaborEntity>((user, dept, userpos, userlabor) => user.LaborId == userlabor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, dept, userpos, userlabor, nation) =>
                            user.Nationality == nation.NationId)
                           .Where((user, dept, userpos, userlabor, nation) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getUserFormBindPage.UserNo))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNo.Contains(getUserFormBindPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getUserFormBindPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.UserNameCn.Contains(getUserFormBindPage.UserName) ||
                    user.UserNameEn.Contains(getUserFormBindPage.UserName));
            }
            // 部门 Id（仅在工号与姓名为空时）
            if (!string.IsNullOrEmpty(getUserFormBindPage.DepartmentId)
                && string.IsNullOrEmpty(getUserFormBindPage.UserNo)
                && string.IsNullOrEmpty(getUserFormBindPage.UserName))
            {
                query = query.Where((user, dept, userpos, userlabor, nation) =>
                    user.DepartmentId == long.Parse(getUserFormBindPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, userpos, userlabor, nation) => new { userpos.SortOrder, user.HireDate });

            var userPage = await query
            .Select((user, dept, userpos, userlabor, nation) => new UserFormBindDto
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
                IsApproval = user.IsApproval,
            }).ToPageListAsync(getUserFormBindPage.PageIndex, getUserFormBindPage.PageSize, totalCount);
            return ResultPaged<UserFormBindDto>.Ok(userPage, totalCount, "");
        }

        /// <summary>
        /// 查询员工绑定表单树
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserFormBindViewTreeDto>> GetUserFormBindViewTree(long userId)
        {
            // 查询员工已绑定的表单组别
            var formGroupBind = await _db.Queryable<FormGroupEntity>()
                                         .With(SqlWith.NoLock)
                                         .LeftJoin<UserFormBindEntity>((formgroup, userformbind) => formgroup.FormGroupId == userformbind.FormGroupTypeId && userformbind.UserId == userId)
                                         .OrderBy((formgroup, userformbind) => formgroup.SortOrder)
                                         .Select((formgroup, userformbind) => new UserFormBindViewTreeDto
                                         {
                                             ParentId = 0,
                                             FormGroupTypeId = formgroup.FormGroupId,
                                             FormGroupTypeName = _lang.Locale == "zh-CN"
                                                                 ? formgroup.FormGroupNameCn
                                                                 : formgroup.FormGroupNameEn,
                                             Description = _lang.Locale == "zh-CN"
                                                                 ? formgroup.DescriptionCn
                                                                 : formgroup.DescriptionEn,
                                             Disabled = false,
                                             IsChecked = SqlFunc.IsNull(userformbind.UserId, 0) > 0,
                                             FormTypeChildren = new List<UserFormBindViewTreeDto>()
                                         }).ToListAsync();
            // 查询员工已绑定的表类型
            var formTypeBind = await _db.Queryable<FormTypeEntity>()
                                        .With(SqlWith.NoLock)
                                        .LeftJoin<UserFormBindEntity>((formtype, userformbind) => formtype.FormTypeId == userformbind.FormGroupTypeId && userformbind.UserId == userId)
                                        .OrderBy((formtype, userformbind) => formtype.SortOrder)
                                        .Select((formtype, userformbind) => new UserFormBindViewTreeDto
                                        {
                                            ParentId = formtype.FormGroupId,
                                            FormGroupTypeId = formtype.FormTypeId,
                                            FormGroupTypeName = _lang.Locale == "zh-CN"
                                                                ? formtype.FormTypeNameCn
                                                                : formtype.FormTypeNameEn,
                                            Description = _lang.Locale == "zh-CN"
                                                                ? formtype.DescriptionCn
                                                                : formtype.DescriptionEn,
                                            IsChecked = SqlFunc.IsNull(userformbind.UserId, 0) > 0,
                                            FormTypeChildren = new List<UserFormBindViewTreeDto>()
                                        }).ToListAsync();
            var userFormBind = new List<UserFormBindViewTreeDto>();
            // 组装树形结构
            foreach (var group in formGroupBind)
            {
                var getFormTypeBind = formTypeBind.Where(formgrouptype => formgrouptype.ParentId == group.FormGroupTypeId);
                userFormBind.Add(new UserFormBindViewTreeDto
                {
                    FormGroupTypeId = group.FormGroupTypeId,
                    FormGroupTypeName = group.FormGroupTypeName,
                    Description = group.Description,
                    Disabled = group.Disabled,
                    IsChecked = group.IsChecked,
                    FormTypeChildren = getFormTypeBind.ToList()
                });
            }
            return userFormBind;
        }

        /// <summary>
        /// 删除员工表单绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserFormBind(long userId)
        {
            return await _db.Deleteable<UserFormBindEntity>(userform => userform.UserId == userId).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增员工表单绑定
        /// </summary>
        /// <param name="userFormBindList"></param>
        /// <returns></returns>
        public async Task<int> InsertUserFormBind(List<UserFormBindEntity> userFormBindList)
        {
            return await _db.Insertable(userFormBindList).ExecuteCommandAsync();
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
                            .OrderBy((dept, deptlevel) => new { deptlevel.DepartmentLevelCode, dept.SortOrder })
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }
    }
}
