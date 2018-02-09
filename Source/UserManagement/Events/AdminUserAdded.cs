using System;
using Concepts;
using doLittle.Events;

namespace Events
{
    public class AdminUserAdded : IEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public bool MobilePhoneNumberConfirmed { get; set; }
    }
}
