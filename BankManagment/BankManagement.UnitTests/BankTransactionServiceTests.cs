using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using Services;
using Moq;
namespace BankManagement.UnitTests
{

    public class BankTransactionServiceTests
    {
        [Fact]
        public async Task GetAllBankTransactionsAsync_ReturnsBankTransactions()
        {
            // Arrange
            var mockBankTransactionRepository = new Mock<IRepository<BankTransaction>>();
            var bankTransactions = new List<BankTransaction>
        {
            new BankTransaction { Id = Guid.NewGuid(), TransactionType = "Credit", Amount = 100 },
            new BankTransaction { Id = Guid.NewGuid(), TransactionType = "Debit", Amount = 50 }
        };
            mockBankTransactionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bankTransactions);

            var service = new BankTransactionService(null, mockBankTransactionRepository.Object, null, null);

            // Act
            var result = await service.GetAllBankTransactionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateBankTransactionAsync_CreatesNewBankTransaction()
        {
            // Arrange
            var mockBankTransactionRepository = new Mock<IRepository<BankTransaction>>();
            var mockBankAccountPostingRepository = new Mock<IRepository<BankAccountPosting>>();
            var mockBankAccountRepository = new Mock<IRepository<BankAccount>>();

            var newBankTransaction = new BankTransaction
            {
                Id = Guid.NewGuid(),
                TransactionType = "Credit",
                Amount = 100,
                Category = "Bank Interest",
                PaymentMethodID = Guid.NewGuid(),
                BankAccountID = Guid.NewGuid()
            };

            mockBankTransactionRepository.Setup(repo => repo.AddAsync(newBankTransaction)).Returns(Task.CompletedTask);

            var existingBankAccount = new BankAccount
            {
                Id = newBankTransaction.BankAccountID,
                TotalBalance = 200
            };

            mockBankAccountRepository.Setup(repo => repo.GetByIdAsync(newBankTransaction.BankAccountID))
                .ReturnsAsync(existingBankAccount);

            var service = new BankTransactionService(null, mockBankTransactionRepository.Object, mockBankAccountPostingRepository.Object, mockBankAccountRepository.Object);

            // Act
            await service.CreateBankTransactionAsync(newBankTransaction);

            // Assert
            mockBankTransactionRepository.Verify(repo => repo.AddAsync(newBankTransaction), Times.Once);
            mockBankAccountRepository.Verify(repo => repo.UpdateAsync(existingBankAccount), Times.Once);
            Assert.Equal(300, existingBankAccount.TotalBalance);
        }

        [Fact]
        public async Task UpdateBankTransactionAsync_UpdatesExistingBankTransaction()
        {
            // Arrange
            var bankTransactionId = Guid.NewGuid();
            var updatedBankTransaction = new BankTransaction
            {
                Id = bankTransactionId,
                TransactionType = "Debit",
                Amount = 75
            };

            var mockBankTransactionRepository = new Mock<IRepository<BankTransaction>>();
            mockBankTransactionRepository.Setup(repo => repo.GetByIdAsync(bankTransactionId))
                .ReturnsAsync(new BankTransaction());

            var service = new BankTransactionService(null, mockBankTransactionRepository.Object, null, null);

            // Act
            await service.UpdateBankTransactionAsync(bankTransactionId, updatedBankTransaction);

            // Assert
            mockBankTransactionRepository.Verify(repo => repo.UpdateAsync(It.IsAny<BankTransaction>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBankTransactionAsync_DeletesExistingBankTransaction()
        {
            // Arrange
            var bankTransactionId = Guid.NewGuid();

            var mockBankTransactionRepository = new Mock<IRepository<BankTransaction>>();
            mockBankTransactionRepository.Setup(repo => repo.GetByIdAsync(bankTransactionId)).ReturnsAsync(new BankTransaction());

            var service = new BankTransactionService(null, mockBankTransactionRepository.Object, null, null);

            // Act
            await service.DeleteBankTransactionAsync(bankTransactionId);

            // Assert
            mockBankTransactionRepository.Verify(repo => repo.DeleteAsync(It.IsAny<BankTransaction>()), Times.Once);
        }
    }

}
