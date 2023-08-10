using Microsoft.EntityFrameworkCore;
using Savi.Data.Context;
using Savi.Data.Domains;
using Savi.Data.Enums;
using Savi.Data.IRepositories;

namespace Savi.Data.Repositories
{
    public class KycRepository : RepositoryBase<KYC>, IKycRepository
    {
        private SaviDbContext _saviDbContext;

        public KycRepository(SaviDbContext db) : base(db)
        {
            _saviDbContext = db;
        }

        public void Update(KYC kyc)
        {
            _saviDbContext.Entry(kyc).State = EntityState.Modified;
            _saviDbContext.SaveChanges();
        }

        public IEnumerable<string> GetIdentityTypes()
        {
            return Enum.GetValues(typeof(IdentificationType)).Cast<IdentificationType>()
                .Select(IdentificationType => IdentificationType.ToString()).ToList();
        }

        public IEnumerable<string> GetOccupations()
        {
            return Enum.GetValues(typeof(Occupations)).Cast<Occupations>()
                .Select(Occupations => Occupations.ToString()).ToList();
        }

        public IEnumerable<ApplicationUser> GetUsersByOccupation(Occupations occupation)
        {
            return _saviDbContext.Set<ApplicationUser>()
                .Where(u => u.Kyc.Occupation == occupation)
                .ToList();
        }

        public IEnumerable<ApplicationUser> GetUsersByIdentityType(IdentificationType identityType)
        {
            return _saviDbContext.Set<ApplicationUser>()
                .Where(u => u.Kyc.IdentityType == identityType)
                .ToList();
        }

    }
}
