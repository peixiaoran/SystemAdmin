using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowRuleRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowRuleRepository(SqlSugarScope db, Language lang)
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
        /// 表单类型下拉
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDrop(long formGroupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormGroupId == formGroupId)
                            .OrderBy(formtype => formtype.SortOrder)
                            .Select(formtype => new FormTypeDropDto
                            {
                                FormTypeId = formtype.FormTypeId,
                                FormTypeName = _lang.Locale == "zh-CN"
                                               ? formtype.FormTypeNameCn
                                               : formtype.FormTypeNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 职级下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<PositionDropDto>> GetPositionDrop()
        {
            return await _db.Queryable<PositionInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(position => position.CreatedDate)
                            .Select((position) => new PositionDropDto
                            {
                                PositionId = position.PositionId,
                                PositionName = _lang.Locale == "zh-CN"
                                               ? position.PositionNameCn
                                               : position.PositionNameEn
                            }).ToListAsync();
        }

        /// <summary>
        /// 规则是否重复配置
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <param name="guidance"></param>
        /// <returns></returns>
        public async Task<bool> RuleIsRepeat(long formTypeId, long positionId, string guidance)
        {
            return await _db.Queryable<WorkflowRuleEntity>()
                            .Where(branch => branch.FormTypeId == formTypeId && branch.PositionId == positionId && branch.Guidance == guidance)
                            .AnyAsync();
        }

        /// <summary>
        /// 新增规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowRule(WorkflowRuleEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询规则是否有步骤配置
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<bool> GetWorkflowRuleStepIsExist(long ruleId)
        {
            return await _db.Queryable<WorkflowRuleStepEntity>()
                            .Where(rule => rule.RuleId == ruleId)
                            .AnyAsync();
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowRule(long ruleId)
        {
            return await _db.Deleteable<WorkflowRuleEntity>()
                            .Where(rule => rule.RuleId == ruleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowRule(WorkflowRuleEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(rule => new
                            {
                                rule.FormTypeId,
                                rule.CreatedBy,
                                rule.CreatedDate,
                            }).Where(rule => rule.RuleId == entity.RuleId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询规则实体
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<WorkflowRuleDto> GetWorkflowRuleEntity(long ruleId)
        {
            var entity = await _db.Queryable<WorkflowRuleEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(rule => rule.RuleId == ruleId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowRuleDto>();
        }

        /// <summary>
        /// 查询规则分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<WorkflowRuleDto>> GetWorkflowRulePage(GetWorkflowRulePage getPage)
        {
            RefAsync<int> totalCount = 0;
            var page = await _db.Queryable<WorkflowRuleEntity>()
                                .With(SqlWith.NoLock)
                                .InnerJoin<PositionInfoEntity>((rule, position) => rule.PositionId == position.PositionId)
                                .Where((rule, position) => rule.FormTypeId == long.Parse(getPage.FormTypeId) && rule.PositionId == long.Parse(getPage.PositionId))
                                .Select((rule, position) => new WorkflowRuleDto
                                {
                                    RuleId = rule.RuleId,
                                    FormTypeId = rule.FormTypeId,
                                    RuleNameCn = rule.RuleNameCn,
                                    RuleNameEn = rule.RuleNameEn,
                                    PositionId = rule.PositionId,
                                    PositionName = _lang.Locale == "zh-CN"
                                                   ? position.PositionNameCn
                                                   : position.PositionNameEn,
                                    Guidance = rule.Guidance,
                                    SortOrder = rule.SortOrder,
                                }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<WorkflowRuleDto>.Ok(page, totalCount);
        }
    }
}
