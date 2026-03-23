using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowConditionRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowConditionRepository(SqlSugarScope db, Language lang)
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
        /// 新增流程条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowCondition(WorkflowConditionEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询流程条件是否绑定流程分支
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<bool> GetWorkflowStepBranchByCon(long conditionId)
        {
            return await _db.Queryable<WorkflowStepBranchEntity>()
                            .Where(branch => branch.ConditionId == conditionId)
                            .AnyAsync();
        }

        /// <summary>
        /// 删除流程条件
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowCondition(long conditionId)
        {
            return await _db.Deleteable<WorkflowConditionEntity>()
                            .Where(branch => branch.ConditionId == conditionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询流程条件实体
        /// </summary>
        /// <param name="conditionId"></param>
        /// <returns></returns>
        public async Task<WorkflowConditionEntity> GetWorkflowConditionEntity(long conditionId)
        {
            return await _db.Queryable<WorkflowConditionEntity>()
                            .Where(branch => branch.ConditionId == conditionId)
                            .FirstAsync();
        }

        /// <summary>
        /// 修改流程条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowCondition(WorkflowConditionEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(condition => new
                            {
                                condition.ConditionId,
                                condition.FormTypeId,
                                condition.CreatedBy,
                                condition.CreatedDate,
                            }).Where(branch => branch.ConditionId == entity.ConditionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询流程条件分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowConditionDto>> GetWorkflowConditionPage(GetWorkflowConditionPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var page = await _db.Queryable<WorkflowConditionEntity>()
                                .With(SqlWith.NoLock)
                                .OrderByDescending(condition => condition.CreatedDate)
                                .ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<WorkflowConditionDto>.Ok(page.Adapt<List<WorkflowConditionDto>>(), totalCount);
        }
    }
}
