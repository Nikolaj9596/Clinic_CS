using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Clinic.Models.Mappings.DTO.DiseaseDto;
using Clinic.Models;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public DiseaseController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetDiseaseListDto>> Get()
        {
            var entity = await _dbContext.Diseases.ProjectTo<GetDiseaseDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetDiseaseListDto { Diseases = entity });

        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetDiseaseDto>>> Show(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Diseases.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetDiseaseDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDiseaseDto disease, CancellationToken cancellationToken)
        {
            var entity = new Disease
            {
                Name = disease.Name,
                Description = disease.Description,
                CategoryDiseaseId = disease.CategoryDiseaseId,
            };
            await _dbContext.Diseases.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetDiseaseDto>>> Update(Guid id, [FromBody] UpdateDiseaseDto disease, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Diseases.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.Name = disease.Name;
            entity.Description = disease.Description;
            entity.CategoryDiseaseId = disease.CategoryDiseaseId;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetDiseaseDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Diseases.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Diseases.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
