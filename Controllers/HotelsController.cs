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
    public class HotelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<HotelsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Hotels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.HotelRepository.GetAll(includes: new List<string> { "Country" });
                var result = _mapper.Map<IList<HotelDto>>(hotels);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                return StatusCode(500, "");
            }
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {

            var hotel = await _unitOfWork.HotelRepository.Get(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<HotelDto>(hotel);
            return result;
        }

        //// PUT: api/Hotels/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        //{
        //    if (id != hotel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(hotel).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HotelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Hotels
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        //{
        //    _context.Hotels.Add(hotel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        //}

        //// DELETE: api/Hotels/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHotel(int id)
        //{
        //    var hotel = await _context.Hotels.FindAsync(id);
        //    if (hotel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Hotels.Remove(hotel);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool HotelExists(int id)
        //{
        //    return _context.Hotels.Any(e => e.Id == id);
        //}
    }
}
