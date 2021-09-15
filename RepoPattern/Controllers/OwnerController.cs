using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoPattern.Controllers
{
    //[Route("api/owner")]
    [ApiController]
    [Route("[controller]/[Action]")]
    public class OwnerController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        //private readonly RepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

            
        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.Owner.GetAllOwners();

                _logger.LogInfo($"Returned All Owners from database");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);

                return Ok(ownersResult);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {e.Message}");

                return StatusCode(500,"Internal Server Error");
                
            }
        }

        //[HttpGet("id")]
        [HttpGet]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);

                if (owner==null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't be in database");
                    return NotFound();
                }
                _logger.LogInfo($"Returned Owner with id: {id} ");

                var ownerResult = _mapper.Map<OwnerDto>(owner);
                return Ok(ownerResult);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside the GetOwnerById action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        //[HttpGet("{id}/account")]
        [HttpGet]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);

                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id} hasn't been found in database");
                    return NotFound();
                }

                _logger.LogInfo($"Returned owner with details for id: {id}");

                var ownerResult = _mapper.Map<OwnerDto>(owner);
                return Ok(ownerResult);

            }
            catch (Exception e)
            {

                _logger.LogError($"Something went wrong in the GetOwnerWithDetails action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        //[HttpPost("{id}", Name = "OwnerById")] 
        [HttpPost]
        public IActionResult CreateOwner([FromBody]OwnerForCreationDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from the client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from the client");
                    return BadRequest("Invalid model object");

                }

                var ownerEntity = _mapper.Map<Owner>(owner);

                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Save();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);

                return CreatedAtRoute("OwnerById", new {id = createdOwner.Id }, createdOwner);
            }
            catch (Exception e)
            {

                _logger.LogError($"Something went wrong inside the CreateOwner action: {e.Message}");
                return StatusCode(500, "Internal Server Error");

            }

        }



        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody]OwnerForUpdateDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from the client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from the client");
                    return BadRequest("Invalid model object");

                }

                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id  {id} hasn't be found in database");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);

                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Save();

                return NoContent();
                
            }
            catch (Exception e)
            {

                _logger.LogError($"Something went wrong inside the UpdateOwner action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id} hasn't been found in database");
                    return NotFound();
                }

                _repository.Owner.DeleteOwner(owner);
                _repository.Save();

                return NoContent();
            }
            catch (Exception e)
            {

                _logger.LogError($"Something went wrong inside the DeleteOwner action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
