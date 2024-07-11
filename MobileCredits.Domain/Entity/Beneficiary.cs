using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MobileCredits.Domain.Entity;

public class Beneficiary : BaseEntity
{
    [MaxLength(20)]
    public string NickName { get; set; }
    public ICollection<BalanceAED> BalanceAed { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public double AEDBalance => BalanceAed.Sum(it => it.Amount);
}