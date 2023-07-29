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
            var controller = new WalletController(paymentServiceMock.Object, null);
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
            var controller = new WalletController(paymentServiceMock.Object, null);

            decimal Amount = 5000;
            string WalletId = "8136582045";

            var expectedResult = new PayStackResponseDto
            {

                Status = true,
                Message = ("Fund Debited Successully!"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(Amount, WalletId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(Amount, WalletId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(okResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
        [Fact]
        public async Task WithdrawFund_AmountLessThanZero__ReturnsBadRequest()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object, null);

            decimal Amount = -50;
            string WalletId = "8136582045";


            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Invalid Amount"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(Amount, WalletId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(Amount, WalletId);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(badResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
        [Fact]
        public async Task WithdrawFund_AmountGreaterThanBalance__ReturnsBadRequest()
        {
            // Arrange
            var paymentServiceMock = new Mock<IPaymentService>();
            var controller = new WalletController(paymentServiceMock.Object, null);

            decimal Amount = 5000;
            string WalletId = "8136582045";


            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Insufficient balance"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(Amount, WalletId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(Amount, WalletId);

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
            var controller = new WalletController(paymentServiceMock.Object, null);

            decimal Amount = 500;
            string WalletId = "ghjklsssss";
            ;
            var expectedResult = new PayStackResponseDto
            {

                Status = false,
                Message = ("Invalid Credentials"),
                Data = null
            };

            paymentServiceMock.Setup(service => service.WithdrawFundAsync(Amount, WalletId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.WithDrawFund(Amount, WalletId);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var actualResult = Assert.IsAssignableFrom<PayStackResponseDto>(badResult.Value);

            Assert.Equal(expectedResult.Status, actualResult.Status);
            Assert.Equal(expectedResult.Message, actualResult.Message);
            Assert.Equal(expectedResult.Data, actualResult.Data);

        }
    }

}

