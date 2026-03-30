using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowBranchStepRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowBranchStepRepository(SqlSugarScope db, Language lang)
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
        /// 新增流程分支步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowBranchStep(WorkflowBranchStepEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除流程分支步骤
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowBranchStep(long branchId, long stepId)
        {
            return await _db.Deleteable<WorkflowBranchStepEntity>()
                            .Where(branchstep => branchstep.BranchId == branchId && branchstep.StepId == stepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改流程分支步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowBranchStep(WorkflowBranchStepEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(branchstep => new
                            {
                                branchstep.BranchId, 
                                branchstep.StepId,
                                branchstep.CreatedBy,
                                branchstep.CreatedDate,
                            }).Where(branchstep => branchstep.BranchId == entity.BranchId && branchstep.StepId == entity.StepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询流程分支步骤实体
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<WorkflowBranchStepDto> GetWorkflowBranchStepEntity(long branchId, long stepId)
        {
            var entity = await _db.Queryable<WorkflowBranchStepEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(branchstep => branchstep.BranchId == branchId && branchstep.StepId == stepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowBranchStepDto>();
        }

        /// <summary>
        /// 查询流程分支步骤分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowBranchStepDto>> GetWorkflowBranchStepPage(GetWorkflowBranchStepPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var page = await _db.Queryable<WorkflowBranchStepEntity>()
                                .With(SqlWith.NoLock)
                                .OrderBy(branchstep => branchstep.SortOrder)
                                .ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<WorkflowBranchStepDto>.Ok(page.Adapt<List<WorkflowBranchStepDto>>(), totalCount);
        }
    }
}
