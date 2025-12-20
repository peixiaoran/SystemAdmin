using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormBasicInfo
{
    public class FormGroupRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormGroupRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增表单组别
        /// </summary>
        /// <param name="formGroupEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormGroupInfo(FormGroupEntity formGroupEntity)
        {
            return await _db.Insertable(formGroupEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除表单组别
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<int> DeleteFormGroupInfo(long formGroupId)
        {
            return await _db.Deleteable<FormGroupEntity>()
                            .Where(formgroup => formgroup.FormGroupId == formGroupId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除表单组别下的表单类别
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<int> DeleteFormTypeInfo(long formGroupId)
        {
            return await _db.Deleteable<FormTypeEntity>()
                            .Where(formgroup => formgroup.FormGroupId == formGroupId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工表单类别绑定
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserFormTypeBind(long formGroupId)
        {
            return await _db.Deleteable<UserFormBindEntity>()
                            .Where(userform => userform.FormGroupTypeId == formGroupId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除表单组别下的表单类别
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetFormTypeIds(long formGroupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .Where(x => x.FormGroupId == formGroupId)
                            .Select(x => x.FormTypeId)
                            .ToListAsync();
        }

        /// <summary>
        /// 删除表单组别下的表单类别
        /// </summary>
        /// <param name="formTypeIds"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserFromType(List<long> formTypeIds)
        {
            return await _db.Deleteable<UserFormBindEntity>()
                            .Where(formtype => formTypeIds.Contains(formtype.FormGroupTypeId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改表单组别
        /// </summary>
        /// <param name="formGroupEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormGroupInfo(FormGroupEntity formGroupEntity)
        {
            return await _db.Updateable(formGroupEntity)
                            .IgnoreColumns(formgroup => new
                            {
                                formgroup.FormGroupId,
                                formgroup.CreatedBy,
                                formgroup.CreatedDate,
                            }).Where(formgroup => formgroup.FormGroupId == formGroupEntity.FormGroupId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询表单组别实体
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<FormGroupDto> GetFormGroupEntity(long formGroupId)
        {
            var formGroupEntity = await _db.Queryable<FormGroupEntity>()
                                           .With(SqlWith.NoLock)
                                           .Where(formgroup => formgroup.FormGroupId == formGroupId)
                                           .FirstAsync();
            return formGroupEntity.Adapt<FormGroupDto>();
        }


        /// <summary>
        /// 查询表单组别分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<FormGroupDto>> GetFormGroupPage(GetFormGroupPage getFormGroupPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<FormGroupEntity>()
                           .With(SqlWith.NoLock);

            // 表单组别名称
            if (!string.IsNullOrEmpty(getFormGroupPage.FormGroupName))
            {
                query = query.Where(formgroup =>
                    formgroup.FormGroupNameCn.Contains(getFormGroupPage.FormGroupName) ||
                    formgroup.FormGroupNameEn.Contains(getFormGroupPage.FormGroupName));
            }

            var formGroupPage = await query.OrderBy(formgroup => formgroup.SortOrder)
                                           .Select(formgroup=> new FormGroupDto
                                           {
                                               FormGroupId = formgroup.FormGroupId,
                                               FormGroupNameCn = formgroup.FormGroupNameCn,
                                               FormGroupNameEn = formgroup.FormGroupNameEn,
                                               Description = _lang.Locale == "zh-CN"
                                                             ? formgroup.DescriptionCn
                                                             : formgroup.DescriptionEn,
                                           }).ToPageListAsync(getFormGroupPage.PageIndex, getFormGroupPage.PageSize, totalCount);
            return ResultPaged<FormGroupDto>.Ok(formGroupPage.Adapt<List<FormGroupDto>>(), totalCount, "");
        }
    }
}
