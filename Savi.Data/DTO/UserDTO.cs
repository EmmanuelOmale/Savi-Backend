﻿namespace Savi.Data.DTO
{
	public class UserDTO
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string PhoneNumber { get; set; }
		public string Email { get; set; } = string.Empty;
		public string ImageUrl { get; set; }
		public string WalletId { get; set; }
	}
}