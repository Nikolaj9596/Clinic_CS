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
    public class CityController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public CityController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetCityListDTO>> Get()
        {
            var entity = await _dbContext.Cityes.ProjectTo<GetCityDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetCityListDTO { Cityes = entity });

        }
        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetCityDto>>> Show(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Cityes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetCityDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCityDto city, CancellationToken cancellationToken)
        {
            var entity = new City
            {
                Name = city.Name
            };
            await _dbContext.Cityes.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetCityDto>>> Update(Guid id, [FromBody] GetCityDto city, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Cityes.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.Name = city.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetCityDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Cityes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Cityes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
