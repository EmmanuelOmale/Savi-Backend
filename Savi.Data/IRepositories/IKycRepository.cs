using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Data.IRepositories
{
    public interface IKycRepository : IRepositoryBase<KYC>
    {
        void Update(KYC kyc);
        IEnumerable<string> GetIdentityTypes();
        IEnumerable<string> GetOccupations();
        IEnumerable<ApplicationUser> GetUsersByOccupation(Occupations occupation);
        IEnumerable<ApplicationUser> GetUsersByIdentityType(IdentificationType identityType);
    }
}
