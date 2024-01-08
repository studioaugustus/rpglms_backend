using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rpglms.DTOs;
using rpglms.src.data;
using rpglms.src.models;

namespace rpglms.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChronicleController(DatabaseContext context, IMapper mapper) : ControllerBase
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        // GET: api/Chronicle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChronicleDto>>> GetAllChronicles()
        {
            List<Chronicle>? chronicles = await _context.Chronicles.ToListAsync();
            IEnumerable<ChronicleDto>? chronicleDtos = _mapper.Map<IEnumerable<ChronicleDto>>(chronicles);
            return Ok(chronicleDtos);
        }

        // GET: api/Chronicle/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ChronicleDto>> GetChronicle(int id)
        {
            Chronicle? chronicle = await _context.Chronicles.FindAsync(id);
            if (chronicle == null)
            {
                return NotFound();
            }
            ChronicleDto? chronicleDto = _mapper.Map<ChronicleDto>(chronicle);
            return Ok(chronicleDto);
        }

        // Other CRUD operations
    }
}
