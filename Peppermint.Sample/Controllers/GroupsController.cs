using Microsoft.AspNetCore.Mvc;
using Peppermint.Core.Services;
using Peppermint.Forum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Sample.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        private GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<dynamic>> GetGroups()
        {

            var groups = await _groupService.GetAllGroups();
            var groupDetails = new List<dynamic>();

            foreach (var group in groups)
            {
                //var canDoAction =
                //    await group.CanPerformAction<CategoryEntity, CategoryEntity>(ForumPermissions.CanEdit, 1);

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
        public async Task<dynamic> GetGroup(int groupId)
        {
            var group = await _groupService.GetGroup(groupId);

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
