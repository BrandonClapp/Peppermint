using Microsoft.AspNetCore.Mvc;
using Peppermint.Core.Entities;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Sample.Controllers
{
    [Route("api/[controller]")]
    public class UserGroupsController : Controller
    {
        private UserGroupService _userGroupsService;

        public UserGroupsController(UserGroupService userGroupsService)
        {
            _userGroupsService = userGroupsService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<dynamic>> GetGroups()
        {
            var groups = await _userGroupsService.GetAllGroups();
            var groupDetails = new List<dynamic>();

            foreach (var group in groups)
            {
                groupDetails.Add(new
                {
                    group.Id,
                    group.Name,
                    Users = await group.GetUsers()
                });
            }

            return groupDetails;
        }

        [HttpGet("{groupId}")]
        public async Task<dynamic> GetGroupo(int groupId)
        {
            var group = await _userGroupsService.GetGroup(groupId);

            if (group == null)
                return null;

            return new
            {
                group.Id,
                group.Name,
                Users = await group.GetUsers()
            };

        }
    }
}
