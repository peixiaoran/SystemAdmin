using Microsoft.Extensions.Logging;
using SqlSugar;
using SystemAdmin.Common.Enums.FormBusiness;
using SystemAdmin.Common.Utilities;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormAudit.Entity;
using SystemAdmin.Model.FormBusiness.FormOperate.Entity;
using SystemAdmin.Repository.FormBusiness.Workflow;

namespace SystemAdmin.Service.FormBusiness.Workflow
{
    public class FormService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormRepository _form;
        private readonly string _publicthis = "FormBusiness.Forms.";

        public FormService(CurrentUser loginuser, ILogger<FormService> logger, SqlSugarScope db, FormRepository form)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _form = form;
        }

        ///// <summary>
        ///// 初始化表单
        ///// </summary>
        ///// <param name="formTypeId"></param>
        ///// <returns></returns>
        //public async Task<Result<string>> InitializeForm(string formTypeId)
        //{
        //    try
        //    {
        //        var formNo = await GetFormAutoNo(formTypeId);
        //        var formInfo = new FormInfoEntity()
        //        {
        //            FormId = SnowFlakeSingle.Instance.NextId(),
        //            FormTypeId = long.Parse(formTypeId),
        //            FormNo = formNo,
        //            FormStatus = FormStatus.PendingSubmission.ToEnumString(),
        //            LastStepId = -1,
        //        };
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 查询最大表单类别表单号
        /// </summary>
        /// <param name="formTypeId"></param>
        /// <returns></returns>
        public async Task<string> GetFormAutoNo(string formTypeId)
        {
            try
            {
                // 查询表单类别最高计数
                var autoEntity = await _form.GetFormAutoNo(long.Parse(formTypeId), DateTime.Now.ToString("yyyyMM"));
                var prefix = await _form.GetFormTypePrefix(long.Parse(formTypeId));

                await _db.BeginTranAsync();
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
                    int count = await _form.InsertFormAutoNo(entity);
                    await _db.CommitTranAsync();

                    return $"{prefix}-{DateTime.Now:yyyyMM}{1:D4}";
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
                    int count = await _form.UpdateFormAutoNo(entity);
                    await _db.CommitTranAsync();

                    return $"{prefix}-{DateTime.Now:yyyyMM}{maxNo:D4}"; 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return "";
            }
        }
    }
}
