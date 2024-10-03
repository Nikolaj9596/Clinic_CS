using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Clinic.Models.Mappings.DTO.ClientDto;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public ClientController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetClientListDto>> Get()
        {
            var entity = await _dbContext.Clients.ProjectTo<GetClientDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetClientListDto { Clients = entity });

        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetClientDto>>> Show(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetClientDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateClientDto client, CancellationToken cancellationToken)
        {
            var entity = new Client
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                MiddleName = client.MiddleName,
                DateBirthday = client.DateBirthday,
                Address = client.Address,
                Avatar = client.Avatar
            };
            await _dbContext.Clients.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetClientDto>>> Update(Guid id, [FromBody] UpdateClientDto client, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.FirstName = client.FirstName;
            entity.LastName = client.LastName;
            entity.MiddleName = client.MiddleName;
            entity.DateBirthday = client.DateBirthday;
            entity.Address = client.Address;
            entity.Avatar = client.Avatar;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetClientDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Clients.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
