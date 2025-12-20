using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Commands;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Dto;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Entity;
using SystemAdmin.Model.SystemBasicMgmt.SystemBasicData.Queries;
using SystemAdmin.Repository.SystemBasicMgmt.SystemBasicData;

namespace SystemAdmin.Service.SystemBasicMgmt.SystemBasicData
{
    public class DepartmentLevelService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<DepartmentLevelService> _logger;
        private readonly SqlSugarScope _db;
        private readonly DepartmentLevelRepository _departmentLevelRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.DeptLevel";

        public DepartmentLevelService(CurrentUser loginuser, ILogger<DepartmentLevelService> logger, SqlSugarScope db, DepartmentLevelRepository departmentLevelRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _departmentLevelRepository = departmentLevelRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增部门级别
        /// </summary>
        /// <param name="deptLevelUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDepartmentLevel(DepartmentLevelUpsert deptLevelUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                DepartmentLevelEntity insertDeptLevelEntity = new DepartmentLevelEntity()
                {
                    DepartmentLevelId = SnowFlakeSingle.Instance.NextId(),
                    DepartmentLevelCode = deptLevelUpsert.DepartmentLevelCode,
                    DepartmentLevelNameCn = deptLevelUpsert.DepartmentLevelNameCn,
                    DepartmentLevelNameEn = deptLevelUpsert.DepartmentLevelNameEn,
                    SortOrder = deptLevelUpsert.SortOrder,
                    Description = deptLevelUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int insertDeptLevelCount = await _departmentLevelRepository.InsertDepartmentLevel(insertDeptLevelEntity);
                await _db.CommitTranAsync();

                return insertDeptLevelCount >= 1
                        ? Result<int>.Ok(insertDeptLevelCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// <param name="deptLevelUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDepartmentLevel(DepartmentLevelUpsert deptLevelUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int delDeptLevelCount = await _departmentLevelRepository.DeleteDepartmentLevel(long.Parse(deptLevelUpsert.DepartmentLevelId));
                await _db.CommitTranAsync();

                return delDeptLevelCount >= 1
                    ? Result<int>.Ok(delDeptLevelCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
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
        /// <param name="deptLevelUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDepartmentLevel(DepartmentLevelUpsert deptLevelUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                DepartmentLevelEntity updateDeptLevelEntity = new DepartmentLevelEntity()
                {
                    DepartmentLevelId = long.Parse(deptLevelUpsert.DepartmentLevelId),
                    DepartmentLevelCode = deptLevelUpsert.DepartmentLevelCode,
                    DepartmentLevelNameCn = deptLevelUpsert.DepartmentLevelNameCn,
                    DepartmentLevelNameEn = deptLevelUpsert.DepartmentLevelNameEn,
                    SortOrder = deptLevelUpsert.SortOrder,
                    Description = deptLevelUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                int updateDeptLevelcount = await _departmentLevelRepository.UpdateDepartmentLevel(updateDeptLevelEntity);
                await _db.CommitTranAsync();

                return updateDeptLevelcount >= 1
                        ? Result<int>.Ok(updateDeptLevelcount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="getDeptLevelEntity"></param>
        /// <returns></returns>
        public async Task<Result<DepartmentLevelDto>> GetDepartmentLevelEntity(GetDepartmentLevelEntity getDeptLevelEntity)
        {
            try
            {
                var depatLevelEntity = await _departmentLevelRepository.GetDepartmentLevelEntity(long.Parse(getDeptLevelEntity.DepartmentLevelId));
                return Result<DepartmentLevelDto>.Ok(depatLevelEntity, "");
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
                var deptLevelList = await _departmentLevelRepository.GetDepartmentLevelList();
                return Result<List<DepartmentLevelDto>>.Ok(deptLevelList, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDto>>.Failure(500, ex.Message);
            }
        }
    }
}
