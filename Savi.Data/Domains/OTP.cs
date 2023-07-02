namespace Savi.Data.Domains
{
    public class OTP : BaseEntity
    {

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Value { get; set; }
        //public DateTime CreatedAt { get; set; }
        public bool IsUsed { get; set; }
    }
}