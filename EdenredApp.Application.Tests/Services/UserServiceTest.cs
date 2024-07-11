using EdenredApp.Application.Services;
using EdenredApp.Domain.Entity;
using EdenredApp.Persistance.Interfaces;
using Moq;

namespace EdenredApp.Application.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IBeneficiaryRepository> _beneficiaryRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _beneficiaryRepositoryMock = new Mock<IBeneficiaryRepository>();
        _userService = new UserService(_userRepositoryMock.Object, _beneficiaryRepositoryMock.Object);
    }

    [Fact]
    public async Task GetBeneficiariesByUserId_WithValidUserId_ReturnsBeneficiaries()
    {
        // Arrange
        var userId = 1L;
        var beneficiaries = new List<Beneficiary>
        {
            new Beneficiary { NickName = "JohnDoe", BalanceAed = new List<BalanceAED>()
            {
                new BalanceAED()
                {
                    Amount = 100
                }
            }}
        };
        _userRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserIdAsync(userId))
            .ReturnsAsync(new User { Beneficiaries = beneficiaries  });

        // Act
        var result = await _userService.GetBeneficiariesByUserId(userId);

        // Assert
        Assert.Single(result);
        Assert.Equal("JohnDoe", result.First().Nickname);
        Assert.Equal(100, result.First().Balance);
    }

    [Fact]
    public async Task GetBeneficiariesByUserId_WithInvalidUserId_ThrowsArgumentException()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserIdAsync(It.IsAny<long>()))
            .ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetBeneficiariesByUserId(999));
    }

    [Fact]
    public async Task AddCreditForBeneficiary_WithValidData_AddsCreditSuccessfully()
    {
        // Arrange
        var userId = 1L;
        var nickName = "JohnDoe";
        var amount = 50.0;
        var beneficiary = new Beneficiary { NickName = nickName,  BalanceAed = new List<BalanceAED>() };
        _userRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserIdAsync(userId))
            .ReturnsAsync(new User { Beneficiaries = new List<Beneficiary> { beneficiary } });

        // Act
        var result = await _userService.AddCreditForBeneficiary(userId, nickName, amount);

        // Assert
        Assert.Equal(amount - 1, result.BalanceAed.Last().Amount);
    }

    [Fact]
    public async Task AddCreditForBeneficiary_WithAmountExceedingLimit_ThrowsArgumentException()
    {
        // Arrange
        var userId = 1L;
        var nickName = "JohnDoe";
        var amount = 200.0; 
        var beneficiary = new Beneficiary { NickName = nickName, BalanceAed = new List<BalanceAED>()
        {
            new BalanceAED()
            {
                Amount = 1000,
                CreatedDate = DateTime.Now
            }
        } };
        _userRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserIdAsync(userId))
            .ReturnsAsync(new User { Beneficiaries = new List<Beneficiary> { beneficiary } , Verified = false });
        
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.AddCreditForBeneficiary(userId, nickName, amount));
    }
}