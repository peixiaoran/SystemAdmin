using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;

namespace SystemAdmin.Repository.SystemBasicMgmt.SystemSettings
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
        /// 新增系统字典
        /// </summary>
        /// <param name="dictionaryEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertDictionaryInfo(DictionaryInfoEntity dictionaryEntity)
        {
            return await _db.Insertable(dictionaryEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除系统字典
        /// </summary>
        /// <param name="dicUpsert"></param>
        /// <returns></returns>
        public async Task<int> DeleteDictionaryInfo(DictionaryInfoUpsert dicUpsert)
        {
            return await _db.Deleteable<DictionaryInfoEntity>()
                            .Where(dicInfo => dicInfo.DicId == long.Parse(dicUpsert.DicId))
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改系统字典
        /// </summary>
        /// <param name="dicUpsert"></param>
        /// <returns></returns>
        public async Task<int> UpdateDictionaryInfo(DictionaryInfoEntity dicUpsert)
        {
            return await _db.Updateable(dicUpsert)
                            .IgnoreColumns(dic => new
                            {
                                dic.DicId,
                                dic.DicCode,
                                dic.CreatedBy,
                                dic.CreatedDate,
                            }).Where(dicInfo => dicInfo.DicId == dicUpsert.DicId)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询字典实体
        /// </summary>
        /// <param name="dicId"></param>
        /// <returns></returns>
        public async Task<DictionaryInfoDto> GetDictionaryInfoEntity(long dicId)
        {
            var dicEntity = await _db.Queryable<DictionaryInfoEntity>()
                                     .With(SqlWith.NoLock)
                                     .Where(dic => dic.DicId == dicId)
                                     .FirstAsync();
            return dicEntity.Adapt<DictionaryInfoDto>();
        }

        /// <summary>
        /// 查询字典分页
        /// </summary>
        /// <param name="getDicPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage(GetDictionaryInfoPage getDicPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<DictionaryInfoEntity>()
                           .With(SqlWith.NoLock)
                           .LeftJoin<ModuleInfoEntity>((dicinfo, moduleinfo) => dicinfo.ModuleId == moduleinfo.ModuleId);

            // 所属模块Id
            if (!string.IsNullOrEmpty(getDicPage.ModuleId))
            {
                query = query.Where((dicinfo, moduleinfo) => dicinfo.ModuleId == long.Parse(getDicPage.ModuleId));
            }
            // 字典名称
            if (!string.IsNullOrEmpty(getDicPage.DicName))
            {
                query = query.Where((dicinfo, moduleinfo) =>
                    dicinfo.DicNameCn.Contains(getDicPage.DicName) ||
                    dicinfo.DicNameEn.Contains(getDicPage.DicName));
            }
            // 字典类型
            if (!string.IsNullOrEmpty(getDicPage.DicType))
            {
                query = query.Where((dicinfo, moduleinfo) => dicinfo.DicType == getDicPage.DicType);
            }

            var dicPage = await query.OrderBy((dicinfo, moduleinfo) => dicinfo.SortOrder)
                                     .Select((dicinfo, moduleinfo) => new DictionaryInfoDto
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
                                         SortOrder = dicinfo.SortOrder,
                                     }).ToPageListAsync(getDicPage.PageIndex, getDicPage.PageSize, totalCount);
            return ResultPaged<DictionaryInfoDto>.Ok(dicPage.Adapt<List<DictionaryInfoDto>>(), totalCount, "");
        }

        /// <summary>
        /// 模块下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<ModuleDropDto>> GetModuleDropDown()
        {
            return await _db.Queryable<ModuleInfoEntity>()
                            .With(SqlWith.NoLock)
                            .OrderBy(module => module.SortOrder)
                            .Select(module => new ModuleDropDto
                            {
                                ModuleId = module.ModuleId,
                                ModuleName = _lang.Locale == "zh-CN"
                                             ? module.ModuleNameCn
                                             : module.ModuleNameEn,
                                Disabled = module.IsEnabled == 0
                            }).ToListAsync();
        }

        /// <summary>
        /// 字典类型下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<DicTypeDropDto>> GetDicTypeDropDown(long moduleId)
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
        public async Task<bool> GetDictionaryInfoIsExist(string dicType, int dicCode)
        {
            return await _db.Queryable<DictionaryInfoEntity>()
                            .With(SqlWith.NoLock)
                            .Where(dic => dic.DicType == dicType && dic.DicCode == dicCode)
                            .AnyAsync();
        }
    }
}
