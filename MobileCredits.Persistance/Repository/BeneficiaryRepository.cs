using MobileCredits.Domain.Entity;
using MobileCredits.Persistance.Context;
using MobileCredits.Persistance.Interfaces;

namespace MobileCredits.Persistance.Repository;

public class BeneficiaryRepository : BaseRepository<Beneficiary>, IBeneficiaryRepository
{
    private readonly EdenredAppContext _context;
    public BeneficiaryRepository(EdenredAppContext context) : base(context)
    {
        _context = context;
    }
}