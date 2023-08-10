/*using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Savi.Api.Controllers;
using Savi.Api.Profiles;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;
using Savi.Data.IRepositories;
using System.Linq.Expressions;

namespace Savi.Test.Controller
{
    public class IdentityTypeControllerTest
	{
		private readonly IMapper _mapper;
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IDocumentUploadService> _uploadServiceMock;
		private readonly IdentityTypeController _controller;

		/// <summary>
		/// Test class for the IdentityTypeController.
		/// Initializes a new instance of the IdentityTypeControllerTest class.
		/// </summary>
		public IdentityTypeControllerTest()
		{
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MappingProfiles>();
			});
			_mapper = mapperConfig.CreateMapper();
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_uploadServiceMock = new Mock<IDocumentUploadService>();
			_controller = new IdentityTypeController(_unitOfWorkMock.Object, _mapper, _uploadServiceMock.Object);
		}

		/// <summary>
		/// Test method for the GetIdentityTypes action in the IdentityTypeController.
		/// It tests whether the action returns a list of identity types.
		/// </summary>
		[Fact]
		public void GetIdentityTypes_ReturnsIdentityTypes()
		{
			// Arrange
			var mockIdentityTypes = new List<IdentityType>
			{
				new IdentityType { Id = "1", Name = "Identity Type 1" },
				new IdentityType { Id = "2", Name = "Identity Type 2" }
			};

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.GetAll(It.IsAny<Expression<Func<IdentityType, bool>>>(), null))
				.Returns(mockIdentityTypes);

			// Act
			var result = _controller.GetIdentityTypes();

			// Assert
			var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(okObjectResult.Value);

			Assert.True(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status200OK.ToString(), apiResponse.StatusCode);
			Assert.Equal("Identity types retrieved successfully", apiResponse.Message);
			Assert.Equal(mockIdentityTypes, apiResponse.Result);
		}

		/// <summary>
		/// Test method for the AddNewIdentityType action in the IdentityTypeController,
		/// when a document image is provided. It tests whether the action returns a
		/// CreatedAtActionResult with the appropriate APIResponse.
		/// </summary>
		/// <returns>Task representing the asynchronous operation.</returns>
		[Fact]
		public async Task AddNewIdentityType_WithDocumentImage_ReturnsCreatedResponse()
		{
			// Arrange
			var mockNewIdentityTypeDto = new CreateIdentityDto
			{
				Name = "Test Identity Type",
				DocumentImage = new FormFile(null, 0, 0, "TestImage", "test.jpg")
			};

			var mockDocumentUploadResult = new DocumentUploadResult
			{
				Url = "https://example.com/test.jpg"
			};

			_uploadServiceMock
				.Setup(u => u.UploadImageAsync(It.IsAny<IFormFile>()))
				.ReturnsAsync(mockDocumentUploadResult);

			var mockNewIdentityType = new IdentityType
			{
				Id = "1",
				Name = "Test Identity Type",
				DocumentImageUrl = mockDocumentUploadResult.Url.ToString()
			};

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Add(It.IsAny<IdentityType>()))
				.Callback<IdentityType>(identityType => mockNewIdentityType = identityType);

			_unitOfWorkMock
				.Setup(u => u.Save());

			// Act
			var result = await _controller.AddNewIdentityType(mockNewIdentityTypeDto);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(createdResult.Value);

			Assert.True(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status201Created.ToString(), apiResponse.StatusCode);
			Assert.Equal("New identification type created successfully", apiResponse.Message);
			Assert.Equal(mockNewIdentityType, apiResponse.Result);

			Assert.Equal("GetIdentificationById", createdResult.ActionName);
			Assert.Single(createdResult.RouteValues);
			Assert.Equal(mockNewIdentityType.Id, createdResult.RouteValues["id"]);
		}

		/// <summary>
		/// Test method for the AddNewIdentityType action in the IdentityTypeController,
		/// when a document image is not provided. It tests whether the action returns a
		/// CreatedAtActionResult with the appropriate APIResponse.
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task AddNewIdentityType_WithoutDocumentImage_ReturnsCreatedResponse()
		{
			// Arrange
			var mockNewIdentityTypeDto = new CreateIdentityDto
			{
				Name = "Test Identity Type",
				DocumentImage = null
			};

			var mockNewIdentityType = new IdentityType
			{
				Id = "1",
				Name = "Test Identity Type"
			};

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Add(It.IsAny<IdentityType>()))
				.Callback<IdentityType>(identityType => mockNewIdentityType = identityType);

			_unitOfWorkMock
				.Setup(u => u.Save());

			// Act
			var result = await _controller.AddNewIdentityType(mockNewIdentityTypeDto);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(createdResult.Value);

			Assert.True(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status201Created.ToString(), apiResponse.StatusCode);
			Assert.Equal("New identification type created successfully", apiResponse.Message);
			Assert.Equal(mockNewIdentityType, apiResponse.Result);

			Assert.Equal("GetIdentificationById", createdResult.ActionName);
			Assert.Single(createdResult.RouteValues);
			Assert.Equal(mockNewIdentityType.Id, createdResult.RouteValues["id"]);
		}

		/// <summary>
		/// Test method for the GetIdentificationById action in the IdentityTypeController,
		/// when an identity type with the specified ID exists. It tests whether the action
		/// returns an OkObjectResult with the appropriate APIResponse.
		/// </summary>
		[Fact]
		public void GetIdentificationById_ReturnsIdentificationType()
		{
			// Arrange
			var mockIdentityTypeId = "1";
			var mockIdentityType = new IdentityType { Id = mockIdentityTypeId, Name = "Identity Type 1" };

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Get(It.IsAny<Expression<Func<IdentityType, bool>>>(),
		It.IsAny<string>(), false))
				.Returns(mockIdentityType);

			// Act
			var result = _controller.GetIdentificationById(mockIdentityTypeId);

			// Assert
			var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(okObjectResult.Value);

			Assert.True(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status200OK.ToString(), apiResponse.StatusCode);
			Assert.Equal("Identity type retrieved successfully", apiResponse.Message);
			Assert.Equal(mockIdentityType, apiResponse.Result);
		}

		/// <summary>
		/// Test method for the GetIdentificationById action in the IdentityTypeController,
		/// when an identity type with the specified ID does not exist. It tests whether the
		/// action returns a NotFoundObjectResult with the appropriate APIResponse.
		/// </summary>
		[Fact]
		public void GetIdentificationById_ReturnsNotFound()
		{
			// Arrange
			var mockIdentityTypeId = "1";

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Get(It.IsAny<Expression<Func<IdentityType, bool>>>(),
		It.IsAny<string>(), false))
				.Returns((IdentityType)null);

			// Act
			var result = _controller.GetIdentificationById(mockIdentityTypeId);

			// Assert
			var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

			Assert.False(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
			Assert.Equal("Identity type not found", apiResponse.Message);
			Assert.Null(apiResponse.Result);
		}

		/// <summary>
		/// Test method for the DeleteIdentificationType action in the IdentityTypeController,
		/// when an identity type with the specified ID exists. It tests whether the action
		/// returns a NoContentResult indicating successful deletion.
		/// </summary>
		[Fact]
		public void DeleteIdentificationType_ReturnsNoContent()
		{
			// Arrange
			var mockIdentityTypeId = "1";

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Get(It.IsAny<Expression<Func<IdentityType, bool>>>(),
				It.IsAny<string>(), false))
				.Returns(new IdentityType());

			// Act
			var result = _controller.DeleteIdentificationType(mockIdentityTypeId);

			// Assert
			var actionResult = Assert.IsType<ActionResult<APIResponse>>(result);
			var noContentResult = Assert.IsType<NoContentResult>(actionResult.Result);

			Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
		}

		/// <summary>
		/// Test method for the DeleteIdentificationType action in the IdentityTypeController,
		/// when an identity type with the specified ID does not exist. It tests whether the
		/// action returns a NotFoundObjectResult with the appropriate APIResponse.
		/// </summary>
		[Fact]
		public void DeleteIdentificationType_ReturnsNotFound()
		{
			// Arrange
			var mockIdentityTypeId = "1";

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Get(It.IsAny<Expression<Func<IdentityType, bool>>>(),
		It.IsAny<string>(), false))
				.Returns((IdentityType)null);

			// Act
			var result = _controller.DeleteIdentificationType(mockIdentityTypeId);

			// Assert
			var actionResult = Assert.IsType<ActionResult<APIResponse>>(result);
			var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
			var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

			Assert.False(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
			Assert.Equal("Identity type not found", apiResponse.Message);
			Assert.Null(apiResponse.Result);
		}

		/// <summary>
		/// Test method for the UpdateIdentityType action in the IdentityTypeController,
		/// when updating an existing identity type. It tests whether the action returns
		/// an OkObjectResult with the updated identity type in the APIResponse.
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task UpdateIdentityType_WithNonExistingIdentityType_ReturnsNotFound()
		{
			// Arrange
			var mockIdentityTypeId = "1";
			var mockUpdatedIdentityTypeDto = new UpdateIdentityDto
			{
				Name = "Updated Identity Type",
				DocumentImage = null
			};

			_unitOfWorkMock
				.Setup(u => u.IdentityTypeRepository.Get(It.IsAny<Expression<Func<IdentityType, bool>>>(),
		It.IsAny<string>(), false))
				.Returns((IdentityType)null);

			// Act
			var result = await _controller.UpdateIdentityType(mockIdentityTypeId, mockUpdatedIdentityTypeDto);

			// Assert
			var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
			var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

			Assert.False(apiResponse.IsSuccess);
			Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
			Assert.Equal("Identity type not found", apiResponse.Message);
			Assert.Null(apiResponse.Result);
		}
	}
}*/