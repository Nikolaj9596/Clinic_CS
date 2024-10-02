using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;

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
        public async Task<ActionResult<DiagnosisListDto>> GetAll(CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosiss.ProjectTo<DiagnosisDetailsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return Ok(new DiagnosisListDto { Diagnosiss = entity });
        }

        [HttpGet("{id}")]
        [ProducesResponseType<DiagnosisDetailsDto>(StatusCodes.Status200OK)]
        public async Task<Results<NotFound, Ok<DiagnosisDetailsDto>>> GetOne(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosiss.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<DiagnosisDetailsDto>(entity));
        }
        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        public async Task<IResult> Create([FromBody] DiagnosisCreateDto diagnosis, CancellationToken cancellationToken)
        {
            var entity = new Diagnosis
            {
                Name = diagnosis.Status,
                Description = diagnosis.Description,
                ClientId = diagnosis.ClientId,
                DoctorId = diagnosis.DoctorId,
                Status = diagnosis.Status,
            };
            await _context.Diagnosiss.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Created("", entity.Id);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<DiagnosisDetailsDto>>> Update(Guid id, [FromBody] DiagnosisUpdateDto diagnosis, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosiss.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            entity.EndDataDiagnosis = diagnosis.EndDataDiagnosis;
            entity.StartDataDiagnosis = diagnosis.StartDataDiagnosis;
            entity.ClientId = diagnosis.ClientId;
            entity.DoctorId = diagnosis.DoctorId;
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<DiagnosisDetailsDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Diagnosiss.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _context.Diagnosiss.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
