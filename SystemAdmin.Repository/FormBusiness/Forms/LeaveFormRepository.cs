using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Dto;
using SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;

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
        /// <param name="leaveFormEntity"></param>
        /// <returns></returns>
        public async Task<int> InitLeaveForm(LeaveFormEntity leaveFormEntity)
        {
            return await _db.Insertable(leaveFormEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 保存请假表单
        /// </summary>
        /// <param name="leaveFormEntity"></param>
        /// <returns></returns>
        public async Task<int> SaveLeaveForm(LeaveFormEntity leaveFormEntity)
        {
            return await _db.Updateable(leaveFormEntity)
                            .IgnoreColumns(leave => new
                            {
                                leave.FormId,
                                leave.FormNo,
                                leave.CreatedBy,
                                leave.CreatedDate,
                            }).Where(leave => leave.FormId == leaveFormEntity.FormId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询请假表单详情
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public async Task<LeaveFormDto> GetLeaveForm(long formId)
        {
            return await _db.Queryable<FormInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<LeaveFormEntity>((forminfo, leaveinfo) => forminfo.FormId == leaveinfo.FormId)
                            .InnerJoin<DictionaryInfoEntity>((forminfo, leaveinfo, dicinfo) => dicinfo.DicType == "FormStatus" && forminfo.FormStatus == dicinfo.DicCode)
                            .Where((forminfo, leaveinfo, dicinfo) => forminfo.FormId == formId)
                            .Select((forminfo, leaveinfo, dicinfo) => new LeaveFormDto()
                            {
                                FormTypeId = forminfo.FormTypeId,
                                Description = forminfo.Description,
                                ImportanceCode = forminfo.ImportanceCode,
                                FormStatus = forminfo.FormStatus,
                                FormStatusName = _lang.Locale == "zh-cn"
                                                 ? dicinfo.DicNameCn
                                                 : dicinfo.DicNameEn,
                                FormId = forminfo.FormId,
                                FormNo = forminfo.FormNo,
                                ApplicantTime = leaveinfo.ApplicantTime,
                                ApplicantUserNo = leaveinfo.ApplicantUserNo,
                                ApplicantUserName = leaveinfo.ApplicantUserName,
                                ApplicantDeptId = leaveinfo.ApplicantDeptId,
                                ApplicantDeptName = leaveinfo.ApplicantDeptName,
                                LeaveTypeCode = leaveinfo.LeaveTypeCode,
                                LeaveReason = leaveinfo.LeaveReason,
                                LeaveStartTime = leaveinfo.LeaveStartTime,
                                LeaveEndTime = leaveinfo.LeaveEndTime,
                                LeaveHours = leaveinfo.LeaveHours,
                                LeaveHandoverUserName = leaveinfo.LeaveHandoverUserName,
                            }).FirstAsync();
        }

        /// <summary>
        /// 查询用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserBasicInfoDto> GetUserInfo(long userId)
        {
            return await _db.Queryable<UserInfoEntity>()
                            .With(SqlWith.NoLock)
                            .InnerJoin<DepartmentInfoEntity>((userinfo, deptinfo) => userinfo.DepartmentId == deptinfo.DepartmentId)
                            .InnerJoin<UserPositionEntity>((userinfo, deptinfo, position) => userinfo.PositionId ==position.PositionId)
                            .Select((userinfo, deptinfo, position) => new UserBasicInfoDto()
                            {
                                UserId = userinfo.UserId,
                                UserNo = userinfo.UserNo,
                                UserName = _lang.Locale == "zh-cn"
                                           ? userinfo.UserNameCn
                                           : userinfo.UserNameEn,
                                DetpId = deptinfo.DepartmentId,
                                DetpName = _lang.Locale == "zh-cn"
                                           ? deptinfo.DepartmentNameCn
                                           : deptinfo.DepartmentNameEn,
                                PositionNo = position.PositionNo,
                                PositionName = _lang.Locale == "zh-cn"
                                           ? position.PositionNameCn
                                           : position.PositionNameEn,
                                PhoneNumber = userinfo.PhoneNumber
                            }).FirstAsync();
        }

        /// <summary>
        /// 请假类别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<LeaveTypeDropDto>> GetLeaveTypeDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .Where(dic => dic.DicType == "LeaveType")
                            .Select(dic => new LeaveTypeDropDto()
                            {
                                LeaveTypeCode = dic.DicCode,
                                LeaveTypeName = _lang.Locale == "zh-cn"
                                                ? dic.DicNameCn
                                                : dic.DicNameEn,
                            }).ToListAsync();
        }
    }
}
