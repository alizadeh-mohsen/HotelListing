using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using HotelListing.Data.DTOs;
using HotelListing.Service.interfaces;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using ILogger = Serilog.ILogger;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;


        public CountriesController(IUnitOfWork unitOfWork, ILogger<CountriesController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountryDto()
        {
            try
            {
                var countries = await _unitOfWork.CountryRepository.GetAll(includes: new List<string> { "HotelDto" });
                var result = _mapper.Map<IList<CountryDto>>(countries);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");
                return StatusCode(500, ex);
            }
        }

        //    // GET: api/Countries/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<CountryDto>> GetCountryDto(int id)
        //    {
        //        //var countryDto = await _context.CountryDto.FindAsync(id);

        //        //if (countryDto == null)
        //        //{
        //        //    return NotFound();
        //        //}

        //        //return countryDto;
        //        return NoContent();
        //    }

        //    // PUT: api/Countries/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutCountryDto(int id, CountryDto countryDto)
        //    {
        //        //if (id != countryDto.Id)
        //        //{
        //        //    return BadRequest();
        //        //}

        //        //_context.Entry(countryDto).State = EntityState.Modified;

        //        //try
        //        //{
        //        //    await _context.SaveChangesAsync();
        //        //}
        //        //catch (DbUpdateConcurrencyException)
        //        //{
        //        //    if (!CountryDtoExists(id))
        //        //    {
        //        //        return NotFound();
        //        //    }
        //        //    else
        //        //    {
        //        //        throw;
        //        //    }
        //        //}

        //        return NoContent();
        //    }

        //    // POST: api/Countries
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<CountryDto>> PostCountryDto(CountryDto countryDto)
        //    {
        //        //_context.CountryDto.Add(countryDto);
        //        //await _context.SaveChangesAsync();

        //        //return CreatedAtAction("GetCountryDto", new { id = countryDto.Id }, countryDto);
        //        return NoContent();
        //    }

        //    // DELETE: api/Countries/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteCountryDto(int id)
        //    {
        //        //var countryDto = await _context.CountryDto.FindAsync(id);
        //        //if (countryDto == null)
        //        //{
        //        //    return NotFound();
        //        //}

        //        //_context.CountryDto.Remove(countryDto);
        //        //await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool CountryDtoExists(int id)
        //    {
        //        //return _context.CountryDto.Any(e => e.Id == id);
        //        return true;
        //    }
        //}
    }
}
