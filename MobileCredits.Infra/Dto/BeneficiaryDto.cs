namespace MobileCredits.Infra.Dto;

public class BeneficiaryDto(string nickname, double balance)
{
    public string Nickname { get; private set; } = nickname;
    public double Balance { get; private set; } = balance;
}