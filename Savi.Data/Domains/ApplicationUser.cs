using Microsoft.AspNetCore.Identity;

namespace Savi.Data.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string BVN { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; }
        public string ImageUrl { get; set; }
        public string ProofOfAddressUrl { get; set; }
        public int KYCLevel { get; set; }
        public Saving Saving { get; set; }
        public OTP OTP { get; set; }

        // Navigation properties

        public ICollection<UserTransaction> UserTransactions { get; set; }
        public ICollection<GroupTransaction> GroupTransactions { get; set; }
        public ICollection<Saving> Savings { get; set; }
        public ICollection<CardDetail> CardDetails { get; set; }
        public ICollection<OTP> OTPs { get; set; }

    }
}
