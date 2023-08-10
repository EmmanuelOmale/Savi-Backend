using Savi.Data.Enums;

namespace Savi.Data.Domains
{
    public class KYC
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Occupations Occupation { get; set; }
        public string Address { get; set; }
        public string BVN { get; set; }
        public IdentificationType IdentityType { get; set; }
        public string? DocumentImageUrl { get; set; } = string.Empty;
        public string? ProofOfAddress { get; set; } = string.Empty;
        public ApplicationUser User { get; set; }
    }
}