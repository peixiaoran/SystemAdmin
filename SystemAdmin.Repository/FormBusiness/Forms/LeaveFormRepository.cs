using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
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
                                leave.FormNo,
                                leave.CreatedBy,
                                leave.CreatedDate,
                            }).Where(leave => leave.FormId == entity.FormId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增附件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFile(LeaveFileEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询请假单明细
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<LeaveFormDto> GetLeaveForm(long formId)
        {
            return await _db.Queryable<FormInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<LeaveFormEntity>((form, leave) => form.FormId == leave.FormId)
                            .InnerJoin<UserInfoEntity>((form, leave, user) => leave.ApplicantUserId == user.UserId)
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
                                LeaveHours = leave.LeaveHours,
                                AgentUserNo = leave.AgentUserNo,
                            }).FirstAsync();
        }

        /// <summary>
        /// 查询员工基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserBasicInfoDto> GetUserInfo(long userId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentInfoEntity>((userinfo, deptinfo) => userinfo.DepartmentId == deptinfo.DepartmentId)
                            .InnerJoin<UserPositionEntity>((userinfo, deptinfo, position) => userinfo.PositionId == position.PositionId)
                            .Select((userinfo, deptinfo, position) => new UserBasicInfoDto()
                            {
                                UserId = userinfo.UserId,
                                UserNo = userinfo.UserNo,
                                UserName = _lang.Locale == "zh-CN"
                                           ? userinfo.UserNameCn
                                           : userinfo.UserNameEn,
                                DetpId = deptinfo.DepartmentId,
                                DetpName = _lang.Locale == "zh-CN"
                                           ? deptinfo.DepartmentNameCn
                                           : deptinfo.DepartmentNameEn,
                                PositionNo = position.PositionNo,
                                PositionName = _lang.Locale == "zh-CN"
                                           ? position.PositionNameCn
                                           : position.PositionNameEn,
                                PhoneNumber = userinfo.PhoneNumber
                            }).FirstAsync();
        }

        /// <summary>
        /// 请假类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveTypeDropDto>> GetLeaveTypeDropDown()
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
    }
}
