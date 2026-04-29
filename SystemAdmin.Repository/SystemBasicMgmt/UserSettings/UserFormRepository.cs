using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.UserSettings
{
    public class UserFormRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public UserFormRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
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

        /// <summary>
        /// 查询员工分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<UserFormDto>> GetUserInfoPage(GetUserFormPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<DepartmentInfoEntity>((user, dept) => user.DepartmentId == dept.DepartmentId)
                           .InnerJoin<PositionInfoEntity>((user, dept, position) => user.PositionId == position.PositionId)
                           .InnerJoin<UserLaborEntity>((user, dept, position, labor) => user.LaborId == labor.LaborId)
                           .InnerJoin<NationalityInfoEntity>((user, dept, position, labor, nation) =>
                            user.Nationality == nation.NationId)
                           .Where((user, dept, position, labor, nation) => user.IsEmployed == 1 && user.IsFreeze == 0);

            // 员工工号
            if (!string.IsNullOrEmpty(getPage.UserNo))
            {
                query = query.Where((user, dept, position, labor, nation) =>
                    user.UserNo.Contains(getPage.UserNo));
            }
            // 员工姓名
            if (!string.IsNullOrEmpty(getPage.UserName))
            {
                query = query.Where((user, dept, position, labor, nation) =>
                    user.UserNameCn.Contains(getPage.UserName) ||
                    user.UserNameEn.Contains(getPage.UserName));
            }
            // 部门Id
            if (!string.IsNullOrEmpty(getPage.DepartmentId) && long.Parse(getPage.DepartmentId) > 0)
            {
                query = query.Where((user, dept, position, labor, nation) =>
                    user.DepartmentId == long.Parse(getPage.DepartmentId));
            }

            // 排序
            query = query.OrderBy((user, dept, position, labor, nation) => new { position.SortOrder, user.HireDate });

            var page = await query.Select((user, dept, position, labor, nation) => new UserFormDto
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
                                 LaborName = _lang.Locale == "zh-CN"
                                            ? labor.LaborNameCn
                                            : labor.LaborNameEn,
                                 NationalityName = _lang.Locale == "zh-CN"
                                            ? nation.NationNameCn
                                            : nation.NationNameEn,
                                 IsApproval = user.IsApproval,
                             }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<UserFormDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询员工绑定表单树
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserFormViewTreeDto>> GetUserFormViewTree(long userId)
        {
            var formGroup = await _db.Queryable<FormGroupEntity>()
                          .With(SqlWith.NoLock)
                          .LeftJoin<UserFormEntity>((formgroup, userform) => formgroup.FormGroupId == userform.FormGroupTypeId && userform.UserId == userId)
                          .OrderBy((formgroup, userform) => formgroup.SortOrder)
                          .Select((formgroup, userform) => new UserFormViewTreeDto
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
                              IsChecked = SqlFunc.IsNull(userform.UserId, 0) > 0,
                              FormTypeChildren = new List<UserFormViewTreeDto>()
                          }).ToListAsync();

            var formType = await _db.Queryable<FormTypeEntity>()
                                    .With(SqlWith.NoLock)
                                    .LeftJoin<UserFormEntity>((formtype, userform) => formtype.FormTypeId == userform.FormGroupTypeId && userform.UserId == userId)
                                    .OrderBy((formtype, userform) => formtype.SortOrder)
                                    .Select((formtype, userform) => new UserFormViewTreeDto
                                    {
                                        ParentId = formtype.FormGroupId,
                                        FormGroupTypeId = formtype.FormTypeId,
                                        FormGroupTypeName = _lang.Locale == "zh-CN" 
                                                            ? formtype.FormTypeNameCn 
                                                            : formtype.FormTypeNameEn,
                                        Description = _lang.Locale == "zh-CN" 
                                                            ? formtype.DescriptionCn 
                                                            : formtype.DescriptionEn,
                                        IsChecked = SqlFunc.IsNull(userform.UserId, 0) > 0,
                                        FormTypeChildren = new List<UserFormViewTreeDto>()
                                    }).ToListAsync();

            var formTypeLookup = formType.ToLookup(formtype => formtype.ParentId);

            return formGroup.Select(group =>
            {
                group.FormTypeChildren = formTypeLookup[group.FormGroupTypeId].ToList();
                return group;
            }).ToList();
        }

        /// <summary>
        /// 删除员工表单绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserForm(long userId)
        {
            return await _db.Deleteable<UserFormEntity>(userform => userform.UserId == userId).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增员工表单绑定
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public async Task<int> InsertUserForm(List<UserFormEntity> entityList)
        {
            return await _db.Insertable(entityList).ExecuteCommandAsync();
        }
    }
}
