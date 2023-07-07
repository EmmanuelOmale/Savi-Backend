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

        public OccupationController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetOccupations()
        {
            List<Occupation> occupations = _unitOfWork.OccupationRepository.GetAll().ToList();
            if (occupations != null)
            {
                return Ok(occupations);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving occupations.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddNewOccupation([FromBody] CreateOccupationDto newOccupation)
        {
            Occupation occupation = _mapper.Map<Occupation>(newOccupation);
            _unitOfWork.OccupationRepository.Add(occupation);
            _unitOfWork.Save();

            return StatusCode(StatusCodes.Status201Created, "New occupation added successfully");
        }


        [HttpGet("{id}", Name = "GetOccupationById")]
        public IActionResult GetOccupationById(string id)
        {
            Occupation occupation = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (occupation == null)
                return NotFound();

            return Ok(occupation);
        }


        [HttpDelete()]
        public IActionResult DeleteOccupation(string id)
        {
            var occupationToDelete = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (occupationToDelete == null)
                return NotFound();
            _unitOfWork.OccupationRepository.Remove(occupationToDelete);
            _unitOfWork.Save();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOccupation(string id, [FromBody] UpdateOccupationDto updateOccupation)
        {
            Occupation existingOccupation = _unitOfWork.OccupationRepository.Get(u => u.Id == id);
            if (existingOccupation == null)
            {
                return NotFound();
            }

            _mapper.Map(updateOccupation, existingOccupation);

            _unitOfWork.OccupationRepository.Update(existingOccupation);
            _unitOfWork.Save();

            return Ok("Occupation updated successfully");
        }
    }
}
