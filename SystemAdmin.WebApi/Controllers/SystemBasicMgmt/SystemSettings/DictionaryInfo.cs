using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemMgmt.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemSettings.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemSettings;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemSettings
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemSettings/[controller]/[action]")]
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
        public async Task<Result<int>> InsertDictionaryInfo([FromBody] DictionaryInfoUpsert dicUpsert)
        {
            return await _dictionaryService.InsertDictionaryInfo(dicUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 删除字典信息")]
        public async Task<Result<int>> DeleteDictionaryInfo([FromBody] DictionaryInfoUpsert dicUpsert)
        {
            return await _dictionaryService.DeleteDictionaryInfo(dicUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 修改字典信息")]
        public async Task<Result<int>> UpdateDictionaryInfo([FromBody] DictionaryInfoUpsert dicUpsert)
        {
            return await _dictionaryService.UpdateDictionaryInfo(dicUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 查询字典实体")]
        public async Task<Result<DictionaryInfoDto>> GetDictionaryInfoEntity([FromBody] GetDictionaryInfoEntity getDicEntity)
        {
            return await _dictionaryService.GetDictionaryInfoEntity(getDicEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 查询字典分页")]
        public async Task<ResultPaged<DictionaryInfoDto>> GetDictionaryInfoPage([FromBody] GetDictionaryInfoPage getDicPage)
        {
            return await _dictionaryService.GetDictionaryInfoPage(getDicPage);
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 模块下拉框")]
        public async Task<Result<List<ModuleDropDto>>> GetModuleDropDown()
        {
            return await _dictionaryService.GetModuleDropDown();
        }

        [HttpPost]
        [Tags("系统基础管理-系统设定模块")]
        [EndpointSummary("[系统字典] 字典类型下拉框")]
        public async Task<Result<List<DicTypeDropDto>>> GetDicTypeDropDown(GetDicTypeDropDown getDicTypeDropDown)
        {
            return await _dictionaryService.GetDicTypeDropDown(getDicTypeDropDown);
        }
    }
}
