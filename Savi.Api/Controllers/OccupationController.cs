using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OccupationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<APIResponse> GetOccupations()
        {
            var occupations = _unitOfWork.OccupationRepository.GetAll().ToList();
            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Occupations retrieved successfully",
                Result = occupations
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> AddNewOccupation([FromBody] CreateOccupationDto newOccupation)
        {
            var occupation = _mapper.Map<Occupation>(newOccupation);
            _unitOfWork.OccupationRepository.Add(occupation);
            _unitOfWork.Save();

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status201Created.ToString(),
                IsSuccess = true,
                Message = "New occupation added successfully",
                Result = occupation
            };

            return CreatedAtAction("GetOccupationById", new { id = occupation.Id }, response);
        }


        [HttpGet("{id}", Name = "GetOccupationById")]
        public ActionResult<APIResponse> GetOccupationById(string id)
        {
            var occupation = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (occupation == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Occupation not found"
                };
                return NotFound(notFoundResponse);
            }

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Occupation retrieved successfully",
                Result = occupation
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse> DeleteOccupation(string id)
        {
            var occupationToDelete = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (occupationToDelete == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Occupation not found"
                };
                return NotFound(notFoundResponse);
            }

            _unitOfWork.OccupationRepository.Remove(occupationToDelete);
            _unitOfWork.Save();

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status204NoContent.ToString(),
                IsSuccess = true,
                Message = "Occupation deleted successfully"
            };
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<APIResponse> UpdateOccupation(string id, [FromBody] UpdateOccupationDto updateOccupation)
        {
            var existingOccupation = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (existingOccupation == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Occupation not found"
                };
                return NotFound(notFoundResponse);
            }

            _mapper.Map(updateOccupation, existingOccupation);
            _unitOfWork.OccupationRepository.Update(existingOccupation);
            _unitOfWork.Save();

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Occupation updated successfully",
                Result = existingOccupation
            };
            return Ok(response);
        }
    }
}
