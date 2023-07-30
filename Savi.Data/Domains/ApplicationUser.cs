using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Savi.Data.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string OccupationId { get; set; }
        public string Address { get; set; }
        public string BVN { get; set; }
        public string IdentityTypeId { get; set; }
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
        [ForeignKey("OccupationId")]
        public Occupation Occupation { get; set; }
        [ForeignKey("IdentityTypeId")]
        public IdentityType IdentityType { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }

	}
}
