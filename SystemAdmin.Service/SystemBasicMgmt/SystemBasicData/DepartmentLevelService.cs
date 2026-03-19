using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class DepartmentLevelService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<DepartmentLevelService> _logger;
        private readonly SqlSugarScope _db;
        private readonly DepartmentLevelRepository _deptLevelRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.DeptLevel";

        public DepartmentLevelService(CurrentUser loginuser, ILogger<DepartmentLevelService> logger, SqlSugarScope db, DepartmentLevelRepository deptLevelRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _deptLevelRepository = deptLevelRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增部门级别
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDepartmentLevel(DepartmentLevelUpsert upsert)
        {
            try
            {
                var entity = new DepartmentLevelEntity()
                {
                    DepartmentLevelId = SnowFlakeSingle.Instance.NextId(),
                    DepartmentLevelCode = upsert.DepartmentLevelCode,
                    DepartmentLevelNameCn = upsert.DepartmentLevelNameCn,
                    DepartmentLevelNameEn = upsert.DepartmentLevelNameEn,
                    SortOrder = upsert.SortOrder,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                int count = await _deptLevelRepository.InsertDepartmentLevel(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 删除部门级别
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDepartmentLevel(string deptlevelId)
        {
            try
            {
                await _db.BeginTranAsync();
                int count = await _deptLevelRepository.DeleteDepartmentLevel(long.Parse(deptlevelId));
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 修改部门级别
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDepartmentLevel(DepartmentLevelUpsert upsert)
        {
            try
            {
                var entity = new DepartmentLevelEntity()
                {
                    DepartmentLevelId = long.Parse(upsert.DepartmentLevelId),
                    DepartmentLevelCode = upsert.DepartmentLevelCode,
                    DepartmentLevelNameCn = upsert.DepartmentLevelNameCn,
                    DepartmentLevelNameEn = upsert.DepartmentLevelNameEn,
                    SortOrder = upsert.SortOrder,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now
                };

                await _db.BeginTranAsync();
                int count = await _deptLevelRepository.UpdateDepartmentLevel(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询部门级别实体
        /// </summary>
        /// <param name="deptlevelId"></param>
        /// <returns></returns>
        public async Task<Result<DepartmentLevelDto>> GetDepartmentLevelEntity(string deptlevelId)
        {
            try
            {
                var entity = await _deptLevelRepository.GetDepartmentLevelEntity(long.Parse(deptlevelId));
                return Result<DepartmentLevelDto>.Ok(entity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<DepartmentLevelDto>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 查询部门级别列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDto>>> GetDepartmentLevelList()
        {
            try
            {
                var list = await _deptLevelRepository.GetDepartmentLevelList();
                return Result<List<DepartmentLevelDto>>.Ok(list, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDto>>.Failure(500, ex.Message);
            }
        }
    }
}
