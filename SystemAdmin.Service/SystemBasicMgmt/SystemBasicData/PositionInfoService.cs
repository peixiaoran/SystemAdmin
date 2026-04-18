using Microsoft.Extensions.Logging;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class PositionInfoService
    {
        private readonly ILogger<PositionInfoService> _logger;
        private readonly PositionInfoRepository _userPositionRepository;

        public PositionInfoService(ILogger<PositionInfoService> logger, PositionInfoRepository userPositionRepository)
        {
            _logger = logger;
            _userPositionRepository = userPositionRepository;
        }

        /// <summary>
        /// 查询职级实体
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public async Task<Result<PositionInfoDto>> GetPositionInfoEntity(string positionId)
        {
            try
            {
                var entity = await _userPositionRepository.GetPositionInfoEntity(long.Parse(positionId));
                return Result<PositionInfoDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<PositionInfoDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询职级列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<PositionInfoDto>>> GetPositionInfoList()
        {
            try
            {
                var list = await _userPositionRepository.GetPositionInfoList();
                return Result<List<PositionInfoDto>>.Ok(list, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<PositionInfoDto>>.Failure(500, ex.Message);
            }
        }
    }
}
