using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using AutoMapper;
using Clinic.Models.Mappings.DTO;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<Results<NotFound, Ok<GetProfessionDto>>> Show(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Professions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetProfessionDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProfessionDto profession, CancellationToken cancellationToken)
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
        public async Task<Results<NotFound, Ok<GetProfessionDto>>> Update(int id, [FromBody] GetProfessionDto profession, CancellationToken cancellationToken)
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
        public async Task<Results<NotFound, NoContent>> Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Professions.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Professions.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
