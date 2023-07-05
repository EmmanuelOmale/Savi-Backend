using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Savi.Data.DTO
{
    public class SignUpDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }

    }
}
