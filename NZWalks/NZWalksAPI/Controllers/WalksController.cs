using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Models.Entities;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;
        private readonly ILogger<WalksController> logger;

        public WalksController(IWalksRepository walksRepository, IMapper mapper, ILogger<WalksController> logger)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] WalkQueryObject query)
        {
            var walks = await walksRepository.GetAllAsync(query); 
            var walkDtos = mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walksRepository.GetByIdAsync(id);
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkToCreate = mapper.Map<Walk>(addWalkRequestDto);
            var walk = await walksRepository.CreateAsync(walkToCreate);
            var walkDto = mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetById), new { walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walk = mapper.Map<Walk>(updateWalkDto);
            walk = await walksRepository.UpdateAsync(id, walk);
            if (walk == null)
            {
                return BadRequest();
            }

            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walksRepository.DeleteAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }
    }
}
