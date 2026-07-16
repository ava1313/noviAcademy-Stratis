using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NoviCode;
using Xunit;

namespace WorldRank.tests.Entities;

public class WalletTests
{
    [Fact(DisplayName = "Constructor should initialize properties correctly with proper input")]
    public void Constructor_ProperInput_InitializedCorrectly()
    {
        // Arrange
        var id = new Guid();
        var playerId = new Guid();
        Currency currency = Currency.EUR;
        decimal balance = 100.0m;
        bool isBlocked = true;

        // Act
        var wallet = new Wallet(
            id,
            playerId,
            currency,
            balance,
            isBlocked
        );

        // Assert
        Assert.Equal(id, wallet.Id);
        Assert.Equal(playerId, wallet.PlayerId);
        Assert.Equal(currency, wallet.Currency);
        Assert.Equal(balance, wallet.Balance);
        Assert.True(wallet.IsBlocked);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Deposit_InvalidAmount_ThrowsInvalidAmountException(int amount)
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid playerId = Guid.NewGuid();
        Currency currency = Currency.EUR;

        var wallet = Wallet.CreateNew(id, playerId, currency);

        // Act
        Action action = () => wallet.Deposit(amount);

        // Assert
        Assert.Throws<InvalidAmountException>(action);
    }
}