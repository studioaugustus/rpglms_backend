using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rpglms.DTOs;
using rpglms.src.data;
using rpglms.src.models;

namespace rpglms.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(UserManager<AppUser> userManager, IMapper mapper, DatabaseContext context) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly DatabaseContext _context = context;

        // GET: api/AppUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetAllUsers()
        {
            List<AppUser>? users = await _userManager.Users.ToListAsync();
            IEnumerable<AppUserDto>? userDtos = _mapper.Map<IEnumerable<AppUserDto>>(users);
            return Ok(userDtos);
        }

        // GET: api/AppUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            AppUserDto? userDto = _mapper.Map<AppUserDto>(user);
            return Ok(userDto);
        }

        // POST: api/AppUser
        [HttpPost]
        public async Task<ActionResult<AppUserDto>> CreateUser(AppUserDto userDto)
        {
            AppUser? user = _mapper.Map<AppUser>(userDto);
            IdentityResult? result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
            }

            return BadRequest(result.Errors);
        }

        // PUT: api/AppUser/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, AppUserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDto, user);
            IdentityResult? result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        // DELETE: api/AppUser/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult? result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }
    }

}
