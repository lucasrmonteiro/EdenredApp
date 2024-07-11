using MobileCredits.Infra.Dto;
using MobileCredits.Persistance.Interfaces;
using MobileCredits.Application.Interfaces;
using MobileCredits.Domain.Entity;

namespace MobileCredits.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBeneficiaryRepository _beneficiaryRepository;
    public UserService(IUserRepository userRepository ,IBeneficiaryRepository beneficiaryRepository)
    {
        _userRepository = userRepository;
        _beneficiaryRepository = beneficiaryRepository;
    }
    
    public async Task<IEnumerable<BeneficiaryDto>> GetBeneficiariesByUserId(long userId)
    {
       var user = await _userRepository.GetBeneficiariesByUserIdAsync(userId);
       
       if (user == null)
           throw new ArgumentException("User not found");

       var listBeneficiaries = user.Beneficiaries?
           .Select(b => new BeneficiaryDto(b.NickName , b.AEDBalance));

       return listBeneficiaries;
    }
    
    public async Task<Beneficiary> AddCreditForBeneficiary(long userId, string nickName, double amount)
    {
        var user = await _userRepository.GetBeneficiariesByUserIdAsync(userId);
        
        if (user == null)
            throw new ArgumentException("User not found");
        
        if(!user.CanAddCreditsLimit())
            throw new ArgumentException("User can't add more credits");
        
        var beneficiary = user.Beneficiaries?.SingleOrDefault(b => b.NickName == nickName);
        
        if (beneficiary == null)
            throw new ArgumentException("Beneficiary not found");
        
        if(beneficiary.AEDBalance > 0 && beneficiary.AEDBalance >= amount)
            throw new ArgumentException("Beneficiary can't add more credits");
        
        beneficiary.BalanceAed.Add(new BalanceAED()
        {
            BeneficiaryId = beneficiary.Id,
            Beneficiary = beneficiary,
            Amount = amount - 1
        });
        
         await _beneficiaryRepository.UpdateAsync(beneficiary);

         return beneficiary;
    }

    public RefillsAvailableDto GetRefillsAvailable() 
        => new RefillsAvailableDto();
}