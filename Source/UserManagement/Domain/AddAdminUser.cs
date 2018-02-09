using System;
using Concepts;

namespace Domain
{
    public class AddAdminUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool MobilePhoneNumberConfirmed { get; set; } = true;
    }
}
