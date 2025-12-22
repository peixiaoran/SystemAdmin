using Mapster;
using SqlSugar;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Dto;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Queries;

namespace SystemAdmin.Repository.FormBusiness.FormBasicInfo
{
    public class ControlInfoRepository
    {
        private readonly SqlSugarScope _db;
        private readonly Language _lang;

        public ControlInfoRepository(SqlSugarScope db, Language lang)
        {
            _db = db;
            _lang = lang;
        }

        /// <summary>
        /// 新增控件
        /// </summary>
        /// <param name="controlInfoEntity"></param>
        /// <returns></returns>
        public async Task<int> InsertControlInfo(ControlInfoEntity controlInfoEntity)
        {
            return await _db.Insertable(controlInfoEntity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="controlCode"></param>
        /// <returns></returns>
        public async Task<int> DeleteControlInfo(string controlCode)
        {
            return await _db.Deleteable<ControlInfoEntity>()
                            .Where(formcontrol => formcontrol.ControlCode == controlCode)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改控件
        /// </summary>
        /// <param name="controlInfoEntity"></param>
        /// <returns></returns>
        public async Task<int> UpdateControlInfo(ControlInfoEntity controlInfoEntity)
        {
            return await _db.Updateable(controlInfoEntity)
                            .IgnoreColumns(formcontrol => new
                            {
                                formcontrol.ControlCode,
                                formcontrol.CreatedBy,
                                formcontrol.CreatedDate,
                            }).Where(formcontrol => formcontrol.ControlCode == controlInfoEntity.ControlCode)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询控件信息实体
        /// </summary>
        /// <param name="controlCode"></param>
        /// <returns></returns>
        public async Task<ControlInfoDto> GetControlInfoEntity(string controlCode)
        {
            var controlInfoEntity = await _db.Queryable<ControlInfoEntity>()
                                             .With(SqlWith.NoLock)
                                             .Where(formcontrol => formcontrol.ControlCode == controlCode)
                                             .FirstAsync();
            return controlInfoEntity.Adapt<ControlInfoDto>();
        }

        /// <summary>
        /// 查询控件信息分页
        /// </summary>
        /// <param name="getControlInfoPage"></param>
        /// <returns></returns>
        public async Task<ResultPaged<ControlInfoDto>> GetControlInfoPage(GetControlInfoPage getControlInfoPage)
        {
            RefAsync<int> totalCount = 0;
            var query = _db.Queryable<ControlInfoEntity>()
                           .With(SqlWith.NoLock);

            // 控件编码
            if (!string.IsNullOrEmpty(getControlInfoPage.ControlCode))
            {
                query = query.Where(control => control.ControlCode.Contains(getControlInfoPage.ControlCode));
            }

            var controlInfoPage = await query.ToPageListAsync(getControlInfoPage.PageIndex, getControlInfoPage.PageSize, totalCount);
            return ResultPaged<ControlInfoDto>.Ok(controlInfoPage.Adapt<List<ControlInfoDto>>(), totalCount, "");
        }
    }
}
