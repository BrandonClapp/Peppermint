﻿using Peppermint.Core.Data;
using Peppermint.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peppermint.Core.Entities
{
    [DataLocation("core.Users")]
    public class User : DataEntity
    {
        private UserService _userService;
        private UserMembershipService _userMembershipService;

        public User(UserService userService, UserMembershipService userMembershipService)
        {
            _userService = userService;
            _userMembershipService = userMembershipService;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Excerpt { get; set; }
        public string TwitterHandle { get; set; }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _userMembershipService.GetGroupsForUser(Id);
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _userMembershipService.GetRolesForUser(Id);
        }
    }
}
