using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using Clinic.Models.Mappings.DTO;

namespace Clinic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ClinicDbContext _context;
    private readonly IMapper _mapper;
    public StockController(ClinicDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    [HttpGet]
    public async Task<ActionResult<StockListDto>> Get()
    {
        var entity = await _context.Stocks.ProjectTo<StockDetailsDto>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(new StockListDto { Stocks = entity });
    }
    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<StockDetailsDto>>> Show(Guid id)
    {
        var entity = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (entity == null) return TypedResults.NotFound();
        return TypedResults.Ok(_mapper.Map<StockDetailsDto>(entity));
    }
    [HttpPost]
    public async Task<IResult> Create([FromBody] StockCreateDto stock)
    {
        var entity = new Stock
        {
            Name = stock.Name,
            Address = stock.Address,
            CityId = stock.CityId
        };
        await _context.Stocks.AddAsync(entity);
        await _context.SaveChangesAsync();
        return Results.Created("", entity.Id);
    }
    [HttpPut("{id}")]
    public async Task<Results<NotFound, Ok<StockDetailsDto>>> Update(Guid id, [FromBody] StockCreateDto stock)
    {
        var entity = await _context.Stocks.FirstOrDefaultAsync(t => t.Id == id);
        if (entity == null) return TypedResults.NotFound();
        entity.Name = stock.Name;
        entity.Address = stock.Address;
        entity.CityId = stock.CityId;
        await _context.SaveChangesAsync();
        return TypedResults.Ok(_mapper.Map<StockDetailsDto>(entity));
    }
    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> Delete(Guid id)
    {
        var entity = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (entity == null) return TypedResults.NotFound();
        _context.Stocks.Remove(entity);
        await _context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
