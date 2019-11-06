using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Permission.Permissions;

namespace TestApp.Permission.Interfaces
{
    public interface IPermissionsChanged
    {
        void permissionsChanged(GeneralPermission[] changedPermissions);
    }
}
