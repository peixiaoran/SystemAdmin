using Microsoft.AspNetCore.Mvc;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Service.SystemBasicMgmt.SystemBasicData;
using SystemAdmin.WebApi.Attributes;

namespace SystemAdmin.WebApi.Controllers.SystemBasicMgmt.SystemBasicData
{
    [JwtAuthorize]
    [RoutingAuthorize]
    [Route("api/SystemBasicMgmt/SystemBasicData/[controller]/[action]")]
    [ApiController]
    public class UserLabor : ControllerBase
    {
        private readonly UserLaborService _userLaborService;
        public UserLabor(UserLaborService userLaborService)
        {
            _userLaborService = userLaborService;
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职业] 新增职业信息")]
        public async Task<Result<int>> InsertUserLabor([FromBody] UserLaborUpsert userLaborUpsert)
        {
            return await _userLaborService.InsertUserLabor(userLaborUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职业] 删除职业信息")]
        public async Task<Result<int>> DeleteUserLabor([FromBody] UserLaborUpsert userLaborUpsert)
        {
            return await _userLaborService.DeleteUserLabor(userLaborUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职业] 修改职业信息")]
        public async Task<Result<int>> UpdateUserLabor([FromBody] UserLaborUpsert userLaborUpsert)
        {
            return await _userLaborService.UpdateUserLabor(userLaborUpsert);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职业] 查询职业实体")]
        public async Task<Result<UserLaborDto>> GetUserLaborEntity([FromBody] GetUserLaborEntity getUserLaborEntity)
        {
            return await _userLaborService.GetUserLaborEntity(getUserLaborEntity);
        }

        [HttpPost]
        [Tags("系统基础管理-基本信息模块")]
        [EndpointSummary("[职业] 查询职业分页")]
        public async Task<ResultPaged<UserLaborDto>> GetUserLaborPage([FromBody] GetUserLaborPage getUserLaborPage)
        {
            return await _userLaborService.GetUserLaborPage(getUserLaborPage);
        }
    }
}
