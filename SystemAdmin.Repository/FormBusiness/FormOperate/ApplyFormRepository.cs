using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormOperate
{
    public class ApplyFormRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public ApplyFormRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询申请表单分页
        /// </summary>
        /// <param name="getApplyFormPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ApplyFormInfoDto>> GetApplyFormPage(getApplyFormPage getApplyFormPage, long userId)
        {
            RefAsync<int> totalCount = 0;
            var applyFormPage = await _db.Queryable<UserFormBindEntity>()
                                         .With(SqlWith.NoLock)
                                         .InnerJoin<FormTypeEntity>((userform, formtype) => userform.FormGroupTypeId == formtype.FormTypeId)
                                         .Where((userform, formtype) => userform.UserId == userId && formtype.FormGroupId == long.Parse(getApplyFormPage.FormGroupId))
                                         .OrderBy((userform, formtype) => formtype.SortOrder)
                                         .Select((userform, formtype) => new ApplyFormInfoDto()
                                         {
                                             FormTypeId = formtype.FormTypeId,
                                             FormTypeName = _lang.Locale == "zh-CN"
                                                            ? formtype.FormTypeNameCn
                                                            : formtype.FormTypeNameEn,
                                             ApprovalPath = formtype.ApprovalPath,
                                             Description = _lang.Locale == "zh-CN"
                                                            ? formtype.DescriptionCn
                                                            : formtype.DescriptionEn,
                                         }).ToPageListAsync(getApplyFormPage.PageIndex, getApplyFormPage.PageSize, totalCount);
            return ResultPaged<ApplyFormInfoDto>.Ok(applyFormPage, totalCount);
        }

        /// <summary>
        /// 表单组别下拉框
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
                                FormGroupName = _lang.Locale == "zh-CN"
                                                ? formgroup.FormGroupNameCn
                                                : formgroup.FormGroupNameEn,
                            }).ToListAsync();
        }
    }
}
