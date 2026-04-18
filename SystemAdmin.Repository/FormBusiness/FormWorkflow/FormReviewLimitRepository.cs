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
    public class FormReviewLimitRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public FormReviewLimitRepository(SqlSugarScope db, Language lang)
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
        /// 表单类别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<FormTypeDropDto>> GetFormTypeDrop(long formGroupId)
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
        /// 新增签核层级上限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormReviewLimit(FormReviewLimitEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除签核层级上限
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<int> DeleteFormReviewLimit(long formTypeId, long positionId)
        {
            return await _db.Deleteable<FormReviewLimitEntity>()
                            .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改签核层级上限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormReviewLimit(FormReviewLimitEntity entity)
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
        /// 查询签核层级上限实体
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<FormReviewLimitDto> GetFormReviewLimitEntity(long formTypeId, long positionId)
        {
            var entity = await _db.Queryable<FormReviewLimitEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                                  .FirstAsync();
            return entity.Adapt<FormReviewLimitDto>();
        }

        /// <summary>
        /// 查询签核层级上限分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<FormReviewLimitDto>> GetFormReviewLimitPage(GetFormReviewLimitPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<PositionInfoEntity>()
                           .With(SqlWith.NoLock)
                           .InnerJoin<FormReviewLimitEntity>((position, limit) => position.PositionId == limit.PositionId)
                           .InnerJoin<PositionInfoEntity>((position, limit, maxposition) => limit.MaxPositionId == maxposition.PositionId)
                           .Where((position, limit, maxposition) => limit.FormTypeId == long.Parse(getPage.FormTypeId));

            var page = await query.OrderByDescending((position, limit, maxposition) => position.SortOrder)
                                  .Select((position, limit, maxposition) => new FormReviewLimitDto
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
            return ResultPaged<FormReviewLimitDto>.Ok(page, totalCount);
        }

        /// <summary>
        /// 查询签核层级上限是否存在
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<bool> FormReviewLimitRepeat(long formTypeId, long positionId)
        {
            return await _db.Queryable<FormReviewLimitEntity>()
                            .With(SqlWith.NoLock)
                            .Where(limit => limit.FormTypeId == formTypeId && limit.PositionId == positionId)
                            .AnyAsync();
        }
    }
}
