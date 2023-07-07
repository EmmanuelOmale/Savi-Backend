using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.DTO;
using Savi.Data.IRepositories;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDocumentUploadService _uploadService;

        public IdentityTypeController(IUnitOfWork unitOfWork, IMapper mapper, IDocumentUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        [HttpGet]
        public ActionResult<APIResponse> GetIdentityTypes()
        {
            var identityTypes = _unitOfWork.IdentityTypeRepository.GetAll().ToList();
            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Identity types retrieved successfully",
                Result = identityTypes
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> AddNewIdentityType([FromForm] CreateIdentityDto newIdentityType)
        {
            if (newIdentityType.DocumentImage != null && newIdentityType.DocumentImage.Length > 0)
            {
                var documentUploadResult = await _uploadService.UploadImageAsync(newIdentityType.DocumentImage);
                string documentImageUrl = documentUploadResult.Url.ToString();

                IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);
                identityType.DocumentImageUrl = documentImageUrl;

                _unitOfWork.IdentityTypeRepository.Add(identityType);
                _unitOfWork.Save();

                var response = new APIResponse
                {
                    StatusCode = StatusCodes.Status201Created.ToString(),
                    IsSuccess = true,
                    Message = "New identification type created successfully",
                    Result = identityType
                };
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else
            {
                IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);

                _unitOfWork.IdentityTypeRepository.Add(identityType);
                _unitOfWork.Save();

                var response = new APIResponse
                {
                    StatusCode = StatusCodes.Status201Created.ToString(),
                    IsSuccess = true,
                    Message = "New identification type created successfully",
                    Result = identityType
                };
                return StatusCode(StatusCodes.Status201Created, response);
            }
        }

        [HttpGet("{id}", Name = "GetIdentification")]
        public ActionResult<APIResponse> GetIdentificationById(string id)
        {
            var identityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (identityType == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Identity type not found"
                };
                return NotFound(notFoundResponse);
            }

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Identity type retrieved successfully",
                Result = identityType
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<APIResponse> DeleteIdentificationType(string id)
        {
            var identificationTypeToDelete = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (identificationTypeToDelete == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Identity type not found"
                };
                return NotFound(notFoundResponse);
            }

            _unitOfWork.IdentityTypeRepository.Remove(identificationTypeToDelete);
            _unitOfWork.Save();

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status204NoContent.ToString(),
                IsSuccess = true,
                Message = "Identity type deleted successfully"
            };
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateIdentityType(string id, [FromForm] UpdateIdentityDto updatedIdentityType)
        {
            var existingIdentityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (existingIdentityType == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Identity type not found"
                };
                return NotFound(notFoundResponse);
            }

            _mapper.Map(updatedIdentityType, existingIdentityType);

            if (updatedIdentityType.DocumentImage != null && updatedIdentityType.DocumentImage.Length > 0)
            {
                var documentUploadResult = await _uploadService.UploadImageAsync(updatedIdentityType.DocumentImage);
                string documentImageUrl = documentUploadResult.Url.ToString();

                existingIdentityType.DocumentImageUrl = documentImageUrl;
            }

            _unitOfWork.IdentityTypeRepository.Update(existingIdentityType);
            _unitOfWork.Save();

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Identity type updated successfully",
                Result = existingIdentityType
            };
            return Ok(response);
        }
    }
}
