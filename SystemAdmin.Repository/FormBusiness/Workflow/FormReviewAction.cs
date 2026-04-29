using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Options;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class FormReviewAction
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;
        private readonly LocalizationService _localization;
        private readonly Language _lang;

        public FormReviewAction(CurrentUser loginuser, SqlSugarScope db, LocalizationService localization, Language lang)
        {
            _loginuser = loginuser;
            _db = db;
            _localization = localization;
            _lang = lang;
        }
    }
}
