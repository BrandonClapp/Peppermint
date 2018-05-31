using Peppermint.Core.Data;
using Peppermint.Core.Entities;

namespace Peppermint.Core.Services
{
    public class UserGroupPermissionService : EntityService
    {
        private IDataAccessor<UserGroupPermissionEnitity> _ugPermissionData;

        public UserGroupPermissionService(
            IDataAccessor<UserGroupPermissionEnitity> ugPermissionData)
        {
            _ugPermissionData = ugPermissionData;
        }

        
    }
}
