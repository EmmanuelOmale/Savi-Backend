using Microsoft.AspNetCore.Mvc;
using Moq;
using Savi.Api.Controllers;
using Savi.Core.Interfaces;
using Savi.Data.DTO;

namespace Savi.Test.Controller
{
	public class AuthControllerTest
	{
		[Fact]
		public async Task ForgotPassword_ValidEmail_ReturnsOkResult()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var email = "test@example.com";

			var expectedResult = new APIResponse
			{
				IsSuccess = true,
				StatusCode = "200",
				Message = "Reset password URL has been sent to the email successfully!",
				Token = "validToken"
			};

			authenticationServiceMock.Setup(service => service.ForgotPasswordAsync(email))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ForgotPassword(email);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(okResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
			Assert.Equal(expectedResult.Message, actualResult.Message);
			Assert.Equal(expectedResult.Token, actualResult.Token);
		}

		[Fact]
		public async Task ForgotPassword_NullEmail_ReturnsBadRequest()
		{
			// Arrange
			var controller = new AuthController(Mock.Of<IAuthService>());

			// Act
			var result = await controller.ForgotPassword(null);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Email is required", badRequestResult.Value);
		}

		[Fact]
		public async Task ForgotPassword_EmptyEmail_ReturnsBadRequest()
		{
			// Arrange
			var controller = new AuthController(Mock.Of<IAuthService>());

			// Act
			var result = await controller.ForgotPassword("");

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Email is required", badRequestResult.Value);
		}

		[Fact]
		public async Task ForgotPassword_UserNotFound_ReturnsNotFound()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var email = "test@example.com";

			var expectedResult = new APIResponse
			{
				IsSuccess = false,
				StatusCode = "404",
				Message = "No user associated with the provided email"
			};

			authenticationServiceMock.Setup(service => service.ForgotPasswordAsync(email))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ForgotPassword(email);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(notFoundResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}

		[Fact]
		public async Task ForgotPassword_ServiceReturnsBadRequest_ReturnsBadRequest()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var email = "test@example.com";

			var expectedResult = new APIResponse
			{
				IsSuccess = false,
				StatusCode = "400",
				Message = "Some error occurred"
			};

			authenticationServiceMock.Setup(service => service.ForgotPasswordAsync(email))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ForgotPassword(email);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(badRequestResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}

		[Fact]
		public async Task ResetPassword_ValidModel_ReturnsOkResult()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var model = new ResetPasswordViewModel
			{
				Email = "test@example.com",
				Token = "validToken",
				NewPassword = "newPassword",
				ConfirmPassword = "newPassword"
			};

			var expectedResult = new APIResponse
			{
				IsSuccess = true,
				Message = "Password has been reset successfully!"
			};

			authenticationServiceMock.Setup(service => service.ResetPasswordAsync(model))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ResetPassword(model);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(okResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}

		[Fact]
		public async Task ResetPassword_InvalidModel_ReturnsBadRequest()
		{
			// Arrange
			var controller = new AuthController(Mock.Of<IAuthService>());
			controller.ModelState.AddModelError("Password", "The Password field is required.");

			// Act
			var result = await controller.ResetPassword(new ResetPasswordViewModel());

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Some properties are not valid", badRequestResult.Value);
		}

		[Fact]
		public async Task ResetPassword_UserNotFound_ReturnsBadRequest()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var model = new ResetPasswordViewModel
			{
				Email = "test@example.com",
				Token = "validToken",
				NewPassword = "newPassword",
				ConfirmPassword = "newPassword"
			};

			var expectedResult = new APIResponse
			{
				IsSuccess = false,
				Message = "No user associated with email"
			};

			authenticationServiceMock.Setup(service => service.ResetPasswordAsync(model))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ResetPassword(model);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(badRequestResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}

		[Fact]
		public async Task ResetPassword_PasswordMismatch_ReturnsBadRequest()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var model = new ResetPasswordViewModel
			{
				Email = "test@example.com",
				Token = "validToken",
				NewPassword = "newPassword",
				ConfirmPassword = "differentPassword"
			};

			var expectedResult = new APIResponse
			{
				IsSuccess = false,
				Message = "Password doesn't match its confirmation"
			};

			authenticationServiceMock.Setup(service => service.ResetPasswordAsync(model))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ResetPassword(model);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(badRequestResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}

		[Fact]
		public async Task ResetPassword_ServiceReturnsError_ReturnsBadRequest()
		{
			// Arrange
			var authenticationServiceMock = new Mock<IAuthService>();
			var controller = new AuthController(authenticationServiceMock.Object);
			var model = new ResetPasswordViewModel
			{
				Email = "test@example.com",
				Token = "validToken",
				NewPassword = "newPassword",
				ConfirmPassword = "newPassword"
			};

			var expectedResult = new APIResponse
			{
				IsSuccess = false,
				Message = "Something went wrong"
			};

			authenticationServiceMock.Setup(service => service.ResetPasswordAsync(model))
				.ReturnsAsync(expectedResult);

			// Act
			var result = await controller.ResetPassword(model);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var actualResult = Assert.IsAssignableFrom<APIResponse>(badRequestResult.Value);

			Assert.Equal(expectedResult.IsSuccess, actualResult.IsSuccess);
			Assert.Equal(expectedResult.Message, actualResult.Message);
		}
	}
}