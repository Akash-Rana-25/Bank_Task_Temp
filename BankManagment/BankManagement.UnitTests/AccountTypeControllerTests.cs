using AutoMapper;
using BankManagment_DTO;
using BankManagment_Domain.Entity;
using Moq;
using Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace YourProject.Tests
{
    public class AccountTypeControllerTests
    {
        [Fact]
        public async Task GetAccountTypes_ReturnsOkResultWithAccountTypes()
        {
            // Arrange
            var mockAccountTypeService = new Mock<IAccountTypeService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new AccountTypeController(mockAccountTypeService.Object, mockMapper.Object);

            var accountTypes = new List<AccountType> { new AccountType { Id = Guid.NewGuid(), Name = "Type 1" } };
            var accountTypeDTOs = new List<AccountTypeDTO> { new AccountTypeDTO { Id = Guid.NewGuid(), Name = "Type 1" } };

            mockAccountTypeService.Setup(service => service.GetAllAccountTypesAsync()).ReturnsAsync(accountTypes);
            mockMapper.Setup(mapper => mapper.Map<List<AccountTypeDTO>>(accountTypes)).Returns(accountTypeDTOs);

            // Act
            var result = await controller.GetAccountTypes() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Same(accountTypeDTOs, result.Value);
        }


        [Fact]
        public async Task UpdateAccountType_ReturnsNoContentWhenModelStateIsValid()
        {
            // Arrange
            var accountTypeId = Guid.NewGuid();
            var mockAccountTypeService = new Mock<IAccountTypeService>();
            var mockMapper = new Mock<IMapper>();
            var controller = new AccountTypeController(mockAccountTypeService.Object, mockMapper.Object);

            var updatedAccountTypeDTO = new AccountTypeDTO { Id = accountTypeId, Name = "Updated Type" };
            var updatedAccountType = new AccountType { Id = accountTypeId, Name = "Updated Type" };

            mockMapper.Setup(mapper => mapper.Map<AccountType>(updatedAccountTypeDTO)).Returns(updatedAccountType);
            mockAccountTypeService.Setup(service => service.UpdateAccountTypeAsync(accountTypeId, updatedAccountType)).Returns(Task.CompletedTask);
            mockAccountTypeService.Setup(service => service.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.UpdateAccountType(accountTypeId, updatedAccountTypeDTO) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteAccountType_ReturnsNoContent()
        {
            // Arrange
            var accountTypeId = Guid.NewGuid();
            var mockAccountTypeService = new Mock<IAccountTypeService>();
            var controller = new AccountTypeController(mockAccountTypeService.Object, null);

            mockAccountTypeService.Setup(service => service.DeleteAccountTypeAsync(accountTypeId)).Returns(Task.CompletedTask);
            mockAccountTypeService.Setup(service => service.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteAccountType(accountTypeId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task CreateAccountType_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AccountTypeController(null, null);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await controller.CreateAccountType(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateAccountType_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var controller = new AccountTypeController(null, null);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await controller.UpdateAccountType(Guid.NewGuid(), null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

    }
}
