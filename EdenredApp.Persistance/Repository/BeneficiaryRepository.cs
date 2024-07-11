using EdenredApp.Domain.Entity;
using EdenredApp.Persistance.Context;
using EdenredApp.Persistance.Interfaces;

namespace EdenredApp.Persistance.Repository;

public class BeneficiaryRepository : BaseRepository<Beneficiary>, IBeneficiaryRepository
{
    private readonly EdenredAppContext _context;
    public BeneficiaryRepository(EdenredAppContext context) : base(context)
    {
        _context = context;
    }
}