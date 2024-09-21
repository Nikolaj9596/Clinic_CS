using System.Diagnostics;
using System.Net.Mime;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.UserDto;
using Clinic.Utils;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ClinicDbContext _context;
        private readonly IMapper _mapper;
        public UserController(ClinicDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        [HttpGet]
        public async Task<ActionResult<UserListDto>> GetAll()
        {
            var entity = await _context.Users.ProjectTo<UserDetailsDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new UserListDto { Users = entity });
        }
        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<UserDetailsDto>>> Get(Guid id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<UserDetailsDto>(entity));
        }
        [HttpPost]
        [ProducesResponseType<UserCreateDetailsDto>(StatusCodes.Status201Created)]
        public async Task<Results<BadRequest, Created<UserCreateDetailsDto>>> Create([FromBody] UserUpdoteDto user, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = user.Name,
                Surname = user.Surname,
                Lastname = user.Lastname,
                DocumentNumber = user.DocumentNumber,
                Email = user.Email,
                Password = PassGenerator.RandomString(16),
                RoleId = user.RoleId ?? await _GetCustomerRoleGuid(cancellationToken)
            };
            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.Created("", _mapper.Map<UserCreateDetailsDto>(entity));
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<UserDetailsDto>>> Update(Guid id, [FromBody] UserUpdoteDto user, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            entity.Name = user.Name;
            entity.Surname = user.Surname;
            entity.Lastname = user.Lastname;
            entity.DocumentNumber = user.DocumentNumber;
            entity.Email = user.Email;
            if (user.RoleId != null)
            {
                entity.RoleId = (Guid)user.RoleId;
            }
            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return TypedResults.Ok(_mapper.Map<UserDetailsDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }

        private async Task<Guid> _GetCustomerRoleGuid(CancellationToken cancellationToken)
        {
            var entity = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "customer");
            if (entity == null)
            {
                var role = new Role { Name = "customer" };
                await _context.AddAsync(role, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return role.Id;
            }
            return entity.Id;
        }
    }
}
