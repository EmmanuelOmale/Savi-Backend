using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Savi.Data.DTO
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

    }
}
