using EdenredApp.Domain.Entity;
using EdenredApp.Infra.Dto;

namespace EdenredApp.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<BeneficiaryDto>> GetBeneficiariesByUserId(long userId);
    Task<Beneficiary> AddCreditForBeneficiary(long userId, string nickName, double amount);
    RefillsAvailableDto GetRefillsAvailable();
}