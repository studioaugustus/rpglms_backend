using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rpglms.DTOs;
using rpglms.src.data;

namespace rpglms.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController(DatabaseContext context, IMapper mapper) : ControllerBase
    {
        private readonly DatabaseContext _context = context;
        private readonly IMapper _mapper = mapper;

        // GET: api/Chapter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetAllChapters()
        {
            var chapters = await _context.Chapters.ToListAsync();
            var chapterDtos = _mapper.Map<IEnumerable<ChapterDto>>(chapters);
            return Ok(chapterDtos);
        }

        // GET: api/Chapter/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapter(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            var chapterDto = _mapper.Map<ChapterDto>(chapter);
            return Ok(chapterDto);
        }

        // Other CRUD operations
    }

}
