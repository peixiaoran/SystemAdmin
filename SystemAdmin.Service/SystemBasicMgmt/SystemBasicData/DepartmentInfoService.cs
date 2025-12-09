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
    public class DepartmentInfoService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<DepartmentInfoService> _logger;
        private readonly SqlSugarScope _db;
        private readonly DepartmentInfoRepository _departmentInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt_SystemBasicData_DepartmentInfo_";

        public DepartmentInfoService(CurrentUser loginuser, ILogger<DepartmentInfoService> logger, SqlSugarScope db, DepartmentInfoRepository departmentInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _departmentInfoRepository = departmentInfoRepository;
            _localization = localization;
        }

        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="deptUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDepartmentInfo(DepartmentInfoUpsert deptUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                DepartmentInfoEntity insertDeptEntity = new DepartmentInfoEntity()
                {
                    DepartmentId = SnowFlakeSingle.Instance.NextId(),
                    DepartmentCode = deptUpsert.DepartmentCode,
                    DepartmentNameCn = deptUpsert.DepartmentNameCn,
                    DepartmentNameEn = deptUpsert.DepartmentNameEn,
                    ParentId = long.Parse(deptUpsert.ParentId),
                    DepartmentLevelId = long.Parse(deptUpsert.DepartmentLevelId),
                    SortOrder = deptUpsert.SortOrder,
                    Landline = deptUpsert.Landline,
                    Email = deptUpsert.Email,
                    Address = deptUpsert.Address,
                    IsEnabled = deptUpsert.IsEnabled,
                    Description = deptUpsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int insertDeptCount = await _departmentInfoRepository.InsertDepartmentInfo(insertDeptEntity);

                await _db.CommitTranAsync();
                return insertDeptCount >= 1
                        ? Result<int>.Ok(insertDeptCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="deptUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDepartmentInfo(DepartmentInfoUpsert deptUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                int delDeptCount = await _departmentInfoRepository.DeleteDepartmentInfo(long.Parse(deptUpsert.DepartmentId));
                await _db.CommitTranAsync();

                return delDeptCount >= 1
                    ? Result<int>.Ok(delDeptCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                    : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="deptUpsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDepartmentInfo(DepartmentInfoUpsert deptUpsert)
        {
            try
            {
                await _db.BeginTranAsync();
                DepartmentInfoEntity updateDeptEntity = new DepartmentInfoEntity()
                {
                    DepartmentId = long.Parse(deptUpsert.DepartmentId),
                    DepartmentCode = deptUpsert.DepartmentCode,
                    DepartmentNameCn = deptUpsert.DepartmentNameCn,
                    DepartmentNameEn = deptUpsert.DepartmentNameEn,
                    ParentId = long.Parse(deptUpsert.ParentId),
                    DepartmentLevelId = long.Parse(deptUpsert.DepartmentLevelId),
                    SortOrder = deptUpsert.SortOrder,
                    Landline = deptUpsert.Landline,
                    Email = deptUpsert.Email,
                    Address = deptUpsert.Address,
                    IsEnabled = deptUpsert.IsEnabled,
                    Description = deptUpsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                int updateDeptCount = await _departmentInfoRepository.UpdateDepartmentInfo(updateDeptEntity);
                await _db.CommitTranAsync();

                return updateDeptCount >= 1
                        ? Result<int>.Ok(updateDeptCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询部门实体
        /// </summary>
        /// <param name="getDeptEntity"></param>
        /// <returns></returns>
        public async Task<Result<DepartmentInfoDto>> GetDepartmentInfoEntity(GetDepartmentInfoEntity getDeptEntity)
        {
            try
            {
                var deptEntity = await _departmentInfoRepository.GetDepartmentInfoEntity(long.Parse(getDeptEntity.DepartmentId));
                return Result<DepartmentInfoDto>.Ok(deptEntity, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<DepartmentInfoDto>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 查询部门树
        /// </summary>
        /// <param name="getDeptTree"></param>
        /// <returns></returns>
        public async Task<Result<List<DepartmentInfoDto>>> GetDepartmentInfoTree(GetDepartmentTree getDeptTree)
        {
            try
            {
                var deptTree = await _departmentInfoRepository.GetDepartmentInfoTree(getDeptTree);
                return Result<List<DepartmentInfoDto>>.Ok(deptTree, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentInfoDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDropDown()
        {
            try
            {
                var deptDrop = await _departmentInfoRepository.GetDepartmentDropDown();
                return Result<List<DepartmentDropDto>>.Ok(deptDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门级别下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDropDown()
        {
            try
            {
                var deptLevelDrop = await _departmentInfoRepository.GetDepartmentLevelDropDown();
                return Result<List<DepartmentLevelDropDto>>.Ok(deptLevelDrop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDropDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
