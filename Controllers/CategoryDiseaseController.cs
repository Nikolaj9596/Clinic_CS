using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.CategoryDiseaseDto;

namespace Clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryDiseaseController : ControllerBase
    {
        private readonly ClinicDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryDiseaseController(ClinicDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetCategoryDiseaseListDto>> Get()
        {
            var entity = await _dbContext.CategoryDiseases.ProjectTo<GetCategoryDiseaseDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(new GetCategoryDiseaseListDto { CategoryDiseases = entity });

        }

        [HttpGet("{id}")]
        public async Task<Results<NotFound, Ok<GetCategoryDiseaseDto>>> Show(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CategoryDiseases.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetCategoryDiseaseDto>(entity));
        }
    }
}
