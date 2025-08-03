using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Dtos;
using NZWalksAPI.Models.Entities;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RegionController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            _logger.LogInformation("GetAllRegion action method was invoked.");
            // Get all regions from database 
            var regions = await _regionRepository.GetAllAsync(query);
            
            _logger.LogInformation($"Finished GetAllRegion with data : {JsonSerializer.Serialize(regions)} ");

            // Convert the regions to regionDto as we do not want to expose our model to end user
            var regionsDto = _mapper.Map<List<RegionDto>>(regions);

            // Return back the regionDto with success status to client
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get a region based on id 
            var region = await _regionRepository.GetByIdAsync(id);

            // Convert it to RegionDto
            var regionDto = _mapper.Map<RegionDto>(region);

            // Return RegionDto to user 
            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {           
            // Map CreateRegionDto to Region
            var regionToCreate = _mapper.Map<Region>(createRegionDto);

            // Add regionToCreate to database 
            var region = await _regionRepository.CreateAsync(regionToCreate);

            // Map region entity to RegionDto 
            var regionDto = _mapper.Map<RegionDto>(region);

            // Return RegionDto to user 
            return CreatedAtAction(nameof(GetById), new { regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            // Map UpdateRegionDto to Region
            var region = _mapper.Map<Region>(updateRegionDto);

            // Update region in database 
            region = await _regionRepository.UpdateAsync(id, region);
            if (region == null)
            {
                return BadRequest();
            }

            // Map region entity to RegionDto 
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete region from database 
            var region = await _regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
    }
}
