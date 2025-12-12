using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity;

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
        /// <param name="formTypeEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormTypeInfo(FormTypeEntity formTypeEntity)
        {
            return await _db.Insertable(formTypeEntity).ExecuteCommandAsync();
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
            return await _db.Deleteable<UserFormBindEntity>()
                            .Where(userform => userform.FormGroupTypeId == formTypeId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改表单类别
        /// </summary>
        /// <param name="formTypeEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormTypeInfo(FormTypeEntity formTypeEntity)
        {
            return await _db.Updateable(formTypeEntity)
                            .IgnoreColumns(formType => new
                            {
                                formType.FormTypeId,
                                formType.CreatedBy,
                                formType.CreatedDate,
                            }).Where(formType => formType.FormTypeId == formTypeEntity.FormTypeId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询表单类别实体
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<FormTypeDto> GetFormTypeEntity(long formTypeId)
        {
            var formTypeEntity = await _db.Queryable<FormTypeEntity>()
                                          .With(SqlWith.NoLock)
                                          .Where(formType => formType.FormTypeId == formTypeId)
                                          .FirstAsync();
            return formTypeEntity.Adapt<FormTypeDto>();
        }

        /// <summary>
        /// 查询表单类别分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<FormTypeDto>> GetFormTypePage(GetFormTypePage getFormTypePage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<FormTypeEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<FormGroupEntity>((formtype, formgroup) => formtype.FormGroupId == formgroup.FormGroupId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getFormTypePage.FormGroupId))
            {
                query = query.Where(formtype => formtype.FormGroupId == long.Parse(getFormTypePage.FormGroupId));
            }
            // 表单类别名称
            if (!string.IsNullOrEmpty(getFormTypePage.FormTypeName))
            {
                query = query.Where(formtype =>
                    formtype.FormTypeNameCn.Contains(getFormTypePage.FormTypeName) ||
                    formtype.FormTypeNameEn.Contains(getFormTypePage.FormTypeName));
            }

            var formTypePage = await query.OrderBy((formtype, formgroup) => formtype.SortOrder)
                                          .Select((formtype, formgroup) => new FormTypeDto
                                          {
                                              FormTypeId = formtype.FormTypeId,
                                              FormGroupId = formtype.FormGroupId,
                                              FormGroupName = _lang.Locale == "zh-cn"
                                                              ? formgroup.FormGroupNameCn
                                                              : formgroup.FormGroupNameEn,
                                              FormTypeNameCn = formtype.FormTypeNameCn,
                                              FormTypeNameEn = formtype.FormTypeNameEn,
                                              Prefix = formtype.Prefix,
                                              ApprovalPath = formtype.ApprovalPath,
                                              ViewPath = formtype.ViewPath,
                                              DescriptionCn = formtype.DescriptionCn,
                                              DescriptionEn = formtype.DescriptionEn,
                                          }).ToPageListAsync(getFormTypePage.PageIndex, getFormTypePage.PageSize, totalCount);
            return ResultPaged<FormTypeDto>.Ok(formTypePage, totalCount, "");
        }

        /// <summary>
        /// 查询表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormGroupDropDto>> GetFormGroupDropDown()
        {
            return await _db.Queryable<FormGroupEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(formgroup => formgroup.SortOrder)
                            .Select(formgroup => new FormGroupDropDto
                            {
                                FormGroupId = formgroup.FormGroupId,
                                FormGroupName = _lang.Locale == "zh-cn"
                                                ? formgroup.FormGroupNameCn
                                                : formgroup.FormGroupNameEn
                            }).ToListAsync();
        }
    }
}
