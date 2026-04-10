using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Security;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Commands;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Dto;
using SystemAdmin.Model.FormBusiness.FormWorkflow.Entity;
using SystemAdmin.Repository.FormBusiness.FormWorkflow;

namespace SystemAdmin.Service.FormBusiness.FormWorkflow
{
    public class FormApprovalLimitService
    {
        private readonly CurrentUser _loginuser;
        private readonly ILogger<FormApprovalLimitService> _logger;
        private readonly SqlSugarScope _db;
        private readonly FormApprovalLimitRepository _formApprovalLimitRepository;
        private readonly LocalizationService _localization;
        private readonly string _this = "FormBusiness.FormWorkflow.FormApprovalLimit";

        public FormApprovalLimitService(CurrentUser loginuser, ILogger<FormApprovalLimitService> logger, SqlSugarScope db, FormApprovalLimitRepository formApprovalLimitRepository, LocalizationService localization)
        {
            _loginuser = loginuser;
            _logger = logger;
            _db = db;
            _formApprovalLimitRepository = formApprovalLimitRepository;
            _localization = localization;
        }

        /// <summary>
        /// 表单组别下拉
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<FormGroupDropDto>>> GetFormGroupDropDown()
        {
            try
            {
                var drop = await _formApprovalLimitRepository.GetFormGroupDropDown();
                return Result<List<FormGroupDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormGroupDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 表单类别下拉
        /// </summary>
        /// <param name="formGroupId"></param>
        /// <returns></returns>
        public async Task<Result<List<FormTypeDropDto>>> GetFormTypeDropDown(string formGroupId)
        {
            try
            {
                var drop = await _formApprovalLimitRepository.GetFormTypeDropDown(long.Parse(formGroupId));
                return Result<List<FormTypeDropDto>>.Ok(drop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<FormTypeDropDto>>.Failure(500, ex.Message);
            }
        }

        /// <summary>
        /// 新增职级签至最大范围
        /// </summary>
        /// <param name="upsert"></param>
        /// <returns></returns>
        public async Task<Result<int>> InsertFormApprovalLimit(FormApprovalLimitUpsert upsert)
        {
            try
            {
                var entity = new FormApprovalLimitEntity()
                {
                    FormTypeId = upsert.FormTypeId,
                    PositionId = upsert.PositionId,
                    MaxPositionId = upsert.MaxPositionId,
                };

                await _db.BeginTranAsync();
                var count = await _formApprovalLimitRepository.InsertFormApprovalLimit(entity);
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
    }
}
