using SqlSugar;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormBasicInfo.Entity;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    /// <summary>
    /// 表单基础
    /// </summary>
    public class FormRepository
    {
        private readonly SqlSugarScope _db;

        public FormRepository(SqlSugarScope db)
        {
            _db = db;
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
        /// 查询表单类别
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> GetFormTypePrefix(long formTypeId)
        {
            return await _db.Queryable<FormTypeEntity>()
                            .Where(formtype => formtype.FormTypeId == formTypeId)
                            .Select(formtype => formtype.Prefix)
                            .FirstAsync();
        }

        ///// <summary>
        ///// 查询表单开始步骤
        ///// </summary>
        ///// <returns></returns>
        //public async Task<long> FormStartStep(long formTypeId)
        //{
        //    return await _db.Queryable<FormTypeEntity>()
        //                    .Where(formtype => formtype.FormTypeId == formTypeId)
        //                    .Select(formtype => formtype.Prefix)
        //                    .FirstAsync();
        //}
    }
}
