using MobileCredits.Domain.Entity;

namespace MobileCredits.Persistance.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetBeneficiariesByUserIdAsync(long userId);
}