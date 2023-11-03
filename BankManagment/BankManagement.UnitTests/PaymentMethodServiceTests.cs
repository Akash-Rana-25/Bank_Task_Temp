using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using Services;
using Moq;

namespace BankManagement.UnitTests
{

    public class PaymentMethodServiceTests
    {
        [Fact]
        public async Task GetAllPaymentMethodsAsync_ReturnsPaymentMethods()
        {
            // Arrange
            var mockPaymentMethodRepository = new Mock<IRepository<PaymentMethod>>();
            var paymentMethods = new List<PaymentMethod>
        {
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Cash" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Credit Card" }
        };
            mockPaymentMethodRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(paymentMethods);

            var service = new PaymentMethodService(null, mockPaymentMethodRepository.Object);

            // Act
            var result = await service.GetAllPaymentMethodsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreatePaymentMethodAsync_CreatesNewPaymentMethod()
        {
            // Arrange
            var mockPaymentMethodRepository = new Mock<IRepository<PaymentMethod>>();
            var newPaymentMethod = new PaymentMethod { Id = Guid.NewGuid(), Name = "New Payment Method" };

            mockPaymentMethodRepository.Setup(repo => repo.AddAsync(newPaymentMethod)).Returns(Task.CompletedTask);

            var service = new PaymentMethodService(null, mockPaymentMethodRepository.Object);

            // Act
            await service.CreatePaymentMethodAsync(newPaymentMethod);

            // Assert
            mockPaymentMethodRepository.Verify(repo => repo.AddAsync(newPaymentMethod), Times.Once);
        }

        [Fact]
        public async Task UpdatePaymentMethodAsync_UpdatesExistingPaymentMethod()
        {
            // Arrange
            var paymentMethodId = Guid.NewGuid();
            var updatedPaymentMethod = new PaymentMethod { Id = paymentMethodId, Name = "Updated Payment Method" };

            var mockPaymentMethodRepository = new Mock<IRepository<PaymentMethod>>();
            mockPaymentMethodRepository.Setup(repo => repo.GetByIdAsync(paymentMethodId))
                .ReturnsAsync(new PaymentMethod());

            var service = new PaymentMethodService(null, mockPaymentMethodRepository.Object);

            // Act
            await service.UpdatePaymentMethodAsync(paymentMethodId, updatedPaymentMethod);

            // Assert
            mockPaymentMethodRepository.Verify(repo => repo.UpdateAsync(It.IsAny<PaymentMethod>()), Times.Once);
        }

        [Fact]
        public async Task DeletePaymentMethodAsync_DeletesExistingPaymentMethod()
        {
            // Arrange
            var paymentMethodId = Guid.NewGuid();

            var mockPaymentMethodRepository = new Mock<IRepository<PaymentMethod>>();
            mockPaymentMethodRepository.Setup(repo => repo.GetByIdAsync(paymentMethodId)).ReturnsAsync(new PaymentMethod());

            var service = new PaymentMethodService(null, mockPaymentMethodRepository.Object);

            // Act
            await service.DeletePaymentMethodAsync(paymentMethodId);

            // Assert
            mockPaymentMethodRepository.Verify(repo => repo.DeleteAsync(It.IsAny<PaymentMethod>()), Times.Once);
        }


     
    }

}
