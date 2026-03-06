using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemConfig.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemConfig;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemConfig
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemConfig/[controller]/[action]")]
    [ApiController]
    public class DictionaryInfo : ControllerBase
    {
        private readonly DictionaryInfoService _dictionaryService;

        public DictionaryInfo(DictionaryInfoService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 新增字典信息")]
        public async Task<Result<int>> InsertDictionaryInfo([FromBody] DictionaryInfoUpsert upsert)
        {
            return await _dictionaryService.InsertDictionaryInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 删除字典信息")]
        public async Task<Result<int>> DeleteDictionaryInfo([FromBody] DictionaryInfoUpsert upsert)
        {
            return await _dictionaryService.DeleteDictionaryInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 修改字典信息")]
        public async Task<Result<int>> UpdateDictionaryInfo([FromBody] DictionaryInfoUpsert upsert)
        {
            return await _dictionaryService.UpdateDictionaryInfo(upsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 查询字典实体")]
        public async Task<Result<DictionaryInfoDto>> GetDictionaryInfoEntity([FromBody] GetDictionaryInfoEntity getEntity)
        {
            return await _dictionaryService.GetDictionaryInfoEntity(getEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 查询字典分页")]
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage([FromBody] GetDictionaryInfoPage getPage)
        {
            return await _dictionaryService.GetDictionaryInfoPage(getPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 模块下拉")]
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            return await _dictionaryService.GetModuleDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 字典类型下拉")]
        public async Task<Result<List<DicTypeDropDto>>> GetDicTypeDropDown(GetDicTypeDropDown getDrop)
        {
            return await _dictionaryService.GetDicTypeDropDown(getDrop);
        }
    }
}
