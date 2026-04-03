using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.X509;
using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    /// <summary>
    /// 表单基础
    /// </summary>
    public class FormRepository
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;

        public FormRepository(CurrentUser loginuser, SqlSugarScope db)
        {
            _loginuser = loginuser;
            _db = db;
        }

        /// <summary>
        /// 初始化表单
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<long> InitializeFormInstance(string formTypeId)
        {
            // 查询表单类别最高计数
            var autoEntity = await GetFormAutoNo(long.Parse(formTypeId), DateTime.Now.ToString("yyyyMM"));
            var prefix = await GetFormTypePrefix(long.Parse(formTypeId));
            var formNo = string.Empty;

            if (autoEntity == null)
            {
                var entity = new FormSequenceEntity()
                {
                    FormTypeId = long.Parse(formTypeId),
                    Ym = DateTime.Now.ToString("yyyyMM"),
                    Total = 1,
                    CreatedBy = _loginuser.UserId,
                    CreatedDate = DateTime.Now,
                };
                await InsertFormAutoNo(entity);
                formNo = $"{prefix}-{DateTime.Now:yyyyMM}{1:D4}";
            }
            else
            {
                var maxNo = $"{autoEntity.Total + 1:D4}";
                var entity = new FormSequenceEntity()
                {
                    FormTypeId = long.Parse(formTypeId),
                    Total = autoEntity.Total + 1,
                    Ym = DateTime.Now.ToString("yyyyMM"),
                    ModifiedBy = _loginuser.UserId,
                    ModifiedDate = DateTime.Now,
                };
                await UpdateFormAutoNo(entity);
                formNo = $"{prefix}-{DateTime.Now:yyyyMM}{maxNo:D4}";
            }

            var formId = SnowFlakeSingle.Instance.NextId();
            // 初始化表单实例
            var formInstance = new FormInstanceEntity()
            {
                FormId = formId,
                FormTypeId = long.Parse(formTypeId),
                FormNo = formNo,
                FormStatus = FormStatus.PendingSubmission.ToEnumString(),
                ApplicantUserId = _loginuser.UserId,
                BranchId = -1,
                CurrentStepId = -1,
                CreatedBy = _loginuser.UserId,
                CreatedDate = DateTime.Now,
            };

            var count = await InsertFormInstance(formInstance);
            await _db.CommitTranAsync();

            return formId;
        }

        /// <summary>
        /// 查询表单类被最新单号
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<FormSequenceEntity> GetFormAutoNo(long formTypeId, string Ym)
        {
            return await _db.Queryable<FormSequenceEntity>()
                            .With(SqlWith.NoLock)
                            .Where(sequen => sequen.FormTypeId == formTypeId && sequen.Ym == Ym)
                            .FirstAsync();
        }

        /// <summary>
        /// 新增表单类单号记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormAutoNo(FormSequenceEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 修改表单类单号记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateFormAutoNo(FormSequenceEntity entity)
        {
            return await _db.Updateable(entity)
                            .IgnoreColumns(sequen => new
                            {
                                sequen.FormTypeId,
                                sequen.CreatedBy,
                                sequen.CreatedDate,
                            }).Where(sequen => sequen.FormTypeId == entity.FormTypeId && sequen.Ym == entity.Ym)
                            .ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询表单前缀
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> GetFormTypePrefix(long formTypeId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .With(SqlWith.NoLock)
                            .Where(formtype => formtype.FormTypeId == formTypeId)
                            .Select(formtype => formtype.Prefix)
                            .FirstAsync();
        }

        /// <summary>
        /// 新增表单实例
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertFormInstance(FormInstanceEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }
    }
}
