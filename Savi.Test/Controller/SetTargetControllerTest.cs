using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Savi.Api.Controllers;
using Savi.Api.Service;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.Enums;

namespace Savi.Api.Tests
{
    public class SetTargetControllerTests
    {
        private readonly Mock<ISetTargetService> _mockSetTargetService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SetTargetController _controller;

        public SetTargetControllerTests()
        {
            _mockSetTargetService = new Mock<ISetTargetService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SetTargetController(_mockSetTargetService.Object, _mockMapper.Object);
        }

        private List<SetTarget> GetMockTargets()
        {
            var targets = new List<SetTarget>
    {
        new SetTarget
        {
            Id = Guid.NewGuid(),
            Target = "Target 1",
            TargetAmount = 1000,
            AmountToSave = 500,
            Frequency = FrequencyType.Weekly,
            StartDate = DateTime.Now,
            WithdrawalDate = DateTime.Now.AddDays(30)
        },
        new SetTarget
        {
            Id = Guid.NewGuid(),
            Target = "Target 2",
            TargetAmount = 2000,
            AmountToSave = 1000,
            Frequency = FrequencyType.Monthly,
            StartDate = DateTime.Now,
            WithdrawalDate = DateTime.Now.AddDays(60)
        },
    };
            return targets;
        }

       
        [Fact]
        public async Task GetAllTargets_ReturnsOkWithTargets()
        {
            // Arrange
            var targets = GetMockTargets();
            _mockSetTargetService.Setup(s => s.GetAllTargets()).ReturnsAsync(new ResponseDto<IEnumerable<SetTarget>>
            {
                StatusCode = 200,
                Result = targets
            });

            // Act
            var result = await _controller.GetAllTargets();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto<IEnumerable<SetTarget>>>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(targets, response.Result);
        }

        
        [Fact]
        public async Task GetTargetById_WithValidId_ReturnsOkWithTarget()
        {
            // Arrange
            var targetId = Guid.NewGuid();
            var target = new SetTarget
            {
                Id = targetId,
                Target = "Target 1",
                TargetAmount = 1000,
                AmountToSave = 500,
                Frequency = FrequencyType.Weekly,
                StartDate = DateTime.Now,
                WithdrawalDate = DateTime.Now.AddDays(30)
            };
            _mockSetTargetService.Setup(s => s.GetTargetById(targetId)).ReturnsAsync(new ResponseDto<SetTarget>
            {
                StatusCode = 200,
                Result = target
            });

            // Act
            var result = await _controller.GetTargetById(targetId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto<SetTarget>>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(target, response.Result);
        }
        [Fact]
        public async Task CreateTarget_WithValidData_ReturnsCreatedWithTarget()
        {
            // Arrange
            var newTarget = new SetTarget
            {
                Target = "New Target",
                TargetAmount = 1500,
                AmountToSave = 750,
                Frequency = FrequencyType.Monthly,
                StartDate = DateTime.Now,
                WithdrawalDate = DateTime.Now.AddDays(45)
            };
            _mockSetTargetService.Setup(s => s.CreateTarget(It.IsAny<SetTarget>())).ReturnsAsync(new ResponseDto<SetTarget>
            {
                StatusCode = 201,
                Result = newTarget
            });

            // Act
            var result = await _controller.CreateTarget(newTarget);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result.Result); 
            var response = Assert.IsType<ResponseDto<SetTarget>>(createdResult.Value);
            Assert.Equal(201, response.StatusCode);
            Assert.Equal(newTarget, response.Result);
        }


       
        [Fact]
        public async Task UpdateTarget_WithValidData_ReturnsOkWithUpdatedTarget()
        {
            // Arrange
            var targetId = Guid.NewGuid();
            var updatedTarget = new SetTarget
            {
                Id = targetId,
                Target = "Updated Target",
                TargetAmount = 2000,
                AmountToSave = 1000,
                Frequency = FrequencyType.Weekly,
                StartDate = DateTime.Now,
                WithdrawalDate = DateTime.Now.AddDays(60)
            };
            _mockSetTargetService.Setup(s => s.UpdateTarget(targetId, It.IsAny<SetTarget>())).ReturnsAsync(new ResponseDto<SetTarget>
            {
                StatusCode = 200,
                Result = updatedTarget
            });

            // Act
            var result = await _controller.UpdateTarget(targetId, updatedTarget);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SetTarget>(okResult.Value);
            Assert.Equal(updatedTarget, response);
        }

        
        [Fact]
        public async Task DeleteTarget_WithValidId_ReturnsOkWithDeletedTarget()
        {
            // Arrange
            var targetId = Guid.NewGuid();
            _mockSetTargetService.Setup(s => s.DeleteTarget(targetId)).ReturnsAsync(new ResponseDto<SetTarget>
            {
                StatusCode = 200,
                Result = null 
            });

            // Act
            var result = await _controller.DeleteTarget(targetId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<ResponseDto<SetTarget>>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Null(response.Result); 
        }

    }
}
