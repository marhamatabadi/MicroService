using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepository repository,
            IMapper mapper,
            ICommandDataClient commandDataClient
            )
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public IActionResult GetPlatforms()
        {
            var list = _repository.GetAllPlatforms();
            var mappedList = _mapper.Map<IEnumerable<PlatformReadDto>>(list);
            return Ok(mappedList);
        }

        [HttpGet("{id}",Name = nameof(GetPlatformById))]
        public IActionResult GetPlatformById(int id)
        {
            var obj = _repository.GetPlatformById(id);
            if (obj == null)
                return NoContent();
            var result = _mapper.Map<PlatformReadDto>(obj);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDto platform)
        {
            if (platform == null)
                return BadRequest();
            var entity = _mapper.Map<Platform>(platform);
            _repository.CreatePlatform(entity);
            var saveResult = _repository.SaveChanges();
            if (saveResult)
            {
                var dto = _mapper.Map<PlatformReadDto>(entity);
                try
                {
                    await _commandDataClient.SendPlatformToCommand(dto);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                return CreatedAtRoute(nameof(GetPlatformById), new { entity.Id }, dto);
            }
            else
                return StatusCode(StatusCodes.Status406NotAcceptable);

        }

  
    }
}
