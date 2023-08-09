namespace Savi.Data.Domains
{
    public class Occupation : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
