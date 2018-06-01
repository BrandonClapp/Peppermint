using Peppermint.Core.Services;
using System;
using System.Threading.Tasks;

namespace Peppermint.Forum.Authorization
{
    public class CategoryAuthorizationService : EntityService
    {
        private UserGroupPermissionService _ugPermissionService;

        public CategoryAuthorizationService(UserGroupPermissionService ugPermissionService)
        {
            _ugPermissionService = ugPermissionService;
        }
        // needs to check the current context user (or have it passed in )

        public async Task<bool> CanCreateCategory()
        {
            var ugCan = await _ugPermissionService.CanPerformAction(CategoryPermission.CanCreateCategory);

            if (ugCan)
                return true;

            // do role next

            return false;
        }

        public async Task<bool> CanEditCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanDeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanViewCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanViewPostInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanCreatePostInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanEditPostInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanDeletePostInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanViewThreadInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanCreateThreadInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanEditThreadInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CanDeleteThreadInCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
