namespace Savi.Data.Domains
{
    public class GroupTransaction : BaseEntity
    {

        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}