using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemConfig
{
    public class DictionaryInfoRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public DictionaryInfoRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 字典类型下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<DicTypeDropDto>> GetDicTypeDrop(long moduleId)
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.ModuleId == moduleId)
                            .GroupBy(dic => dic.DicType)
                            .OrderBy(dic => dic.DicType)
                            .Select(dic => new DicTypeDropDto
                            {
                                DicTypeCode = dic.DicType,
                                DicTypeName = dic.DicType,
                            }).ToListAsync();
        }

        /// <summary>
        /// 查询同一个字典类型下字典编码是否存在
        /// </summary>
        /// <param name="dicType"></param>
        /// <param name="dicCode"></param>
        /// <returns></returns>
        public async Task<bool> GetDictionaryInfoIsExist(string dicType, string dicCode)
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.DicType == dicType && dic.DicCode == dicCode)
                            .AnyAsync();
        }

        /// <summary>
        /// 新增系统字典
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertDictionaryInfo(DictionaryInfoEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除系统字典
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<int> DeleteDictionaryInfo(DictionaryInfoUpsert upsert)
        {
            return await _db.Deleteable<DictionaryInfoEntity>()
                            .Where(dicInfo => dicInfo.DicId == long.Parse(upsert.DicId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改系统字典
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<int> UpdateDictionaryInfo(DictionaryInfoEntity upsert)
        {
            return await _db.Updateable(upsert)
                            .IgnoreColumns(dic => new
                            {
                                dic.DicId,
                                dic.DicCode,
                                dic.CreatedBy,
                                dic.CreatedDate,
                            }).Where(dicInfo => dicInfo.DicId == upsert.DicId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询字典实体
        /// </summary>
        /// <param name="dicId"></param>
        /// <returns></returns>
        public async Task<DictionaryInfoDto> GetDictionaryInfoEntity(long dicId)
        {
            var entity = await _db.Queryable<DictionaryInfoEntity>()
                                  .With(SqlWith.NoLock)
                                  .Where(dic => dic.DicId == dicId)
                                  .FirstAsync();
            return entity.Adapt<DictionaryInfoDto>();
        }

        /// <summary>
        /// 查询字典分页
        /// </summary>
        /// <param name="getPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage(GetDictionaryInfoPage getPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<DictionaryInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<ModuleInfoEntity>((dicinfo, moduleinfo) => dicinfo.ModuleId == moduleinfo.ModuleId);

            // 所属模块Id
            if (!string.IsNullOrEmpty(getPage.ModuleId))
            {
                query = query.Where((dicinfo, moduleinfo) => dicinfo.ModuleId == long.Parse(getPage.ModuleId));
            }
            // 字典名称
            if (!string.IsNullOrEmpty(getPage.DicName))
            {
                query = query.Where((dicinfo, moduleinfo) =>
                    dicinfo.DicNameCn.Contains(getPage.DicName) ||
                    dicinfo.DicNameEn.Contains(getPage.DicName));
            }
            // 字典类型
            if (!string.IsNullOrEmpty(getPage.DicType))
            {
                query = query.Where((dicinfo, moduleinfo) => dicinfo.DicType == getPage.DicType);
            }

            // 排序
            query = query.OrderBy((dicinfo, moduleinfo) => dicinfo.SortOrder);

            var page = await query.Select((dicinfo, moduleinfo) => new DictionaryInfoDto
                                  {
                                      DicId = dicinfo.DicId,
                                      ModuleId = dicinfo.ModuleId,
                                      ModuleName = _lang.Locale == "zh-CN"
                                                   ? moduleinfo.ModuleNameCn
                                                   : moduleinfo.ModuleNameEn,
                                      DicType = dicinfo.DicType,
                                      DicCode = dicinfo.DicCode,
                                      DicNameCn = dicinfo.DicNameCn,
                                      DicNameEn = dicinfo.DicNameEn,
                                      SortOrder = dicinfo.SortOrder
                                  }).ToPageListAsync(getPage.PageIndex, getPage.PageSize, totalCount);
            return ResultPaged<DictionaryInfoDto>.Ok(page.Adapt<List<DictionaryInfoDto>>(), totalCount, "");
        }

        /// <summary>
        /// 模块下拉
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDropDto>> GetModuleDrop()
        {
            return await _db.Queryable<ModuleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(module => module.SortOrder)
                            .Select(module => new ModuleDropDto
                            {
                                ModuleId = module.ModuleId,
                                ModuleName = _lang.Locale == "zh-CN"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn
                            }).ToListAsync();
        }
    }
}
