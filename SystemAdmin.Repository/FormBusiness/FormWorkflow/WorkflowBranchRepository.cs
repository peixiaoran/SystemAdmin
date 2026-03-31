using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowBranchRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowBranchRepository(SqlSugarScope db, Language lang)
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
        /// 新增分支
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowBranch(WorkflowBranchEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询分支是否有对应分支步骤
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<bool> GetWorkflowStepBranchByCon(long branchId)
        {
            return await _db.Queryable<WorkflowBranchStepEntity>()
                            .Where(branch => branch.BranchId == branchId)
                            .AnyAsync();
        }

        /// <summary>
        /// 删除分支
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowBranch(long branchId)
        {
            return await _db.Deleteable<WorkflowBranchEntity>()
                            .Where(branch => branch.BranchId == branchId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改分支
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowBranch(WorkflowBranchEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(branch => new
                            {
                                branch.BranchId,
                                branch.FormTypeId,
                                branch.CreatedBy,
                                branch.CreatedDate,
                            }).Where(branch => branch.BranchId == entity.BranchId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询分支实体
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public async Task<WorkflowBranchDto> GetWorkflowBranchEntity(long branchId)
        {
            var entity = await _db.Queryable<WorkflowBranchEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(branch => branch.BranchId == branchId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowBranchDto>();
        }

        /// <summary>
        /// 查询分支分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowBranchDto>> GetWorkflowBranchPage(GetWorkflowBranchPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var page = await _db.Queryable<WorkflowBranchEntity>()
                                .With(SqlWith.NoLock)
                                .OrderByDescending(branch => branch.CreatedDate)
                                .ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<WorkflowBranchDto>.Ok(page.Adapt<List<WorkflowBranchDto>>(), totalCount);
        }
    }
}
