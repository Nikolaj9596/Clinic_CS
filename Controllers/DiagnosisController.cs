using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.DiagnosisDto;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
        private readonly ClinicDbContext _context;
        private readonly IMapper _mapper;
        public DiagnosisController(ClinicDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
        [HttpGet]
        public async Task<ActionResult<GetDiagnosisListDto>> GetAll(CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosis.ProjectTo<GetDiagnosisDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return Ok(new GetDiagnosisListDto { Diagnosis = entity });
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetDiagnosisDto>(StatusCodes.Status200OK)]
        public async Task<Results<NotFound, Ok<GetDiagnosisDto>>> GetOne(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosis.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetDiagnosisDto>(entity));
        }
        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        public async Task<IResult> Create([FromBody] CreateDiagnosisDto diagnosis, CancellationToken cancellationToken)
        {
            var entity = new Diagnosis
            {
                Name = diagnosis.Status,
                Description = diagnosis.Description,
                ClientId = diagnosis.ClientId,
                DoctorId = diagnosis.DoctorId,
                Status = diagnosis.Status,
            };
            await _context.Diagnosis.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Created("", entity.Id);
        }

        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetDiagnosisDto>>> Update(Guid id, [FromBody] UpdateDiagnosisDto diagnosis, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosis.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            entity.Name = diagnosis.Name;
            entity.Description = diagnosis.Description;
            entity.ClientId = diagnosis.ClientId;
            entity.DoctorId = diagnosis.DoctorId;
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetDiagnosisDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosis.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _context.Diagnosis.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
