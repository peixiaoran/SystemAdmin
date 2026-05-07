using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormBasicInfo
{
    public class FormTypeRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormTypeRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增表单类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormTypeInfo(FormTypeEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<int> DeleteFormTypeInfo(long formTypeId)
        {
            return await _db.Deleteable<FormTypeEntity>()
                            .Where(formType => formType.FormTypeId == formTypeId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除员工表单类别绑定
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserFormTypeBind(long formTypeId)
        {
            return await _db.Deleteable<UserFormEntity>()
                            .Where(userform => userform.FormGroupTypeId == formTypeId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改表单类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormTypeInfo(FormTypeEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(formType => new
                            {
                                formType.FormTypeId,
                                formType.CreatedBy,
                                formType.CreatedDate,
                            }).Where(formType => formType.FormTypeId == entity.FormTypeId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询表单类别实体
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<FormTypeDto> GetFormTypeEntity(long formTypeId)
        {
            var entity = await _db.Queryable<FormTypeEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(formType => formType.FormTypeId == formTypeId)
                                          .FirstAsync();
            return entity.Adapt<FormTypeDto>();
        }

        /// <summary>
        /// 查询表单类别分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<FormTypeDto>> GetFormTypePage(GetFormTypePage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<FormTypeEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<FormGroupEntity>((formtype, formgroup) => formtype.FormGroupId == formgroup.FormGroupId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getPage.FormGroupId))
            {
                query = query.Where(formtype => formtype.FormGroupId == long.Parse(getPage.FormGroupId));
            }
            // 表单类别名称
            if (!string.IsNullOrEmpty(getPage.FormTypeName))
            {
                query = query.Where(formtype =>
                    formtype.FormTypeNameCn.Contains(getPage.FormTypeName) ||
                    formtype.FormTypeNameEn.Contains(getPage.FormTypeName));
            }

            // 排序
            query = query.OrderBy((formtype, formgroup) => formtype.SortOrder);

            var page = await query.Select((formtype, formgroup) => new FormTypeDto
            {
                FormTypeId = formtype.FormTypeId,
                FormGroupId = formtype.FormGroupId,
                FormGroupName = _lang.Locale == "zh-CN"
                                ? formgroup.FormGroupNameCn
                                : formgroup.FormGroupNameEn,
                FormTypeNameCn = formtype.FormTypeNameCn,
                FormTypeNameEn = formtype.FormTypeNameEn,
                Prefix = formtype.Prefix,
                ApprovalPath = formtype.ApprovalPath,
                ViewPath = formtype.ViewPath,
                DescriptionCn = formtype.DescriptionCn,
                DescriptionEn = formtype.DescriptionEn,
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<FormTypeDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormGroupDropDto>> GetFormGroupDrop()
        {
            return await _db.Queryable<FormGroupEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(formgroup => formgroup.SortOrder)
                            .Select(formgroup => new FormGroupDropDto
                            {
                                FormGroupId = formgroup.FormGroupId,
                                FormGroupName = _lang.Locale == "zh-CN"
                                                ? formgroup.FormGroupNameCn
                                                : formgroup.FormGroupNameEn
                            }).ToListAsync();
        }
    }
}
