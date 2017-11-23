using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Utilities.StringKeys
{
    public class PolicyKeys
    {
        public const string POLICY_DEFAULT_VALUE = "Allowed";

        #region Admin Related
        public const string ADMIN_MODULE_ACCESS = "AdminModuleAccess";
        public const string ROLE_CREATE_EDIT = "RoleCreateAndUpdate";
        public const string ROLE_DELETE = "RoleDelete";
        public const string USER_DELETE = "UserDelete";
        public const string USER_CREATE_EDIT = "UserCreateAndUpdate";
        #endregion
        

        #region Related To All
        public const string CREATE_EDIT = "CreateEdit";
        public const string DELETE = "Delete";
        public const string APPROVE = "Approve";
        #endregion
    }
}
