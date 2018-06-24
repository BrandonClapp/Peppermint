using Microsoft.AspNetCore.Authorization;
using Peppermint.Core.Services;
using System.Threading.Tasks;

namespace Peppermint.App.Controllers.Authorization
{
    public class CanPerformActionHandler : AuthorizationHandler<CanPerformAction>
    {
        UserFactory _userFactory;
        AuthorizationService _authorization;

        public CanPerformActionHandler(UserFactory userFactory, AuthorizationService authorization)
        {
            _userFactory = userFactory;
            _authorization = authorization;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CanPerformAction requirement)
        {
            var user = await _userFactory.GetCurrentUser();
            var canPerformAction = await _authorization.CanPerformAction(user?.Id, requirement.Permission);

            if (canPerformAction)
            {
                context.Succeed(requirement);
            }
        }
    }
}
