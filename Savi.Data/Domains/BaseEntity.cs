using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Savi.Data.Domains
{
	public class BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public string Id { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }

		public bool IsDeleted { get; set; }
	}
}