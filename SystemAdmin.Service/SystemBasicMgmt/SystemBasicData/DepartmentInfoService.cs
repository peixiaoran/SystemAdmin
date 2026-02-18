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
        private readonly DepartmentInfoRepository _deptInfoRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "SystemBasicMgmt.SystemBasicData.DeptInfo";

        public DepartmentInfoService(CurrentUser loginuser, ILogger<DepartmentInfoService> logger, SqlSugarScope db, DepartmentInfoRepository deptInfoRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _deptInfoRepository = deptInfoRepository;
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
                bool codeExist = await _deptInfoRepository.GetDepartCodeIsExist(deptUpsert.DepartmentCode);
                if (codeExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeptCodeExist"));
                }
                else
                {
                    DepartmentInfoEntity insertDept = new DepartmentInfoEntity()
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
                        Description = deptUpsert.Description,
                        CreatedBy = _loginuser.UserId,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };
                    int insertDeptCount = await _deptInfoRepository.InsertDepartmentInfo(insertDept);
                    await _db.CommitTranAsync();

                    return insertDeptCount >= 1
                            ? Result<int>.Ok(insertDeptCount, _localization.ReturnMsg($"{_this}InsertSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}InsertFailed"));
                }
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
                int delDeptCount = await DeleteDepartmentWithChildrenAsync(long.Parse(deptUpsert.DepartmentId));
                if (delDeptCount == 0)
                {
                    await _db.RollbackTranAsync();
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DepartmentHasUsers"));
                }
                await _db.CommitTranAsync();

                return Result<int>.Ok(delDeptCount, _localization.ReturnMsg($"{_this}DeleteSuccess"));
            }
            catch (Exception ex)
            {
                await _db.RollbackTranAsync();
                _logger.LogError(ex, ex.Message);
                return Result<int>.Failure(500, ex.Message.ToString());
            }
        }

        /// <summary>
        /// 删除部门及其子部门（只有当部门及子部门下没有人员时才删除）
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        private async Task<int> DeleteDepartmentWithChildrenAsync(long departmentId)
        {
            // 获取所有人员和部门信息
            var userList = await _deptInfoRepository.GetUserInfoList();
            var deptList = await _deptInfoRepository.GetDepartmentInfoList();

            // 构建部门树字典，Parent -> List<Dept>
            var deptChildrenMap = deptList
                .GroupBy(dept => dept.ParentId)
                .ToDictionary(g => g.Key, g => g.ToList());

            // 判断部门及子部门是否可以删除
            bool CanDelete(long deptId)
            {
                // dept 下是否有人员
                if (userList.Any(user => user.DepartmentId == deptId)) return false;

                // 递归判断子部门
                if (deptChildrenMap.TryGetValue(deptId, out var children))
                {
                    foreach (var child in children)
                    {
                        if (!CanDelete(child.DepartmentId)) return false;
                    }
                }
                return true;
            }

            // 递归删除部门
            async Task<int> DeleteRecursive(long deptId)
            {
                int deleteCount = 0;

                if (deptChildrenMap.TryGetValue(deptId, out var children))
                {
                    foreach (var child in children)
                    {
                        deleteCount += await DeleteRecursive(child.DepartmentId);
                    }
                }

                // 删除当前部门
                deleteCount += await _deptInfoRepository.DeleteDepartmentInfo(deptId);
                return deleteCount;
            }

            // 判断是否可以删除
            if (!CanDelete(departmentId))
            {
                return 0; // 有人员，不允许删除
            }

            // 执行删除
            return await DeleteRecursive(departmentId);
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
                bool codeExist = await _deptInfoRepository.GetDepartCodeIsExist(long.Parse(deptUpsert.DepartmentId), deptUpsert.DepartmentCode);
                if (codeExist)
                {
                    return Result<int>.Failure(500, _localization.ReturnMsg($"{_this}DeptCodeExist"));
                }
                else
                {
                    DepartmentInfoEntity updateDept = new DepartmentInfoEntity()
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
                        Description = deptUpsert.Description,
                        ModifiedBy = _loginuser.UserId,
                        ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };
                    int updateDeptCount = await _deptInfoRepository.UpdateDepartmentInfo(updateDept);
                    await _db.CommitTranAsync();

                    return updateDeptCount >= 1
                            ? Result<int>.Ok(updateDeptCount, _localization.ReturnMsg($"{_this}UpdateSuccess"))
                            : Result<int>.Failure(500, _localization.ReturnMsg($"{_this}UpdateFailed"));
                }
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
                var deptEntity = await _deptInfoRepository.GetDepartmentInfoEntity(long.Parse(getDeptEntity.DepartmentId));
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
                var deptTree = await _deptInfoRepository.GetDepartmentInfoTree(getDeptTree);
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
                var deptDrop = await _deptInfoRepository.GetDepartmentDropDown();
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
                var deptLevelDrop = await _deptInfoRepository.GetDepartmentLevelDropDown();
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
