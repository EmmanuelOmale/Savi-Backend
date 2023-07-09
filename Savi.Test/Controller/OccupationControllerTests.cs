using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Savi.Api.Controllers;
using Savi.Api.Profiles;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Savi.Test.Controller
{
    public class OccupationControllerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly OccupationController _controller;

        /// <summary>
        ///  Initializes a new instance of the OccupationControllerTests class.
        /// </summary>
        public OccupationControllerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            _mapper = mapperConfig.CreateMapper();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new OccupationController(_unitOfWorkMock.Object, _mapper);
        }

        /// <summary>
        /// Test method for the GetOccupations action in the OccupationController.
        /// It tests whether the action returns a list of occupations.
        /// </summary>
        [Fact]
        public void GetOccupations_ReturnsOccupations()
        {
            // Arrange
            var mockOccupations = new List<Occupation>
            {
                new Occupation { Id = "1", Name = "Occupation 1" },
                new Occupation { Id = "2", Name = "Occupation 2" }
            };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.GetAll(It.IsAny<Expression<Func<Occupation, bool>>>(), null))
                .Returns(mockOccupations);

            // Act
            var result = _controller.GetOccupations();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(okObjectResult.Value);

            Assert.True(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupations retrieved successfully", apiResponse.Message);
            Assert.Equal(mockOccupations, apiResponse.Result);
        }


        /// <summary>
        /// Test method for the AddNewOccupation action in the OccupationController.
        /// It tests whether the action successfully adds a new occupation and returns a Created response. 
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task AddNewOccupation_ReturnsCreatedResponse()
        {
            // Arrange
            var mockNewOccupationDto = new CreateOccupationDto
            {
                Name = "Test Occupation"
            };

            var mockNewOccupation = new Occupation
            {
                Id = "1",
                Name = "Test Occupation"
            };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Add(It.IsAny<Occupation>()))
                .Callback<Occupation>(occupation => mockNewOccupation = occupation);

            _unitOfWorkMock
                .Setup(u => u.Save());

            // Act
            var result = await _controller.AddNewOccupation(mockNewOccupationDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(createdResult.Value);

            Assert.True(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status201Created.ToString(), apiResponse.StatusCode);
            Assert.Equal("New occupation added successfully", apiResponse.Message);
            Assert.Equal(mockNewOccupation, apiResponse.Result);

            Assert.Equal("GetOccupationById", createdResult.ActionName);
            Assert.Single(createdResult.RouteValues);
            Assert.Equal(mockNewOccupation.Id, createdResult.RouteValues["id"]);
        }


        /// <summary>
        /// Test method for the GetOccupationById action in the OccupationController.
        /// It tests whether the action successfully retrieves an occupation by its ID and returns an Ok response.
        /// </summary>
        [Fact]
        public void GetOccupationById_ReturnsOccupation()
        {
            // Arrange
            var mockOccupationId = "1";
            var mockOccupation = new Occupation { Id = mockOccupationId, Name = "Occupation 1" };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns(mockOccupation);

            // Act
            var result = _controller.GetOccupationById(mockOccupationId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(okObjectResult.Value);

            Assert.True(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupation retrieved successfully", apiResponse.Message);
            Assert.Equal(mockOccupation, apiResponse.Result);
        }


        /// <summary>
        /// Test method for the GetOccupationById action in the OccupationController.
        /// It tests whether the action returns a NotFound response when the requested occupation is not found.
        /// </summary>
        [Fact]
        public void GetOccupationById_ReturnsNotFound()
        {
            // Arrange
            var mockOccupationId = "1";

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns((Occupation)null);

            // Act
            var result = _controller.GetOccupationById(mockOccupationId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

            Assert.False(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupation not found", apiResponse.Message);
            Assert.Null(apiResponse.Result);
        }


        /// <summary>
        /// Test method for the DeleteOccupation action in the OccupationController.
        /// It tests whether the action returns a NoContent response when the occupation is successfully deleted.
        /// </summary>
        [Fact]
        public void DeleteOccupation_ReturnsNoContent()
        {
            // Arrange
            var mockOccupationId = "1";
            var mockOccupationToDelete = new Occupation { Id = mockOccupationId, Name = "Occupation 1" };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns(mockOccupationToDelete);

            // Act
            var result = _controller.DeleteOccupation(mockOccupationId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<APIResponse>>(result);
            var noContentResult = Assert.IsType<NoContentResult>(actionResult.Result);
            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status204NoContent.ToString(),
                IsSuccess = true,
                Message = "Occupation deleted successfully"
            };
            Assert.Equal(StatusCodes.Status204NoContent.ToString(), response.StatusCode);
            Assert.True(response.IsSuccess);
            Assert.Equal("Occupation deleted successfully", response.Message);
        }


        /// <summary>
        /// Test method for the DeleteOccupation action in the OccupationController.
        /// It tests whether the action returns a NotFound response when the occupation is not found.
        /// </summary>
        [Fact]
        public void DeleteOccupation_ReturnsNotFound()
        {
            // Arrange
            var mockOccupationId = "1";

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns((Occupation)null);

            // Act
            var result = _controller.DeleteOccupation(mockOccupationId);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

            Assert.False(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupation not found", apiResponse.Message);
            Assert.Null(apiResponse.Result);
        }


        /// <summary>
        ///Test method for the UpdateOccupation action in the OccupationController.
        /// It tests whether the action returns the updated occupation when a valid occupation ID and update DTO are provided. 
        /// </summary>
        [Fact]
        public void UpdateOccupation_ReturnsUpdatedOccupation()
        {
            // Arrange
            var mockOccupationId = "1";
            var mockUpdateOccupationDto = new UpdateOccupationDto { Name = "Updated Occupation" };
            var mockExistingOccupation = new Occupation { Id = mockOccupationId, Name = "Occupation 1" };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns(mockExistingOccupation);

            // Act
            var result = _controller.UpdateOccupation(mockOccupationId, mockUpdateOccupationDto);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(okObjectResult.Value);

            Assert.True(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status200OK.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupation updated successfully", apiResponse.Message);
            Assert.Equal(mockExistingOccupation, apiResponse.Result);
        }


        /// <summary>
        /// Test method for the UpdateOccupation action in the OccupationController.
        /// It tests whether the action returns a not found response when an invalid occupation ID is provided.
        /// </summary>
        [Fact]
        public void UpdateOccupation_ReturnsNotFound()
        {
            // Arrange
            var mockOccupationId = "1";
            var mockUpdateOccupationDto = new UpdateOccupationDto { Name = "Updated Occupation" };

            _unitOfWorkMock
                .Setup(u => u.OccupationRepository.Get(It.IsAny<Expression<Func<Occupation, bool>>>(), null, false))
                .Returns((Occupation)null);

            // Act
            var result = _controller.UpdateOccupation(mockOccupationId, mockUpdateOccupationDto);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var apiResponse = Assert.IsType<APIResponse>(notFoundObjectResult.Value);

            Assert.False(apiResponse.IsSuccess);
            Assert.Equal(StatusCodes.Status404NotFound.ToString(), apiResponse.StatusCode);
            Assert.Equal("Occupation not found", apiResponse.Message);
            Assert.Null(apiResponse.Result);
        }
    }
}
