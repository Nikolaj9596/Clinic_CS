using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinic.Models;
using Clinic.Models.Mappings.DTO.CargoDto;
using Clinic.Models.Mappings.DTO.InvoiceDto;
using Clinic.Models.Mappings.DTO.StatusDto;

namespace Clinic.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InvoiceController : ControllerBase
  {
    private readonly ClinicDbContext _context;
    private readonly IMapper _mapper;
    public InvoiceController(ClinicDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
    [HttpGet]
    public async Task<ActionResult<InvoiceListDto>> GetAll(CancellationToken cancellationToken)
    {
      var entity = await _context.Invoices.ProjectTo<InvoiceDetailsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
      return Ok(new InvoiceListDto { Invoices = entity });
    }

    [HttpGet("{id}")]
    [ProducesResponseType<InvoiceDetailsDto>(StatusCodes.Status201Created)]
    public async Task<Results<NotFound, Ok<InvoiceDetailsDto>>> GetOne(Guid id, CancellationToken cancellationToken)
    {
      var entity = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
      if (entity == null) return TypedResults.NotFound();
      return TypedResults.Ok(_mapper.Map<InvoiceDetailsDto>(entity));
    }
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IResult> Create([FromBody] InvoiceUpdateDto inv, CancellationToken cancellationToken)
    {
      var entity = new Invoice
      {
        EndpointId = inv.EndpointId,
        SenderId = inv.SenderId,
        RecipientId = inv.RecipientId,
        StatusId = inv.StatusId
      };
      await _context.Invoices.AddAsync(entity, cancellationToken);
      await _context.SaveChangesAsync(cancellationToken);
      return Results.Created("", entity.Id);
    }
    [HttpPut("{id}")]
    public async Task<Results<NotFound, Ok<InvoiceDetailsDto>>> Update(Guid id, [FromBody] InvoiceUpdateDto inv, CancellationToken cancellationToken)
    {
      var entity = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
      if (entity == null) return TypedResults.NotFound();
      entity.EndpointId = inv.EndpointId;
      entity.SenderId = inv.SenderId;
      entity.RecipientId = inv.RecipientId;
      entity.StatusId = inv.StatusId;
      await _context.SaveChangesAsync(cancellationToken);
      return TypedResults.Ok(_mapper.Map<InvoiceDetailsDto>(entity));
    }
    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> Delete(Guid id, CancellationToken cancellationToken)
    {
      var entity = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
      if (entity == null) return TypedResults.NotFound();
      _context.Invoices.Remove(entity);
      await _context.SaveChangesAsync(cancellationToken);
      return TypedResults.NoContent();
    }

    [HttpGet("{id}/cargoes")]
    [ProducesResponseType<CargoListDto>(StatusCodes.Status200OK)]
    public async Task<Results<NotFound, Ok<CargoListDto>>> GetCargoes(Guid id, CancellationToken cancellationToken)
    {
      var entity = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
      if (entity == null) return TypedResults.NotFound();
      var cargoes = entity.Cargoes.Select(i => _mapper.Map<CargoDetailsDto>(i)).ToList();
      return TypedResults.Ok(new CargoListDto { Cargoes = cargoes });
    }
    [HttpPost("{id}/cargoes")]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<Results<NotFound, Created<Guid>>> CreateCargo(Guid id, [FromBody] CargoUpdateDto cargo, CancellationToken cancellationToken)
    {
      var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
      if (invoice == null) return TypedResults.NotFound();
      var newCargo = new Cargo
      {
        Weight = cargo.Weight
      };
      invoice.Cargoes.Add(newCargo);
      await _context.SaveChangesAsync(cancellationToken);
      return TypedResults.Created("", newCargo.Id);
    }
  }
}
