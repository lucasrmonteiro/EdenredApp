using EdenredApp.Domain.Entity;

namespace EdenredApp.Persistance.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetBeneficiariesByUserIdAsync(long userId);
}