using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.ProfessionDto;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProfessionController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetProfessionListDto>> Get()
        {
            var entity = await _dbContext.Professions.ProjectTo<GetProfessionDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetProfessionListDto { Professions = entity });

        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetProfessionDto>>> Show(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Professions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetProfessionDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GetProfessionDto profession, CancellationToken cancellationToken)
        {
            var entity = new Profession
            {
                Name = profession.Name
            };
            await _dbContext.Professions.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetProfessionDto>>> Update(Guid id, [FromBody] GetProfessionDto profession, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Professions.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.Name = profession.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetProfessionDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Professions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Professions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
