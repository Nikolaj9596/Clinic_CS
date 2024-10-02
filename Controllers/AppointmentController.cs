using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.AppointmentDto;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ClinicDbContext _context;
        private readonly IMapper _mapper;
        public AppointmentController(ClinicDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
        [HttpGet]
        public async Task<ActionResult<GetAppointmentListDto>> GetAll(CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.ProjectTo<GetAppointmentDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return Ok(new GetAppointmentListDto { Appointments = entity });
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetAppointmentDto>(StatusCodes.Status200OK)]
        public async Task<Results<NotFound, Ok<GetAppointmentDto>>> GetOne(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetAppointmentDto>(entity));
        }
        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        public async Task<IResult> Create([FromBody] CreateAppointmentDto appointment, CancellationToken cancellationToken)
        {
            var entity = new Appointment
            {
                EndDataAppointment = appointment.EndDataAppointment,
                StartDataAppointment = appointment.StartDataAppointment,
                ClientId = appointment.ClientId,
                DoctorId = appointment.DoctorId
            };
            await _context.Appointments.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Results.Created("", entity.Id);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetAppointmentDto>>> Update(Guid id, [FromBody] UpdateAppointmentDto appointment, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            entity.EndDataAppointment = appointment.EndDataAppointment;
            entity.StartDataAppointment = appointment.StartDataAppointment;
            entity.ClientId = appointment.ClientId;
            entity.DoctorId = appointment.DoctorId;
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetAppointmentDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _context.Appointments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
