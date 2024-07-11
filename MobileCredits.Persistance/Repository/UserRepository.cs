using MobileCredits.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using MobileCredits.Persistance.Context;
using MobileCredits.Persistance.Interfaces;

namespace MobileCredits.Persistance.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly EdenredAppContext _context;
    public UserRepository(EdenredAppContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<User?> GetBeneficiariesByUserIdAsync(long userId)
    {
        var data = await _context.Set<User>()
            .Include(u => u.Beneficiaries)
            .FirstOrDefaultAsync(u => u.Code == userId);
        return data;
    }
}