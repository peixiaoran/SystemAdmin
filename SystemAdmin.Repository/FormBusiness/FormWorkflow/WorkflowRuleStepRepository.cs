using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class WorkflowRuleStepRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public WorkflowRuleStepRepository(SqlSugarScope db, Language lang)
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
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDrop(long groupId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormGroupId == groupId)
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
        /// 规则下拉
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<List<WorkflowRuleDropDto>> GetWorkflowRuleDrop(long formTypeId)
        {
            return await _db.Queryable<WorkflowRuleEntity>()
                            .With(SqlWith.NoLock)
                            .Where(rule => rule.FormTypeId == formTypeId)
                            .OrderBy(rule => rule.SortOrder)
                            .Select(rule => new WorkflowRuleDropDto
                            {
                                RuleId = rule.RuleId,
                                RuleName = _lang.Locale == "zh-CN"
                                           ? rule.RuleNameCn
                                           : rule.RuleNameEn,
                            }).ToListAsync();
        }

        /// <summary>
        /// 规则步骤是否重复配置
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <param name="nextStepId"></param>
        /// <returns></returns>
        public async Task<bool> RuleStepIsRepeat(long ruleId, long currentStepId, long nextStepId)
        {
            return await _db.Queryable<WorkflowRuleStepEntity>()
                            .With(SqlWith.NoLock)
                            .Where(rulestep => rulestep.RuleId == ruleId && (rulestep.CurrentStepId == currentStepId || rulestep.NextStepId == nextStepId))
                            .AnyAsync();
        }

        /// <summary>
        /// 规则步骤是否重复配置
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="nextStepId"></param>
        /// <returns></returns>
        public async Task<bool> RuleStepIsRepeat(long ruleId, long nextStepId)
        {
            return await _db.Queryable<WorkflowRuleStepEntity>()
                            .With(SqlWith.NoLock)
                            .Where(rulestep => rulestep.RuleId == ruleId && rulestep.NextStepId == nextStepId)
                            .AnyAsync();
        }

        /// <summary>
        /// 新增规则步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertWorkflowRuleStep(WorkflowRuleStepEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除规则步骤
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <returns></returns>
        public async Task<int> DeleteWorkflowRuleStep(long ruleId, long currentStepId)
        {
            return await _db.Deleteable<WorkflowRuleStepEntity>()
                            .Where(rulestep => rulestep.RuleId == ruleId && rulestep.CurrentStepId == currentStepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改规则步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateWorkflowRuleStep(WorkflowRuleStepEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(rulestep => new
                            {
                                rulestep.CreatedBy,
                                rulestep.CreatedDate,
                            }).Where(rulestep => rulestep.RuleId == entity.RuleId && rulestep.CurrentStepId == entity.CurrentStepId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询规则步骤实体
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="currentStepId"></param>
        /// <returns></returns>
        public async Task<WorkflowRuleDto> GetWorkflowRuleStepEntity(long ruleId, long currentStepId)
        {
            var entity = await _db.Queryable<WorkflowRuleStepEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(rulestep => rulestep.RuleId == ruleId && rulestep.CurrentStepId == currentStepId)
                                  .FirstAsync();
            return entity.Adapt<WorkflowRuleDto>();
        }

        /// <summary>
        /// 查询规则步骤列表
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public async Task<Result<List<WorkflowRuleStepDto>>> GetWorkflowRuleStepList(long ruleId)
        {
            var list = await _db.Queryable<WorkflowRuleStepEntity>()
                                .With(SqlWith.NoLock)
                                .InnerJoin<WorkflowStepEntity>((rulestep, currentstep) => rulestep.CurrentStepId == currentstep.StepId)
                                .LeftJoin<WorkflowStepEntity>((rulestep, currentstep, nextstep) => rulestep.NextStepId == nextstep.StepId)
                                .Where((rulestep, currentstep, nextstep) => rulestep.RuleId == ruleId)
                                .Select((rulestep, currentstep, nextstep) => new WorkflowRuleStepDto
                                {
                                    RuleId = rulestep.RuleId,
                                    CurrentStepId = currentstep.StepId,
                                    CurrentStepName = _lang.Locale == "zh-CN"
                                                      ? currentstep.StepNameCn
                                                      : currentstep.StepNameEn,
                                    NextStepId = nextstep.StepId,
                                    NextStepName = _lang.Locale == "zh-CN"
                                                      ? nextstep.StepNameCn
                                                      : nextstep.StepNameEn,
                                    SortOrder = rulestep.SortOrder,
                                }).ToListAsync();
            return Result<List<WorkflowRuleStepDto>>.Ok(list);
        }
    }
}
