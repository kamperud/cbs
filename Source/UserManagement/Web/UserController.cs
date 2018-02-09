/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Domain;
using Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using doLittle.Events;
using Read;
using Infrastructure.AspNet;
using Newtonsoft.Json.Linq;
using Concepts;

namespace Web
{
    [Route("api/usermanagement/")]
    public class UserController : BaseController
    {
        readonly IUsers _users;
        readonly ILogger<UserController> _logger;

        public UserController(
            IUsers users,
            ILogger<UserController> logger)
        {
            _users = users;
            _logger = logger;
        }

        [HttpPost]
        public void Add([FromRoute] UserType type, [FromBody] JObject command)
        {
            var id = Guid.NewGuid();
            Apply(id, Deserialize(id, type, command));
        }

        public IEvent Deserialize(Guid id, UserType type, JObject command)
        {
            switch (type)
            {
                case UserType.Admin:
                    var user = command.ToObject<AddAdminUser>();
                    return new AdminUserAdded
                    {
                        Id = id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        MobilePhoneNumberConfirmed = user.MobilePhoneNumberConfirmed
                    };
            }
            return null;
        }

        // TODO, remove once frontend is updated
        [HttpPost("staffuser")]
        public void OldAdd([FromBody] AddStaffUser command)
        {
            var id = Guid.NewGuid();
            Apply(id, new StaffUserAdded
            {
                Id = id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Age = command.Age,
                Sex = command.Sex,
                Location = command.Location,
                NationalSociety = command.NationalSociety,
                PreferredLanguage = command.PreferredLanguage,
                MobilePhoneNumber = command.MobilePhoneNumber,
                Email = command.Email
            });
        }
        
        [HttpGet("staffusers")]
        public IEnumerable<StaffUser> GetAllStaffUsers()
        {
            Console.WriteLine("in staffusers");
            var users = _users.GetAllStaffUsers();
            return users;
        }        

        [HttpDelete("delete/{id}")]
        public void Delete(Guid id)
        {
            Apply(id, new UserDeleted
            {
                Id = id
            });
        }
    }
}