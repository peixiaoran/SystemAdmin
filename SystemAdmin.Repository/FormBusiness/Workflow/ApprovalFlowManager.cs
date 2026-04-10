using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using SystemAdmin.CommonSetup.Security;

namespace SystemAdmin.Repository.FormBusiness.Workflow
{
    public class ApprovalFlowManager
    {
        private readonly CurrentUser _loginuser;
        private readonly SqlSugarScope _db;

        public ApprovalFlowManager(CurrentUser loginuser, SqlSugarScope db)
        {
            _loginuser = loginuser;
            _db = db;
        }

        public 
    }
}
