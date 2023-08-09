//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Savi.Api.Controllers;
//using Savi.Api.Service;
//using Savi.Data.Domains;
//using Savi.Data.DTO;

//namespace Savi.Api.Tests
//{
//    public class SavingGoalControllerTests
//    {
//        private readonly Mock<ISavingGoalService> _mockGoalService;
//        private readonly SavingGoalController _controller;

//        public SavingGoalControllerTests()
//        {
//            _mockGoalService = new Mock<ISavingGoalService>();
//            _controller = new SavingGoalController(_mockGoalService.Object);
//        }

//        [Fact]
//        public async Task CreateGoal_ReturnsOkResult()
//        {
//            // Arrange
//            var goal = new SavingGoal();

//            // Mock the service
//            _mockGoalService
//                .Setup(service => service.CreateGoal(goal))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 200 });

//            // Act
//            var result = await _controller.CreateGoal(goal);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task GetAllGoals_ReturnsOkResult()
//        {
//            // Arrange
//            var goals = new List<SavingGoal>();

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.GetAllGoals())
//                .ReturnsAsync(new ResponseDto<List<SavingGoal>>
//                {
//                    StatusCode = 200,
//                    Result = goals
//                });

//            // Act
//            var result = await _controller.GetAllGoals();

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task GetGoalById_WithValidId_ReturnsOkResult()
//        {
//            // Arrange
//            var id = 1;
//            var goal = new SavingGoal();

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.GetGoalById(id))
//                .ReturnsAsync(new ResponseDto<SavingGoal>
//                {
//                    StatusCode = 200,
//                    Result = goal
//                });

//            // Act
//            var result = await _controller.GetGoalById(id);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task GetGoalById_WithInvalidId_ReturnsNotFoundResult()
//        {
//            // Arrange
//            var id = 1;

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.GetGoalById(id))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 404 });

//            // Act
//            var result = await _controller.GetGoalById(id);

//            // Assert
//            Assert.IsType<NotFoundObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task DeleteGoal_WithValidId_ReturnsOkResult()
//        {
//            // Arrange
//            var id = 1;

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.DeleteGoal(id))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 200 });

//            // Act
//            var result = await _controller.DeleteGoal(id);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task DeleteGoal_WithInvalidId_ReturnsNotFoundResult()
//        {
//            // Arrange
//            var id = 1;

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.DeleteGoal(id))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 404 });

//            // Act
//            var result = await _controller.DeleteGoal(id);

//            // Assert
//            Assert.IsType<NotFoundObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task UpdateGoal_WithValidId_ReturnsOkResult()
//        {
//            // Arrange
//            var id = 1;
//            var updatedGoal = new SavingGoal();

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.UpdateGoal(id, updatedGoal))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 200 });

//            // Act
//            var result = await _controller.UpdateGoal(id, updatedGoal);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task UpdateGoal_WithInvalidId_ReturnsNotFoundResult()
//        {
//            // Arrange
//            var id = 1;
//            var updatedGoal = new SavingGoal();

//            // Mock the service method
//            _mockGoalService
//                .Setup(service => service.UpdateGoal(id, updatedGoal))
//                .ReturnsAsync(new ResponseDto<SavingGoal> { StatusCode = 404 });

//            // Act
//            var result = await _controller.UpdateGoal(id, updatedGoal);

//            // Assert
//            Assert.IsType<NotFoundObjectResult>(result.Result);
//        }

//    }
//}