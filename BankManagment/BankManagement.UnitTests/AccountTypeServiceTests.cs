using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;
using Moq;
using Services;
using Xunit;

namespace BankManagement.UnitTests
{
    public class AccountTypeServiceTests
    {
        [Fact]
        public async Task GetAllAccountTypesAsync_ReturnsAccountTypes()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<AccountType>>();
            var accountTypes = new List<AccountType>
            {
                new AccountType { Id = Guid.NewGuid(), Name = "Type 1" },
                new AccountType { Id = Guid.NewGuid(), Name = "Type 2" }
            };
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(accountTypes);

            var service = new AccountTypeService(null, mockRepository.Object);

            // Act
            var result = await service.GetAllAccountTypesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateAccountTypeAsync_CreatesNewAccountType()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<AccountType>>();
            var newAccountType = new AccountType { Id = Guid.NewGuid(), Name = "New Type" };

            var service = new AccountTypeService(null, mockRepository.Object);

            // Act
            await service.CreateAccountTypeAsync(newAccountType);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(newAccountType), Times.Once);
        }

        [Fact]
        public async Task UpdateAccountTypeAsync_UpdatesExistingAccountType()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var updatedAccountType = new AccountType { Id = accountId, Name = "Updated Type" };

            var mockRepository = new Mock<IRepository<AccountType>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(accountId)).ReturnsAsync(new AccountType());

            var service = new AccountTypeService(null, mockRepository.Object);

            // Act
            await service.UpdateAccountTypeAsync(accountId, updatedAccountType);

            // Assert
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<AccountType>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountTypeAsync_DeletesExistingAccountType()
        {
            // Arrange
            var accountId = Guid.NewGuid();

            var mockRepository = new Mock<IRepository<AccountType>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(accountId)).ReturnsAsync(new AccountType());

            var service = new AccountTypeService(null, mockRepository.Object);

            // Act
            await service.DeleteAccountTypeAsync(accountId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<AccountType>()), Times.Once);
        }

      

       

        [Fact]
        public async Task SaveChangesAsync_ThrowsException_WhenSaveFails()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.SaveAsync()).ThrowsAsync(new Exception("Database save failed"));

            var service = new AccountTypeService(mockUnitOfWork.Object, null);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.SaveChangesAsync());
            Assert.Equal("Database save failed", exception.Message);
        }

    }
}
