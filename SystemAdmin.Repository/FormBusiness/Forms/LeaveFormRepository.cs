using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.PublicForm.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.Forms
{
    public class LeaveFormRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public LeaveFormRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveTypeDropDto>> GetLeaveTypeDrop()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .Where(dic => dic.DicType == "LeaveType")
                            .Select(dic => new LeaveTypeDropDto()
                            {
                                LeaveTypeCode = dic.DicCode,
                                LeaveTypeName = _lang.Locale == "zh-CN"
                                                ? dic.DicNameCn
                                                : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 初始化请假表单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InitLeaveForm(LeaveFormEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 保存请假表单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> SaveLeaveForm(LeaveFormEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(leave => new
                            {
                                leave.FormId,
                                leave.CreatedBy,
                                leave.CreatedDate,
                            }).Where(leave => leave.FormId == entity.FormId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询请假单明细
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<LeaveFormDto> GetLeaveForm(long formId)
        {
            return await _db.Queryable<FormInstanceEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<LeaveFormEntity>((form, leave) => form.FormId == leave.FormId)
                            .InnerJoin<UserInfoEntity>((form, leave, user) => form.ApplicantUserId == user.UserId)
                            .InnerJoin<DepartmentInfoEntity>((form, leave, user, dept) => user.DepartmentId == dept.DepartmentId)
                            .InnerJoin<DictionaryInfoEntity>((form, leave, user, dept, dic) => dic.DicType == "FormStatus" && form.FormStatus == dic.DicCode)
                            .Where((form, leave, user, dept, dic) => form.FormId == formId)
                            .Select((form, leave, user, dept, dic) => new LeaveFormDto()
                            {
                                FormTypeId = form.FormTypeId,
                                FormStatus = form.FormStatus,
                                FormStatusName = _lang.Locale == "zh-CN"
                                                 ? dic.DicNameCn
                                                 : dic.DicNameEn,
                                FormId = form.FormId,
                                FormNo = form.FormNo,
                                ApplicantUserNo = user.UserNo,
                                ApplicantUserName = _lang.Locale == "zh-CN"
                                                 ? user.UserNameCn
                                                 : user.UserNameEn,
                                ApplicantDeptName = _lang.Locale == "zh-CN"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                LeaveTypeCode = leave.LeaveTypeCode,
                                LeaveReason = leave.LeaveReason,
                                LeaveStartTime = leave.LeaveStartTime,
                                LeaveEndTime = leave.LeaveEndTime,
                                LeaveHours = leave.LeaveDays,
                                AgentUserNo = leave.AgentUserNo,
                            }).FirstAsync();
        }

        /// <summary>
        /// 查询附件列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormAttachmentDto>> GetAttachmentList(long formId)
        {
            var list = await _db.Queryable<FormAttachmentEntity>()
                                .With(SqlWith.NoLock)
                                .Where(formfile => formfile.FormId == formId)
                                .ToListAsync();
            return list.Adapt<List<FormAttachmentDto>>();
        }

        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAttachment(FormAttachmentEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public async Task<int> DeleteAttachment(long attachmentId)
        {
            return await _db.Deleteable<FormAttachmentEntity>()
                            .Where(attach => attach.AttachmentId == attachmentId)
                            .ExecuteCommandAsync();
        }


        /// <summary>
        /// 查询审批记录列表
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<List<FormReviewRecordDto>> GetReviewRecordList(long formId)
        {
            var list = await _db.Queryable<FormReviewRecordEntity>()
                                .InnerJoin<WorkflowStepEntity>((review, step) => review.StepId == step.StepId)
                                .InnerJoin<DictionaryInfoEntity>((review, step, reviewresult) => reviewresult.DicType == "ReviewResult" && review.ReviewResult == reviewresult.DicCode)
                                .With(SqlWith.NoLock)
                                .Where(formfile => formfile.FormId == formId)
                                .ToListAsync();
            return list.Adapt<List<FormReviewRecordDto>>();
        }
    }
}
