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
            return Ok(identityTypes);
        }

        /*[HttpPost]
        public async Task<IActionResult> AddNewIdentityType([FromForm] CreateIdentityDto newIdentityType)
        {
            if(documentImage != null && documentImage.Length > 0)
            {
                
                var documentUploadResult = await _Uploadservice.UploadImageAsync(documentImage);
                string url = documentUploadResult.Url.ToString();
                bool result = 

            }
             IdentityType IdentityType = _mapper.Map<IdentityType>(newIdentityType);
            _unitOfWork.IdentityTypeRepository.Add(IdentityType);
            _unitOfWork.Save();
            return Ok("New identification type created successfully");
        }*/
        /* [HttpPost]
         public async Task<IActionResult> AddNewIdentityType([FromForm] CreateIdentityDto newIdentityType)
         {
             if (newIdentityType.DocumentImage != null && newIdentityType.DocumentImage.Length > 0)
             {
                 var documentUploadResult = await _Uploadservice.UploadImageAsync(newIdentityType.DocumentImage);
                 newIdentityType.DocumentImageUrl = documentUploadResult.Url.ToString();
             }

             IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);
             _unitOfWork.IdentityTypeRepository.Add(identityType);
             _unitOfWork.Save();

             return Ok("New identification type created successfully");
         }*/
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
            }
            else
            {
                IdentityType identityType = _mapper.Map<IdentityType>(newIdentityType);

                _unitOfWork.IdentityTypeRepository.Add(identityType);
                _unitOfWork.Save();
            }

            return Ok("New identification type created successfully");
        }




        [HttpGet("{id}", Name = "GetIdentification")]
        public IActionResult GetIdentificationById(string id)
        {
            IdentityType identityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (identityType == null)
                return NotFound();

            return Ok(identityType);
        }


        [HttpDelete()]
        // [Authorize(Roles = "Admin")]
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
        public IActionResult UpdateIdentityType(string id, [FromBody] UpdateIdentityDto updatedIdentityType)
        {
            IdentityType existingIdentityType = _unitOfWork.IdentityTypeRepository.Get(u => u.Id == id);
            if (existingIdentityType == null)
                return NotFound();

            // Map the updated data to the existing entity
            _mapper.Map(updatedIdentityType, existingIdentityType);

            _unitOfWork.IdentityTypeRepository.Update(existingIdentityType);
            _unitOfWork.Save();

            return Ok("Identification type updated successfully");
        }

    }
}
