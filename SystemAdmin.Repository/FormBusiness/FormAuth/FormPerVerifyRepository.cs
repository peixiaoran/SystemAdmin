using SqlSugar;
using SystemAdmin.Model.FormBusiness.FormAuth.FormPerVerify;
using SystemAdmin.Model.SystemBasicMgmt.SystemUserConfig.Entity;

namespace SystemAdmin.Repository.FormBusiness.FormAuth
{
    public class FormPerVerifyRepository
    {
        private readonly SqlSugarScope _db;

        public FormPerVerifyRepository(SqlSugarScope db)
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
