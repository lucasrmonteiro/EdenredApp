using MobileCredits.Domain.Validation;

namespace MobileCredits.Domain.Entity;

public class User : BaseEntity
{
    public long Code { get; set; }
    public string Name { get; set; }
    public bool Verified { get; set; }
    [MaxCollectionSize(5)]
    public ICollection<Beneficiary>? Beneficiaries { get; set; }
    public bool CanAddCreditsLimit() => Beneficiaries != null && Beneficiaries
        .Where(it => it.CreatedDate.Date.Month == DateTime.Now.Month)
        .Sum(it => it.AEDBalance) < (Verified ? 1000 : 500);
}