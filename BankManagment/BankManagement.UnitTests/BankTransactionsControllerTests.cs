using AutoMapper;
using BankManagment_DTO;
using BankManagment_Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using Xunit;

namespace YourProject.Tests
{
    public class BankTransactionsControllerTests
    {
        [Fact]
        public async Task GetBankTransactions_ReturnsOkResultWithBankTransactions()
        {
            // Arrange
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new BankTransactionsController(mockBankTransactionService.Object, mockMapper.Object);

            var bankTransactions = new List<BankTransaction> { new BankTransaction { Id = Guid.NewGuid(), TransactionPersonLastName = "Transaction 1" } };
            var bankTransactionDTOs = new List<BankTransactionDTO> { new BankTransactionDTO { Id = Guid.NewGuid(), TransactionPersonLastName = "Transaction 1" } };

            mockBankTransactionService.Setup(service => service.GetAllBankTransactionsAsync()).ReturnsAsync(bankTransactions);
            mockMapper.Setup(mapper => mapper.Map<List<BankTransactionDTO>>(bankTransactions)).Returns(bankTransactionDTOs);

            // Act
            var result = await controller.GetBankTransactions() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Same(bankTransactionDTOs, result.Value);
        }
        [Fact]
        public async Task UpdateBankTransaction_ReturnsNoContentWhenModelStateIsValid()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new BankTransactionsController(mockBankTransactionService.Object, mockMapper.Object);

            var updatedTransactionDTO = new BankTransactionDTO { Id = transactionId, TransactionPersonLastName = "Updated Transaction" };
            var updatedTransaction = new BankTransaction { Id = transactionId, TransactionPersonLastName = "Updated Transaction" };

            mockMapper.Setup(mapper => mapper.Map<BankTransaction>(updatedTransactionDTO)).Returns(updatedTransaction);
            mockBankTransactionService.Setup(service => service.UpdateBankTransactionAsync(transactionId, updatedTransaction)).Returns(Task.CompletedTask);
            mockBankTransactionService.Setup(service => service.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.UpdateBankTransaction(transactionId, updatedTransactionDTO) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteBankTransaction_ReturnsNoContent()
        {
            // Arrange
            var transactionId = Guid.NewGuid();
            var mockBankTransactionService = new Mock<IBankTransactionService>();
            var controller = new BankTransactionsController(mockBankTransactionService.Object, null);

            mockBankTransactionService.Setup(service => service.DeleteBankTransactionAsync(transactionId)).Returns(Task.CompletedTask);
            mockBankTransactionService.Setup(service => service.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteBankTransaction(transactionId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task CreateBankTransaction_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new BankTransactionsController(null, null);
            controller.ModelState.AddModelError("TransactionPersonLastName", "TransactionPersonLastName is required");

            // Act
            var result = await controller.CreateBankTransaction(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateBankTransaction_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new BankTransactionsController(null, null);
            controller.ModelState.AddModelError("TransactionPersonLastName", "TransactionPersonLastName is required");

            // Act
            var result = await controller.UpdateBankTransaction(Guid.NewGuid(), null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
