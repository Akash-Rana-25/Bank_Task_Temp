using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;
using Moq;

namespace BankManagement.UnitTests
{

    public class BankAccountServiceTests
    {
        [Fact]
        public async Task GetAllBankAccountsAsync_ReturnsBankAccounts()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BankAccount>>();
            var bankAccounts = new List<BankAccount>
        {
            new BankAccount { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
            new BankAccount { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
        };
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bankAccounts);

            var service = new BankAccountService(null, mockRepository.Object);

            // Act
            var result = await service.GetAllBankAccountsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateBankAccountAsync_CreatesNewBankAccount()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<BankAccount>>();
            var newBankAccount = new BankAccount { Id = Guid.NewGuid(), FirstName = "New", LastName = "Account" };

            var service = new BankAccountService(null, mockRepository.Object);

            // Act
            await service.CreateBankAccountAsync(newBankAccount);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(newBankAccount), Times.Once);
        }

        [Fact]
        public async Task UpdateBankAccountAsync_UpdatesExistingBankAccount()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var updatedBankAccount = new BankAccount { Id = accountId, FirstName = "Updated", LastName = "Account" };

            var mockRepository = new Mock<IRepository<BankAccount>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(accountId)).ReturnsAsync(new BankAccount());

            var service = new BankAccountService(null, mockRepository.Object);

            // Act
            await service.UpdateBankAccountAsync(accountId, updatedBankAccount);

            // Assert
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<BankAccount>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBankAccountAsync_DeletesExistingBankAccount()
        {
            // Arrange
            var accountId = Guid.NewGuid();

            var mockRepository = new Mock<IRepository<BankAccount>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(accountId)).ReturnsAsync(new BankAccount());

            var service = new BankAccountService(null, mockRepository.Object);

            // Act
            await service.DeleteBankAccountAsync(accountId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<BankAccount>()), Times.Once);
        }

      

        [Fact]
        public async Task SaveChangesAsync_ThrowsException_WhenSaveFails()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.SaveAsync()).ThrowsAsync(new Exception("Database save failed"));

            var service = new BankAccountService(mockUnitOfWork.Object, null);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => service.SaveChangesAsync());
        }
    }
}
