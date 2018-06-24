using Microsoft.AspNetCore.Authorization;
using Peppermint.Core.Authorization;

namespace Peppermint.App.Controllers.Authorization
{
    public class CanPerformAction : IAuthorizationRequirement
    {
        public PermissionType Permission;
        public CanPerformAction(PermissionType permission)
        {
            Permission = permission;
        }
    }
}
