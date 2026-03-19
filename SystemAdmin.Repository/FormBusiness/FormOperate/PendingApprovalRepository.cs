using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormOperate
{
    public class PendingApprovalRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public PendingApprovalRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 表单组别下拉
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

        /// <summary>
        /// 表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDropDown()
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(formgroup => formgroup.SortOrder)
                            .Select(formgroup => new FormTypeDropDto
                            {
                                FormTypeId = formgroup.FormGroupId,
                                FormTypeName = _lang.Locale == "zh-CN"
                                               ? formgroup.FormTypeNameCn
                                               : formgroup.FormTypeNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 表单状态下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormStatusDropDto>> GetFormStatusDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.DicType == "FormStatus")
                            .OrderBy(dic => dic.SortOrder)
                            .Select(dic => new FormStatusDropDto
                            {
                                FormStatusCode = dic.DicCode,
                                FormStatusName = _lang.Locale == "zh-CN"
                                               ? dic.DicNameCn
                                               : dic.DicNameEn,
                            }).ToListAsync();
        }

        //public async Task<ResultPaged<PendingApprovalDto>> GetPendingApprovalPage(GetPendingApprovalPage getPage)
        //{
        //    RefAsync<int> totalCount = 0;
        //    var page = await _db.Queryable<PendingApprovalEntity>()
        //                         .With(SqlWith.NoLock)
        //                         .LeftJoin<UserInfoEntity>((pendapp, appuser) => pendapp.ApproveUserId == appuser.UserId)
        //                         .LeftJoin<FormInfoEntity>((pendapp, appuser, forminfo) => pendapp.FormId == forminfo.FormId)
        //                         .LeftJoin<FormTypeEntity>((pendapp, appuser, forminfo, formtype) => forminfo.FormTypeId == formtype.FormTypeId)
        //                         .LeftJoin<DictionaryInfoEntity>((pendapp, appuser, forminfo, formtype, dic) => dic.DicType == "FormStatus")
        //                         .Select<PendingApprovalDto>();
        //}
    }
}
