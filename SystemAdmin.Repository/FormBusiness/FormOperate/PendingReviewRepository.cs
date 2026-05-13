using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Dto;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Queries;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormOperate
{
    public class PendingReviewRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public PendingReviewRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 表单组别下拉
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
                                                : formgroup.FormGroupNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDrop(long formGroupId)
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
        public async Task<List<FormStatusDropDto>> GetFormStatusDrop()
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
        public async Task<ResultPaged<PendingReviewDto>> GetPendingSubmissionPage(GetPendingSubAppPage getPage, long loginUserId)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<PendingReviewEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormInstanceEntity>((pending, instance) => pending.FormId == instance.FormId)
                           .InnerJoin<DictionaryInfoEntity>((pending, instance, dic) => dic.DicType == "FormStatus" && dic.DicCode == instance.FormStatus && instance.FormStatus == FormStatus.PendingSubmit.ToEnumString())
                           .InnerJoin<FormTypeEntity>((pending, instance, dic, formtype) => instance.FormTypeId == formtype.FormTypeId)
                           .LeftJoin<UserInfoEntity>((pending, instance, dic, formtype, applyuser) => instance.ApplicantUserId == applyuser.UserId)
                           .LeftJoin<DepartmentInfoEntity>((pending, instance, dic, formtype, applyuser, applyuserdept) => applyuser.DepartmentId == applyuserdept.DepartmentId)
                           .LeftJoin<UserAgentEntity>((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => applyuser.UserId == useragent.SubstituteUserId && useragent.StartTime <= DateTime.Now && useragent.EndTime >= DateTime.Now)
                           .Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => instance.ApplicantUserId == loginUserId || useragent.AgentUserId == loginUserId);


            // 表单组别Id
            if (!string.IsNullOrEmpty(getPage.FormGroupId) && long.Parse(getPage.FormGroupId) > 0)
            {
                query = query.Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) =>
                    formtype.FormGroupId == long.Parse(getPage.FormGroupId));
            }
            // 表单类别Id
            if (!string.IsNullOrEmpty(getPage.FormTypeId) && long.Parse(getPage.FormTypeId) > 0)
            {
                query = query.Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) =>
                    formtype.FormTypeId == long.Parse(getPage.FormTypeId));
            }

            // 排序
            query = query.OrderBy((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => new { instance.CreatedDate });

            var page = await query.Select((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => new PendingReviewDto
            {
                FormId = instance.FormId,
                FormNo = instance.FormNo,
                FormTypeId = formtype.FormTypeId,
                FormTypeName = _lang.Locale == "zh-CN"
                               ? formtype.FormTypeNameCn
                               : formtype.FormTypeNameEn,
                FormStatus = instance.FormStatus,
                FormStatusName = _lang.Locale == "zh-CN"
                               ? dic.DicNameCn
                               : dic.DicNameEn,
                ApplyUserName = _lang.Locale == "zh-CN"
                               ? applyuser.UserNameCn
                               : applyuser.UserNameEn,
                ApplyUserDeptName = _lang.Locale == "zh-CN"
                               ? applyuserdept.DepartmentNameCn
                               : applyuserdept.DepartmentNameEn,
                ReviewPath = formtype.ReviewPath,
                ViewPath = formtype.ViewPath,
                isVoided = (instance.ApplicantUserId == loginUserId && instance.FormStatus != FormStatus.Voided.ToEnumString()) ? 1 : 0
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<PendingReviewDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询待审批分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<PendingReviewDto>> GetPendingReviewPage(GetPendingSubAppPage getPage, long loginUserId)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<PendingReviewEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormInstanceEntity>((pending, instance) => pending.FormId == instance.FormId)
                           .InnerJoin<DictionaryInfoEntity>((pending, instance, dic) => dic.DicType == "FormStatus" && instance.FormStatus == dic.DicCode && instance.FormStatus == FormStatus.UnderReview.ToEnumString())
                           .InnerJoin<FormTypeEntity>((pending, instance, dic, formtype) => instance.FormTypeId == formtype.FormTypeId)
                           .InnerJoin<UserInfoEntity>((pending, instance, dic, formtype, applyuser) => instance.ApplicantUserId == applyuser.UserId)
                           .InnerJoin<DepartmentInfoEntity>((pending, instance, dic, formtype, applyuser, applyuserdept) => applyuser.DepartmentId == applyuserdept.DepartmentId)
                           .LeftJoin<UserAgentEntity>((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => pending.ReviewUserId == useragent.SubstituteUserId && useragent.StartTime <= DateTime.Now && useragent.EndTime >= DateTime.Now)
                           .Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => (pending.ReviewUserId == loginUserId || useragent.AgentUserId == loginUserId) && instance.ApplicantUserId != loginUserId);

            // 表单组别Id
            if (!string.IsNullOrEmpty(getPage.FormGroupId) && long.Parse(getPage.FormGroupId) > 0)
            {
                query = query.Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) =>
                    formtype.FormGroupId == long.Parse(getPage.FormGroupId));
            }
            // 表单类别Id
            if (!string.IsNullOrEmpty(getPage.FormTypeId) && long.Parse(getPage.FormTypeId) > 0)
            {
                query = query.Where((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) =>
                    formtype.FormTypeId == long.Parse(getPage.FormTypeId));
            }

            // 排序
            query = query.OrderBy((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => new { instance.CreatedDate });

            var page = await query.Select((pending, instance, dic, formtype, applyuser, applyuserdept, useragent) => new PendingReviewDto
            {
                FormId = instance.FormId,
                FormNo = instance.FormNo,
                FormTypeId = formtype.FormTypeId,
                FormTypeName = _lang.Locale == "zh-CN"
                               ? formtype.FormTypeNameCn
                               : formtype.FormTypeNameEn,
                FormStatus = instance.FormStatus,
                FormStatusName = _lang.Locale == "zh-CN"
                               ? dic.DicNameCn
                               : dic.DicNameEn,
                ApplyUserName = _lang.Locale == "zh-CN"
                               ? applyuser.UserNameCn
                               : applyuser.UserNameEn,
                ApplyUserDeptName = _lang.Locale == "zh-CN"
                               ? applyuserdept.DepartmentNameCn
                               : applyuserdept.DepartmentNameEn,
                ReviewPath = formtype.ReviewPath,
                ViewPath = formtype.ViewPath,
                isVoided = 0
            }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<PendingReviewDto>.Ok(page, totalCount, "");
        }

        /// <summary>
        /// 查询表单待审批员工
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormPendingReviewDto>> GetFormPendingReviewUser(long formId)
        {
            return await _db.Queryable<PendingReviewEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<WorkflowStepEntity>((pending, step) => pending.StepId == step.StepId)
                            .InnerJoin<DictionaryInfoEntity>((pending, step, dic) => dic.DicType == "AppointmentType" && pending.AppointmentType == dic.DicCode)
                            .InnerJoin<UserInfoEntity>((pending, step, dic, user) => pending.ReviewUserId == user.UserId)
                            .LeftJoin<UserAgentEntity>((pending, step, dic, user, useragent) => user.UserId == useragent.SubstituteUserId && useragent.StartTime <= DateTime.Now && useragent.EndTime >= DateTime.Now)
                            .LeftJoin<UserInfoEntity>((pending, step, dic, user, useragent, agentuser) => useragent.AgentUserId == agentuser.UserId)
                            .Where((pending, step, dic, user, useragent, agentuser) => pending.FormId == formId)
                            .Select((pending, step, dic, user, useragent, agentuser) => new FormPendingReviewDto
                            {
                                StepName = _lang.Locale == "zh-CN"
                                           ? step.StepNameCn
                                           : step.StepNameEn,
                                AppointmentType = pending.AppointmentType,
                                AppointmentTypeName = _lang.Locale == "zh-CN"
                                           ? dic.DicNameCn
                                           : dic.DicNameEn,
                                ReviewUserName = _lang.Locale == "zh-CN"
                                           ? user.UserNameCn
                                           : user.UserNameEn,
                                AgentUserName =_lang.Locale == "zh-CN"
                                           ? agentuser.UserNameCn
                                           : agentuser.UserNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 作废表单
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public Task<int> VoidedForm(long formId, long loginUserId)
        {
            return _db.Updateable<FormInstanceEntity>()
                      .SetColumns(instance => new FormInstanceEntity
                      {
                          FormStatus = FormStatus.Voided.ToEnumString(),
                          ModifiedBy = loginUserId,
                          ModifiedDate = DateTime.Now,
                      }).Where(instance => instance.FormId == formId)
                      .ExecuteCommandAsync();
        }
    }
}
