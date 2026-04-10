using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Queries;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormWorkflow
{
    public class FormApprovalLimitRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormApprovalLimitRepository(SqlSugarScope db, Language lang)
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
        /// 新增职级签至最大范围
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormApprovalLimit(FormApprovalLimitEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除职级签至最大范围
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteFormApprovalLimit(long formTypeId, long positionId)
        {
            return await _db.Deleteable<FormApprovalLimitEntity>()
                            .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增职级签至最大范围
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormApprovalLimit(FormApprovalLimitEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(limit => new
                            {
                                limit.FormTypeId,
                                limit.PositionId,
                                limit.CreatedBy,
                                limit.CreatedDate,
                            }).Where(limit => limit.FormTypeId == entity.FormTypeId && limit.PositionId == entity.PositionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询分支实体
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<FormApprovalLimitDto> GetFormApprovalLimitEntity(long formTypeId, long positionId)
        {
            var entity = await _db.Queryable<FormApprovalLimitEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                                  .FirstAsync();
            return entity.Adapt<FormApprovalLimitDto>();
        }

        /// <summary>
        /// 查询分支分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormApprovalLimitDto>> GetFormApprovalLimitPage(GetFormApprovalLimitPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<UserPositionEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormApprovalLimitEntity>((position, limit) => position.PositionId == limit.PositionId)
                           .InnerJoin<UserPositionEntity>((position, limit, maxposition) => limit.MaxPositionId == maxposition.PositionId);

            if (!string.IsNullOrEmpty(getPage.FormTypeId) && long.Parse(getPage.FormTypeId) > -1)
            {
                query.Where((position, limit, maxposition) => limit.FormTypeId == long.Parse(getPage.FormTypeId));
            }

            var page = await query.OrderByDescending((position, limit, maxposition) => position.SortOrder)
                                  .Select((position, limit, maxposition) => new FormApprovalLimitDto
                                  {
                                      FormTypeId = limit.FormTypeId,
                                      PositionId = position.PositionId,
                                      PositionName = _lang.Locale == "zh-CN"
                                                     ? position.PositionNameCn
                                                     : position.PositionNameEn,
                                      MaxPositionId = maxposition.PositionId,
                                      MaxPositionName = _lang.Locale == "zh-CN"
                                                     ? maxposition.PositionNameCn
                                                     : maxposition.PositionNameEn,
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<FormApprovalLimitDto>.Ok(page, totalCount);
        }

        /// <summary>
        /// 查询职级签至最大范围是否存在
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<bool> FormApprovalLimitRepeat(long formTypeId, long positionId)
        {
            return await _db.Queryable<FormApprovalLimitEntity>()
                            .With(SqlWith.NoLock)
                            .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                            .AnyAsync();
        }
    }
}
