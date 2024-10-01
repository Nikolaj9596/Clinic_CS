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
        public async Task<Results<NotFound, Ok<GetCategoryDiseaseDto>>> Show(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CategoryDiseases.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            if (entity == null) return TypedResults.NotFound();
            return TypedResults.Ok(_mapper.Map<GetCategoryDiseaseDto>(entity));
        }
    }
}
