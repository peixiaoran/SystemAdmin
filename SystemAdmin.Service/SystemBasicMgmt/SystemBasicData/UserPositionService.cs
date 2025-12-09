using Microsoft.Extensions.Logging;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class UserPositionService
    {
        private readonly ILogger<UserPositionService> _logger;
        private readonly UserPositionRepository _userPositionRepository;

        public UserPositionService(ILogger<UserPositionService> logger, UserPositionRepository userPositionRepository)
        {
            _logger = logger;
            _userPositionRepository = userPositionRepository;
        }

        /// <summary>
        /// 查询职级实体
        /// </summary>
        /// <param name="getUserPositionEntity"></param>
        /// <returns></returns>
        public async Task<Result<UserPositionDto>> GetUserPositionEntity(GetUserPositionEntity getUserPositionEntity)
        {
            try
            {
                var userPositionEntity = await _userPositionRepository.GetUserPositionEntity(long.Parse(getUserPositionEntity.PositionId));
                return Result<UserPositionDto>.Ok(userPositionEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<UserPositionDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询职级列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<UserPositionDto>>> GetUserPositionList()
        {
            try
            {
                var userPositionList = await _userPositionRepository.GetUserPositionList();
                return Result<List<UserPositionDto>>.Ok(userPositionList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<UserPositionDto>>.Failure(500, ex.Message);
            }
        }
    }
}
