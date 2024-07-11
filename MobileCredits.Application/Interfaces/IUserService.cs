using MobileCredits.Domain.Entity;
using MobileCredits.Infra.Dto;

namespace MobileCredits.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<BeneficiaryDto>> GetBeneficiariesByUserId(long userId);
    Task<Beneficiary> AddCreditForBeneficiary(long userId, string nickName, double amount);
    RefillsAvailableDto GetRefillsAvailable();
}