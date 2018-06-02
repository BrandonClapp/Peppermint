using Peppermint.Core.Services;
using System;
using System.Threading.Tasks;

namespace Peppermint.Forum.Authorization
{
    public class CategoryAuthorizationService : EntityService
    {
        private AuthorizationService _authorizationService;

        public CategoryAuthorizationService(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        // needs to check the current context user (or have it passed in )

        public async Task<bool> CanCreateCategory()
        {
            int userId = 3;
            var authorized = await _authorizationService.CanPerformAction(userId, CategoryPermission.CanCreateCategory);

            return authorized;
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
            return await Task.FromResult(true);
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
