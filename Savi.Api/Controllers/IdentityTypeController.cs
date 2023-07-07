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
        private readonly IDocumentUploadService _Uploadservice;

        public IdentityTypeController(IUnitOfWork unitOfWork, IMapper mapper, IDocumentUploadService Uploadservice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _Uploadservice = Uploadservice;
        }


        [HttpGet]
        public IActionResult GetIdentityTypes()
        {
            List<IdentityType> identityTypes = _unitOfWork.IdentityTypeRepository.GetAll().ToList();
            if (identityTypes != null)
            {
                return Ok(identityTypes);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving identity types.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewIdentityType([FromForm] CreateIdentityDto newIdentityType)
        {
            if (newIdentityType.DocumentImage != null && newIdentityType.DocumentImage.Length > 0)
            {
                var documentUploadResult = await _Uploadservice.UploadImageAsync(newIdentityType.DocumentImage);
                string documentImageUrl = documentUploadResult.Url.ToString();

                IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);
                identityType.DocumentImageUrl = documentImageUrl;

                _unitOfWork.IdentityTypeRepository.Add(identityType);
                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status201Created, "New identification type created successfully");
            }
            else
            {
                IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);

                _unitOfWork.IdentityTypeRepository.Add(identityType);
                _unitOfWork.Save();

                return StatusCode(StatusCodes.Status201Created, "New identification type created successfully");
            }
        }




        [HttpGet("{id}", Name = "GetIdentification")]
        public IActionResult GetIdentificationById(string id)
        {
            IdentityType identityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (identityType == null)
                return NotFound();

            return Ok(identityType);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteIdentificationType(string id)
        {
            var IdentificationTypeToDelete = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (IdentificationTypeToDelete == null)
                return NotFound();
            _unitOfWork.IdentityTypeRepository.Remove(IdentificationTypeToDelete);
            _unitOfWork.Save();
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdentityType(string id, [FromForm] UpdateIdentityDto updatedIdentityType)
        {
            IdentityType existingIdentityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (existingIdentityType == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedIdentityType, existingIdentityType);

            if (updatedIdentityType.DocumentImage != null && updatedIdentityType.DocumentImage.Length > 0)
            {
                var documentUploadResult = await _Uploadservice.UploadImageAsync(updatedIdentityType.DocumentImage);
                string documentImageUrl = documentUploadResult.Url.ToString();

                existingIdentityType.DocumentImageUrl = documentImageUrl;
            }
            _unitOfWork.IdentityTypeRepository.Update(existingIdentityType);
            _unitOfWork.Save();

            return StatusCode(StatusCodes.Status200OK, "Identification type updated successfully");
        }

    }
}
