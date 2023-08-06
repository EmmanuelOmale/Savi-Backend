using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Savi.Data.DTO;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImageUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDto<string>> UploadFile(IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return new ResponseDto<string>
                    {
                        DisplayMessage = "No image file was uploaded.",
                        StatusCode = StatusCodes.Status400BadRequest,
                        Result = null
                    };
                }

                var cloudinaryAccount = new Account(
                    _configuration["CloudinarySettings:CloudName"],
                    _configuration["CloudinarySettings:ApiKey"],
                    _configuration["CloudinarySettings:ApiSecret"]
                );
                var cloudinary = new Cloudinary(cloudinaryAccount);

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream())
                };

                ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

                return new ResponseDto<string>
                {
                    DisplayMessage = "Image uploaded successfully.",
                    StatusCode = StatusCodes.Status200OK,
                    Result = uploadResult.Url.AbsoluteUri
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<string>
                {
                    DisplayMessage = "Image not uploaded.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Result = null
                };
            }
        }

    }
}
