namespace EdenredApp.Domain.Entity;

public class BalanceAED : BaseEntity
{
    public Guid BeneficiaryId { get; set; }
    public Beneficiary Beneficiary { get; set; }
    public double Amount { get; set; }
}