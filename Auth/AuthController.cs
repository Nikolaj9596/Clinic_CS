using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClinicDbContext _context;
        public AuthController(ClinicDbContext context) => _context = context;
        [HttpPost]
        public async Task<ActionResult> Auth(AuthDto authDto, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Email == authDto.email, cancellationToken);
            if (entity == null) return Unauthorized();
            if (!entity.Password.Equals(authDto.pass)) return Unauthorized();
            return Ok(AuthService.GenerateToken(entity));
        }
    }
}
