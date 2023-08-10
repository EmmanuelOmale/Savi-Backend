using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Savi.Data.Domains;
using Savi.Data.IRepositories;
using Savi.Core.Interfaces;
using Savi.Data.DTO;
using Savi.Data.Enums;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KYCController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDocumentUploadService _uploadService;

        public KYCController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IDocumentUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<APIResponse> GetKycDetails()
        {
            var KycDetails = _unitOfWork.KycRepository.GetAll().ToList();
            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Customer's details retrieved successfully",
                Result = KycDetails
            };
            return Ok(response);
        }

        [HttpGet("{userid}", Name = "GetKycDetails")]
        public ActionResult<APIResponse> GetKycDetails(string userid)
        {
            var kycDetail = _unitOfWork.KycRepository.Get(u => u.UserId == userid);
            if (kycDetail == null)
            {
                var notFoundResponse = new APIResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    IsSuccess = false,
                    Message = "Kyc not completed"
                };
                return NotFound(notFoundResponse);
            }

            var response = new APIResponse
            {
                StatusCode = StatusCodes.Status200OK.ToString(),
                IsSuccess = true,
                Message = "Identity type retrieved successfully",
                Result = kycDetail
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> UpdateKycDetails([FromForm] AddKycDto kyc, string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new APIResponse { StatusCode = StatusCodes.Status404NotFound.ToString(), IsSuccess = false, Message = "User not found." });
            }
            else if (user.IsKycComplete is false)
            {
                if (kyc.DocumentImageUrl != null && kyc.DocumentImageUrl.Length > 0 || kyc.ProofOfAddress != null && kyc.ProofOfAddress.Length > 0)
                {
                    var documentUpload = await _uploadService.UploadImageAsync(kyc.DocumentImageUrl);
                    string documentUploadUrl = documentUpload.Url.ToString();
                    var proofOfAddress = await _uploadService.UploadImageAsync(kyc.ProofOfAddress);
                    string proofOfAddressUrl = proofOfAddress.Url.ToString();

                    KYC kycDetail = _mapper.Map<KYC>(kyc);
                    kycDetail.UserId = userid;
                    kycDetail.DocumentImageUrl = documentUploadUrl;
                    kycDetail.ProofOfAddress = proofOfAddressUrl;

                    _unitOfWork.KycRepository.Add(kycDetail);
                    user.IsKycComplete = true;
                    _unitOfWork.Save();

                    var jsonOptions = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    };

                    var response = new APIResponse
                    {
                        StatusCode = StatusCodes.Status201Created.ToString(),
                        IsSuccess = true,
                        Message = "kycDetail updated successfully",
                        Result = kycDetail
                    };
                    return CreatedAtAction("GetKycDetails", new { id = user.Id }, response);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new APIResponse { StatusCode = StatusCodes.Status400BadRequest.ToString(), IsSuccess = false, Message = "User KYC is already completed." });
        }



        [HttpGet("identitytypes")]
        public IActionResult GetIdentityTypes()
        {
            var identityTypes = _unitOfWork.KycRepository.GetIdentityTypes();
            return Ok(identityTypes);
        }

        [HttpGet("occupations")]
        public IActionResult GetOccupations()
        {
            var occupations = _unitOfWork.KycRepository.GetOccupations();
            return Ok(occupations);
        }

        [HttpGet("users/byoccupation/{occupation}")]
        public IActionResult GetUsersByOccupation(Occupations occupation)
        {
            var users = _unitOfWork.KycRepository.GetUsersByOccupation(occupation);
            return Ok(users);
        }

        [HttpGet("users/byidentitytype/{identityType}")]
        public IActionResult GetUsersByIdentityType(IdentificationType identityType)
        {
            var users = _unitOfWork.KycRepository.GetUsersByIdentityType(identityType);
            return Ok(users);
        }



    }
}
