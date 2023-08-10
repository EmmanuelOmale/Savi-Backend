namespace Savi.Data.IRepositories
{
	public interface IUnitOfWork
	{
		//IIdentityTypeRepository IdentityTypeRepository { get; }
		//IOccupationRepository OccupationRepository { get; }
		IUserRepository UserRepository { get; }
        IKycRepository KycRepository { get; }

        void Save();
	}
}