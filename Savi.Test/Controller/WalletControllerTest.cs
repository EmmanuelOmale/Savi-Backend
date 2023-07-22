using Microsoft.AspNetCore.Mvc;
using Moq;
using Savi.Api.Controllers;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Test.Controller
{
    public class WalletControllerTest
    {
        [Fact]
        public async Task VerifyPayment_ValidReference_ReturnsOkResult()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object);
            var paymentReference = "T470565688084910";

            var expectedResult = new PayStackResponseDto
            {

                Status = true,
                Message = ("Payment Verified!"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.VerifyPaymentAsync(paymentReference))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.VerifyPayment(paymentReference);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(okResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }

        [Fact]
        public async Task WithdrawFund_ValidAmount_ReturnsOkResult()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object);
            var newWalletfund = new WalletFundingDto()
            {
                Amount = 5000,
                WalletId = "8136582045"

            };
            var expectedResult = new PayStackResponseDto
            {

                Status = true,
                Message = ("Fund Debited Successully!"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(newWalletfund))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(newWalletfund);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(okResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
        [Fact]
        public async Task WithdrawFund_InvalidAmount__ReturnsBadRequest()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object);
            var newWalletfund = new WalletFundingDto()
            {
                Amount = 5000000000,
                WalletId = "8136582045"

            };
            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Insufficient balance"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(newWalletfund))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(newWalletfund);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(badResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
        [Fact]
        public async Task WithdrawFund_InvalidAmountInput__ReturnsBadRequest()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object);
            var newWalletfund = new WalletFundingDto()
            {
                Amount = -0,
                WalletId = "8136582045"

            };
            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Invalid Credentials"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(newWalletfund))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(newWalletfund);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(badResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
        [Fact]
        public async Task WithdrawFund_InvalidWalletIdInput__ReturnsBadRequest()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object);
            var newWalletfund = new WalletFundingDto()
            {
                Amount = 500,
                WalletId = "ghjklsssss"

            };
            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Invalid Credentials"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(newWalletfund))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(newWalletfund);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(badResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
    }

}

