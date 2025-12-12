using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormAudit.Dto;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormAudit.Queries;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormAudit
{
    public class FormCountingRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormCountingRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 查询表单计数信息分页
        /// </summary>
        /// <returns></returns>
        public async Task<ResultPaged<FormCountingDto>> GetFormCountingPage(GetFormCountingPage getFormCountingPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<FormTypeEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<FormCountingEntity>((formtype, formcounting) => formcounting.FormTypeId == formtype.FormTypeId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getFormCountingPage.FormTypeId))
            {
                query = query.Where(formtype => formtype.FormTypeId == long.Parse(getFormCountingPage.FormTypeId));
            }

            var formCountingPage = await query.OrderBy((formtype, formcounting) => formcounting.YM)
            .Select((formtype, formcounting) => new FormCountingDto
            {
                FormTypeId = formtype.FormTypeId,
                FormTypeName = _lang.Locale == "zh-cn"
                               ? formtype.FormTypeNameCn
                               : formtype.FormTypeNameEn,
                YM = formcounting.YM,
                Total = formcounting.Total,
                Draft = formcounting.Draft,
                Submitted = formcounting.Submitted,
                Approved = formcounting.Approved,
                Rejected = formcounting.Rejected,
                Canceled = formcounting.Canceled
            }).ToPageListAsync(getFormCountingPage.PageIndex, getFormCountingPage.PageSize, totalCount);
            return ResultPaged<FormCountingDto>.Ok(formCountingPage, totalCount, "");
        }
    }
}
