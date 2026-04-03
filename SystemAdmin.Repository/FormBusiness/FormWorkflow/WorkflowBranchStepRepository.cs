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
        /// 分支步骤是否重复配置
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public async Task<bool> BranchStepIsRepeat(long branchId, long stepId)
        {
            return await _db.Queryable<WorkflowBranchStepEntity>()
                            .Where(branch => branch.BranchId == branchId && branch.StepId == stepId)
                            .AnyAsync();
        }

        /// <summary>
        /// 新增分支步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowBranchStep(WorkflowBranchStepEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除分支步骤
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
        /// 修改分支步骤
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
        /// 查询分支步骤实体
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
        /// 查询分支步骤分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowBranchStepDto>> GetWorkflowBranchStepPage(GetWorkflowBranchStepPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<WorkflowBranchStepEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<WorkflowStepEntity>((branchstep, step) => branchstep.StepId == step.StepId)
                           .InnerJoin<WorkflowStepEntity>((branchstep, step, nextstep) => branchstep.NextStepId == nextstep.StepId);

            if (!string.IsNullOrEmpty(getPage.BranchId) && long.Parse(getPage.BranchId) > -1)
            {
                query.Where((branchstep, step, nextstep) => branchstep.BranchId == long.Parse(getPage.BranchId));
            }

            var page = await query.OrderBy((branchstep, step, nextstep) => branchstep.SortOrder)
                                  .Select((branchstep, step, nextstep) => new WorkflowBranchStepDto { 
                                      BranchId = branchstep.BranchId,
                                      StepId = step.StepId,
                                      StepName = _lang.Locale == "zh-CN"
                                                 ? step.StepNameCn
                                                 : step.StepNameEn,
                                      NextStepId = nextstep.StepId,
                                      NextStepName = _lang.Locale == "zh-CN"
                                                 ? nextstep.StepNameCn
                                                 : nextstep.StepNameEn,
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<WorkflowBranchStepDto>.Ok(page, totalCount);
        }
    }
}
