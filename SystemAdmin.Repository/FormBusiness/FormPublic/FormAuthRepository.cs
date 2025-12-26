using SqlSugar;
using SystemAdmin.Model.FormBusiness.Enum;
using SystemAdmin.Model.SystemBasicMgmt.UserSettings.Entity;

namespace SystemAdmin.Repository.FormBusiness.Enum
{
    public class FormAuthRepository
    {
        private readonly SqlSugarScope _db;

        public FormAuthRepository(SqlSugarScope db)
        {
            _db = db;
        }

        /// <summary>
        /// 验证员工是否有权限申请、审批、查看、作废表单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="formTypeId"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public async Task<bool> HasUserApplyFormType(long userId, long formTypeId, FormOp op)
        {
            if (op.HasFlag(FormOp.Apply))
            {
                return await _db.Queryable<UserFormBindEntity>()
                                .With(SqlWith.NoLock)
                                .Where(userform => userform.UserId == userId && userform.FormGroupTypeId == formTypeId)
                                .AnyAsync();
            }
            else
            {
                return true;
            }
        }
    }
}
