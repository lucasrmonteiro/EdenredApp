using MobileCredits.Domain.Entity;

namespace MobileCredits.Domain.Tests.Entity;

public class BeneficiaryTest
{
    [Fact]
    public void AEDBalance_WithMultipleBalances_CalculatesCorrectSum()
    {
        var beneficiary = new Beneficiary
        {
            BalanceAed = new List<BalanceAED>
            {
                new BalanceAED { Amount = 100 },
                new BalanceAED { Amount = 200 },
                new BalanceAED { Amount = 300 }
            }
        };

        var result = beneficiary.AEDBalance;

        Assert.Equal(600, result);
    }

    [Fact]
    public void AEDBalance_WithNoBalances_ReturnsZero()
    {
        var beneficiary = new Beneficiary
        {
            BalanceAed = new List<BalanceAED>()
        };

        var result = beneficiary.AEDBalance;

        Assert.Equal(0, result);
    }

    [Fact]
    public void AEDBalance_WithNegativeBalances_CalculatesCorrectSum()
    {
        var beneficiary = new Beneficiary
        {
            BalanceAed = new List<BalanceAED>
            {
                new BalanceAED { Amount = -100 },
                new BalanceAED { Amount = 200 },
                new BalanceAED { Amount = -300 }
            }
        };

        var result = beneficiary.AEDBalance;

        Assert.Equal(-200, result);
    }

    [Fact]
    public void NickName_WithMaxLength_DoesNotThrowException()
    {
        var beneficiary = new Beneficiary
        {
            NickName = new string('a', 20)
        };

        var exception = Record.Exception(() => { var nickName = beneficiary.NickName; });

        Assert.Null(exception);
    }
}