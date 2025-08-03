using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Models.Entities;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DifficultyController> logger;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper, ILogger<DifficultyController> logger)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            logger.LogInformation("GetAllDifficulty action method was invoked.");

            var difficulties = await difficultyRepository.GetAllAsync(query);

            logger.LogInformation($"Finished GetAllDifficulty with data : {JsonSerializer.Serialize(difficulties)} ");

            var difficultyDtos = mapper.Map<List<DifficultyDto>>(difficulties);

            return Ok(difficultyDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var difficulty = await difficultyRepository.GetByIdAsync(id);

            var difficultyDto = mapper.Map<DifficultyDto>(difficulty);

            return Ok(difficultyDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateDifficultyDto createDifficultyDto)
        {
            var difficultyToCreate = mapper.Map<Difficulty>(createDifficultyDto);

            var difficulty = await difficultyRepository.CreateAsync(difficultyToCreate);

            var difficultyDto = mapper.Map<DifficultyDto>(difficulty);

            return CreatedAtAction(nameof(GetById), new { difficultyDto.Id }, difficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDifficultyDto updateDifficultyDto)
        {
            var difficulty = mapper.Map<Difficulty>(updateDifficultyDto);

            difficulty = await difficultyRepository.UpdateAsync(id, difficulty);
            if (difficulty == null)
            {
                return BadRequest();
            }

            var difficultyDto = mapper.Map<DifficultyDto>(difficulty);
            return Ok(difficultyDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var difficulty = await difficultyRepository.DeleteAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            var difficultyDto = mapper.Map<DifficultyDto>(difficulty);

            return Ok(difficultyDto);
        }
    }
}
