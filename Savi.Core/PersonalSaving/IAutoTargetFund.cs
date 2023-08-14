namespace Savi.Core.PersonalSaving
{
    public interface IAutoTargetFund
    {
        Task<bool> AutoTarget();
        Task<bool> AutoSavings(string TargetId);
    }
}
