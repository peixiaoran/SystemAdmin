using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class FormStepRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormStepRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增审批步骤信息
        /// </summary>
        /// <param name="formStepEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormStep(FormStepEntity formStepEntity)
        {
            return await _db.Insertable(formStepEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改审批步骤信息
        /// </summary>
        /// <param name="formStepEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormStep(FormStepEntity formStepEntity)
        {
            return await _db.Updateable(formStepEntity)
                            .IgnoreColumns(step => new
                            {
                                step.StepId,
                                step.FormTypeId,
                                step.CreatedBy,
                                step.CreatedDate,
                            }).Where(step => step.StepId == formStepEntity.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询审批步骤分页
        /// </summary>
        /// <param name="getFormStepPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormStepDto>> GetFormStepPage(GetFormStepPage getFormStepPage)
        {
            RefAsync<int> totalCount = 0;
            var formStepPage = await _db.Queryable<FormStepEntity>()
                                        .With(SqlWith.NoLock)
                                        .InnerJoin<DictionaryInfoEntity>((stepinfo, assigndic) => assigndic.DicType == "ApproverAssignment" && stepinfo.Assignment == assigndic.DicCode)
                                        .Where((stepinfo, assigndic) => stepinfo.FormTypeId == long.Parse(getFormStepPage.FormTypeId))
                                        .OrderBy((stepinfo, assigndic) => stepinfo.SortOrder)
                                        .Select((stepinfo, assigndic) => new FormStepDto()
                                        {
                                            StepId = stepinfo.StepId,
                                            StepName = _lang.Locale == "zh-cn"
                                                       ? stepinfo.StepNameCn
                                                       : stepinfo.StepNameEn,
                                            Assignment = stepinfo.Assignment,
                                            AssignmentName = _lang.Locale == "zh-cn"
                                                       ? assigndic.DicNameCn
                                                       : assigndic.DicNameEn,
                                            SortOrder = stepinfo.SortOrder,
                                            Description = stepinfo.Description,
                                        }).ToPageListAsync(getFormStepPage.PageIndex, getFormStepPage.PageSize, totalCount);
            return ResultPaged<FormStepDto>.Ok(formStepPage.Adapt<List<FormStepDto>>(), totalCount);
        }

        /// <summary>
        /// 查询审批步骤实体
        /// </summary>
        /// <param name="getFormStepEntity"></param>
        /// <returns></returns>
        public async Task<FormStepDto> GetFormStepEntity(GetFormStepEntity getFormStepEntity)
        {
            var formStepEntity = await _db.Queryable<FormStepEntity>()
                                          .Where(step => step.FormTypeId == long.Parse(getFormStepEntity.StepId))
                                          .OrderBy(step => step.SortOrder)
                                          .ToListAsync();
            return formStepEntity.Adapt<FormStepDto>();
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
                                FormGroupName = _lang.Locale == "zh-cn"
                                                ? formgroup.FormGroupNameCn
                                                : formgroup.FormGroupNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 表单类型下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDropDown(long groupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormGroupId == groupId)
                            .OrderBy(formtype => formtype.SortOrder)
                            .Select(formtype => new FormTypeDropDto
                            {
                                FormTypeId = formtype.FormTypeId,
                                FormTypeName = _lang.Locale == "zh-cn"
                                               ? formtype.DescriptionCn
                                               : formtype.DescriptionEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 审批人选取方式下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssignmentDropDto>> GetAssignmentDropDown()
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .Where(dic => dic.DicType == "ApproverAssignment")
                            .Select(dic => new AssignmentDropDto
                            {
                                ApproverAssignmentCode = dic.DicCode,
                                ApproverAssignmentName = _lang.Locale == "zh-cn"
                                                         ? dic.DicNameCn
                                                         : dic.DicNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 部门树下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentDropDto>> GetDepartmentDropDown()
        {
            return await _db.Queryable<DepartmentInfoEntity>()
                            .With(SqlWith.NoLock)
                            .LeftJoin<DepartmentLevelEntity>((dept, deptlevel) => dept.DepartmentLevelId == deptlevel.DepartmentLevelId)
                            .OrderBy(dept => dept.SortOrder)
                            .Select((dept, deptlevel) => new DepartmentDropDto
                            {
                                DepartmentId = dept.DepartmentId,
                                DepartmentName = _lang.Locale == "zh-cn"
                                                 ? dept.DepartmentNameCn
                                                 : dept.DepartmentNameEn,
                                ParentId = dept.ParentId,
                                Disabled = dept.IsEnabled == 0
                            }).ToTreeAsync(menu => menu.DepartmentChildList, menu => menu.ParentId, 0);
        }

        /// <summary>
        /// 部门级别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentLevelDropDto>> GetDepartmentLevelDropDown()
        {
            return await _db.Queryable<DepartmentLevelEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(deptlevel => deptlevel.SortOrder)
                            .Select(deptlevel => new DepartmentLevelDropDto
                            {
                                DepartmentLevelId = deptlevel.DepartmentLevelId,
                                DepartmentLevelName = _lang.Locale == "zh-cn"
                                                      ? deptlevel.DepartmentLevelNameCn
                                                      : deptlevel.DepartmentLevelNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职级下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPositionDropDto>> GetUserPositionDropDown()
        {
            return await _db.Queryable<UserPositionEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(userpos => userpos.CreatedDate)
                            .Select((userpos) => new UserPositionDropDto
                            {
                                PositionId = userpos.PositionId,
                                PositionName = _lang.Locale == "zh-cn"
                                               ? userpos.PositionNameCn
                                               : userpos.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 职业下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserLaborDropDto>> GetLaborDropDown()
        {
            return await _db.Queryable<UserLaborEntity>()
                            .With(SqlWith.NoLock)
                            .Select(userlabor => new UserLaborDropDto
                            {
                                LaborId = userlabor.LaborId,
                                LaborName = _lang.Locale == "zh-cn"
                                            ? userlabor.LaborNameCn
                                            : userlabor.LaborNameEn
                            }).ToListAsync();
        }
    }
}
