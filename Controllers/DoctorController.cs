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
    public class DoctorController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public DoctorController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetDoctorListDto>> Get()
        {
            var entity = await _dbContext.Doctors.ProjectTo<GetDoctorDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetDoctorListDto { Doctors = entity });

        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetDoctorDto>>> Show(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Doctors.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetDoctorDto>(entity));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDoctorDto doctor, CancellationToken cancellationToken)
        {
            var entity = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                MiddleName = doctor.MiddleName,
                DateBirthday = doctor.DateBirthday,
                DateStartWork = doctor.DateStartWork,
                Avatar = doctor.Avatar,
                ProfessionId = doctor.ProfessionId,
            };
            await _dbContext.Doctors.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<Results<NotFound, Ok<GetDoctorDto>>> Update(int id, [FromBody] UpdateDoctorDto doctor, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Doctors.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return TypedResults.NotFound();
            }
            entity.FirstName = doctor.FirstName;
            entity.LastName = doctor.LastName;
            entity.MiddleName = doctor.MiddleName;
            entity.DateBirthday = doctor.DateBirthday;
            entity.DateStartWork = doctor.DateStartWork;
            entity.ProfessionId = doctor.ProfessionId;
            entity.Avatar = doctor.Avatar;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.Ok(_mapper.Map<GetDoctorDto>(entity));
        }
        [HttpDelete("{id}")]
        public async Task<Results<NotFound, NoContent>> Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Doctors.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            _dbContext.Doctors.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
