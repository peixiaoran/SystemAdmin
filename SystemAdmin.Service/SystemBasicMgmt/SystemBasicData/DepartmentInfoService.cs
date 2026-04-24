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
        private readonly DepartmentInfoRepository _deptInfoRepo;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.DeptInfo";

        public DepartmentInfoService(CurrentUser loginuser, ILogger<DepartmentInfoService> logger, SqlSugarScope db, DepartmentInfoRepository deptInfoRepo, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _deptInfoRepo = deptInfoRepo;
            _localization = localization;
        }

        /// <summary>
        /// 部门下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentDropDto>>> GetDepartmentDrop()
        {
            try
            {
                var drop = await _deptInfoRepo.GetDepartmentDrop();
                return Result<List<DepartmentDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 部门级别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<DepartmentLevelDropDto>>> GetDepartmentLevelDrop()
        {
            try
            {
                var drop = await _deptInfoRepo.GetDepartmentLevelDrop();
                return Result<List<DepartmentLevelDropDto>>.Ok(drop, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentLevelDropDto>>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertDepartmentInfo(DepartmentInfoUpsert upsert)
        {
            try
            {
                var entity = new DepartmentInfoEntity()
                {
                    DepartmentId = SnowFlakeSingle.Instance.NextId(),
                    DepartmentCode = upsert.DepartmentCode,
                    DepartmentNameCn = upsert.DepartmentNameCn,
                    DepartmentNameEn = upsert.DepartmentNameEn,
                    ParentId = long.Parse(upsert.ParentId),
                    DepartmentLevelId = long.Parse(upsert.DepartmentLevelId),
                    SortOrder = upsert.SortOrder,
                    Landline = upsert.Landline,
                    Email = upsert.Email,
                    Address = upsert.Address,
                    Description = upsert.Description,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                int count = await _deptInfoRepo.InsertDepartmentInfo(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}InsertSuccess"))
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
        /// 删除部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<Result<int>> DeleteDepartmentInfo(string deptId)
        {
            var userList = await _deptInfoRepo.GetUserInfoList();
            var deptList = await _deptInfoRepo.GetDepartmentInfoList();

            var deptChildrenMap = deptList.GroupBy(dept => dept.ParentId).ToDictionary(g => g.Key, g => g.ToList());

            bool CanDelete(long deptId)
            {
                if (userList.Any(user => user.DepartmentId == deptId)) return false;

                if (deptChildrenMap.TryGetValue(deptId, out var children))
                {
                    foreach (var child in children)
                    {
                        if (!CanDelete(child.DepartmentId)) return false;
                    }
                }
                return true;
            }

            async Task<int> DeleteRecursive(long deptId)
            {
                int count = 0;

                if (deptChildrenMap.TryGetValue(deptId, out var children))
                {
                    foreach (var child in children)
                    {
                        count += await DeleteRecursive(child.DepartmentId);
                    }
                }

                count += await _deptInfoRepo.DeleteDepartmentInfo(deptId);
                return count;
            }

            if (!CanDelete(long.Parse(deptId)))
            {
                return Result<int>.Failure(400, _localization.ReturnMsg($"{_this}NotDelete"));
            }

            var totalCount = await DeleteRecursive(long.Parse(deptId));
            return totalCount >= 1
                        ? Result<int>.Ok(totalCount, _localization.ReturnMsg($"{_this}DeleteSuccess"))
                        : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeleteFailed"));
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> UpdateDepartmentInfo(DepartmentInfoUpsert upsert)
        {
            try
            {
                var entity = new DepartmentInfoEntity()
                {
                    DepartmentId = long.Parse(upsert.DepartmentId),
                    DepartmentCode = upsert.DepartmentCode,
                    DepartmentNameCn = upsert.DepartmentNameCn,
                    DepartmentNameEn = upsert.DepartmentNameEn,
                    ParentId = long.Parse(upsert.ParentId),
                    DepartmentLevelId = long.Parse(upsert.DepartmentLevelId),
                    SortOrder = upsert.SortOrder,
                    Landline = upsert.Landline,
                    Email = upsert.Email,
                    Address = upsert.Address,
                    Description = upsert.Description,
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };

                await _db.BeginTranAsync();
                int count = await _deptInfoRepo.UpdateDepartmentInfo(entity);
                await _db.CommitTranAsync();

                return count >= 1
                        ? Result<int>.Ok(count, _localization.ReturnMsg($"{_this}UpdateSuccess"))
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
        /// <param name="deptId"></param>
        /// <returns></returns>
        public async Task<Result<DepartmentInfoDto>> GetDepartmentInfoEntity(string deptId)
        {
            try
            {
                var entity = await _deptInfoRepo.GetDepartmentInfoEntity(long.Parse(deptId));
                return Result<DepartmentInfoDto>.Ok(entity, "");
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
        /// <param name="getTree"></param>
        /// <returns></returns>
        public async Task<Result<List<DepartmentInfoDto>>> GetDepartmentInfoTree(GetDepartmentTree getTree)
        {
            try
            {
                var tree = await _deptInfoRepo.GetDepartmentInfoTree(getTree);
                return Result<List<DepartmentInfoDto>>.Ok(tree, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<DepartmentInfoDto>>.Failure(500, ex.Message.ToString());
            }
        }
    }
}
