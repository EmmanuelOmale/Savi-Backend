namespace Savi.Data.Domains
{
	public class CardDetail : BaseEntity
	{
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public string NameOnCard { get; set; }
		public string CardNumber { get; set; }
		public string Expiry { get; set; }
		public string CVV { get; set; }
	}
}