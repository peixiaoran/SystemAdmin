using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormOperate
{
    public class PendingSubAppRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public PendingSubAppRepository(SqlSugarScope db, Language lang)
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
        public async Task<List<FormTypeDropDto>> GetFormTypeDropDown(long formGroupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formgroup => formgroup.FormGroupId == formGroupId)
                            .OrderBy(formgroup => formgroup.SortOrder)
                            .Select(formgroup => new FormTypeDropDto
                            {
                                FormTypeId = formgroup.FormTypeId,
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

        /// <summary>
        /// 查询待送审分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingSubmissionPage(GetPendingSubAppPage getPage, long loginUserId)
        {
            RefAsync<int> totalCount = 0;

            var query = _db.Queryable<PendingApprovalEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormInstanceEntity>((pendapp, forminfo) => pendapp.FormId == forminfo.FormId)
                           .InnerJoin<FormTypeEntity>((pendapp, forminfo, formtype) => forminfo.FormTypeId == formtype.FormTypeId)
                           .InnerJoin<DictionaryInfoEntity>((pendapp, forminfo, formtype, dic) => dic.DicType == "FormStatus" && dic.DicCode == FormStatus.PendingSubmission.ToEnumString() && forminfo.FormStatus == dic.DicCode)
                           .InnerJoin<UserInfoEntity>((pendapp, forminfo, formtype, dic, penduser) => pendapp.ApproveUserId == penduser.UserId)
                           .LeftJoin<UserInfoEntity>((pendapp, forminfo, formtype, dic, penduser, appuser) => forminfo.CreatedBy == appuser.UserId)
                           .LeftJoin<DepartmentInfoEntity>((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => appuser.DepartmentId == appuserdept.DepartmentId)
                           .Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => forminfo.CreatedBy == loginUserId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getPage.FormGroupId) && long.Parse(getPage.FormGroupId) > 0)
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) =>
                    formtype.FormGroupId == long.Parse(getPage.FormGroupId));
            }
            // 表单类别Id
            if (!string.IsNullOrEmpty(getPage.FormTypeId) && long.Parse(getPage.FormTypeId) > 0)
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) =>
                    formtype.FormTypeId == long.Parse(getPage.FormTypeId));
            }
            // 表单状态
            if (!string.IsNullOrEmpty(getPage.FormStatus))
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => forminfo.FormStatus == getPage.FormStatus);
            }

            // 排序
            query = query.OrderBy((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => new { forminfo.CreatedDate });

            var page = await query.Select((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => new PendingSubAppDto
                                  {
                                      FormId = forminfo.FormId,
                                      FormNo = forminfo.FormNo,
                                      FormTypeId = formtype.FormTypeId,
                                      FormTypeName = _lang.Locale == "zh-CN"
                                                     ? formtype.FormTypeNameCn
                                                     : formtype.FormTypeNameEn,
                                      FormStatus = forminfo.FormStatus,
                                      FormStatusName = _lang.Locale == "zh-CN"
                                                     ? dic.DicNameCn
                                                     : dic.DicNameEn,
                                      ApplyUserName = _lang.Locale == "zh-CN"
                                                     ? appuser.UserNameCn
                                                     : appuser.UserNameEn,
                                      ApplyUserDeptName = _lang.Locale == "zh-CN"
                                                     ? appuserdept.DepartmentNameCn
                                                     : appuserdept.DepartmentNameEn,
                                      ApprovalPath = formtype.ApprovalPath,
                                      ViewPath = formtype.ViewPath,
                                      isDelete = (appuser.CreatedBy == loginUserId
                                                  && forminfo.FormStatus != FormStatus.Voided.ToEnumString()) ? 1 : 0
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<PendingSubAppDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询待签核分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<PendingSubAppDto>> GetPendingApprovalPage(GetPendingSubAppPage getPage, long loginUserId)
        {
            RefAsync<int> totalCount = 0;

            var query = _db.Queryable<PendingApprovalEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormInstanceEntity>((pendapp, forminfo) => pendapp.FormId == forminfo.FormId)
                           .InnerJoin<FormTypeEntity>((pendapp, forminfo, formtype) => forminfo.FormTypeId == formtype.FormTypeId)
                           .InnerJoin<DictionaryInfoEntity>((pendapp, forminfo, formtype, dic) => dic.DicType == "FormStatus" && forminfo.FormStatus == dic.DicCode)
                           .InnerJoin<UserInfoEntity>((pendapp, forminfo, formtype, dic, penduser) => pendapp.ApproveUserId == penduser.UserId)
                           .LeftJoin<UserInfoEntity>((pendapp, forminfo, formtype, dic, penduser, appuser) => forminfo.CreatedBy == appuser.UserId)
                           .LeftJoin<DepartmentInfoEntity>((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => appuser.DepartmentId == appuserdept.DepartmentId)
                           .Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => pendapp.ApproveUserId == loginUserId && forminfo.CreatedBy != loginUserId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getPage.FormGroupId) && long.Parse(getPage.FormGroupId) > 0)
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) =>
                    formtype.FormGroupId == long.Parse(getPage.FormGroupId));
            }
            // 表单类别Id
            if (!string.IsNullOrEmpty(getPage.FormTypeId) && long.Parse(getPage.FormTypeId) > 0)
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) =>
                    formtype.FormTypeId == long.Parse(getPage.FormTypeId));
            }
            // 表单状态
            if (!string.IsNullOrEmpty(getPage.FormStatus))
            {
                query = query.Where((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => forminfo.FormStatus == getPage.FormStatus);
            }

            // 排序
            query = query.OrderBy((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => new { forminfo.CreatedDate });

            var page = await query.Select((pendapp, forminfo, formtype, dic, penduser, appuser, appuserdept) => new PendingSubAppDto
                                  {
                                      FormId = forminfo.FormId,
                                      FormNo = forminfo.FormNo,
                                      FormTypeId = formtype.FormTypeId,
                                      FormTypeName = _lang.Locale == "zh-CN"
                                                     ? formtype.FormTypeNameCn
                                                     : formtype.FormTypeNameEn,
                                      FormStatus = forminfo.FormStatus,
                                      FormStatusName = _lang.Locale == "zh-CN"
                                                     ? dic.DicNameCn
                                                     : dic.DicNameEn,
                                      ApplyUserName = _lang.Locale == "zh-CN"
                                                     ? appuser.UserNameCn
                                                     : appuser.UserNameEn,
                                      ApplyUserDeptName = _lang.Locale == "zh-CN"
                                                     ? appuserdept.DepartmentNameCn
                                                     : appuserdept.DepartmentNameEn,
                                      ApprovalPath = formtype.ApprovalPath,
                                      ViewPath = formtype.ViewPath,
                                      isDelete = 0
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<PendingSubAppDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 是否可以作废表单
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public Task<bool> IsVoidedForm(long formId)
        {
            return _db.Queryable<FormInstanceEntity>()
                      .Where(forminfo => forminfo.FormStatus == FormStatus.PendingSubmission.ToEnumString())
                      .AnyAsync();
        }

        /// <summary>
        /// 作废表单
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public Task<int> VoidedForm(long formId,long loginUserId)
        {
            return _db.Updateable<FormInstanceEntity>()
                      .SetColumns(forminfo => new FormInstanceEntity
                      {
                          FormStatus = FormStatus.Voided.ToEnumString(),
                          ModifiedBy = loginUserId,
                          ModifiedDate = DateTime.Now,
                      }).Where(forminfo => forminfo.FormId == formId)
                      .ExecuteCommandAsync();
        }
    }
}
