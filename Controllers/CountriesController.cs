using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HotelListing.Data.DTOs;
using HotelListing.Service.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountryDto()
        {
            try
            {
                var countries = await _unitOfWork.CountryRepository.GetAll(includes: new List<string> { "Hotels" });
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryDto>> GetCountryDto(int id)
        {
            try
            {
                var country = await _unitOfWork.CountryRepository.Get(c => c.Id == id, includes: new List<string> { "Hotels" });

                if (country == null)
                    return NotFound();

                var result = _mapper.Map<CountryDto>(country);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"server error");
                return StatusCode(500, e);
            }

        }

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
