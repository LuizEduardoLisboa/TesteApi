using ApiTeste.Domain;
using ApiTeste.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/compras")]
public class ComprasController : ControllerBase
{
    private readonly ICompraService _service;

    public ComprasController(ICompraService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarCompraDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse(
                "Dados inválidos"
            ));
        }

        try
        {
            Compra compra = await _service.CriarAsync(dto);
            return StatusCode(201, compra);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(
                "Erro ao criar compra",
                ex.Message
            ));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            List<Compra> compras = await _service.ListarAsync();
            return Ok(compras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(
                "Erro ao listar compras",
                ex.Message
            ));
        }
    }

    [HttpGet("ultima")]
    public async Task<IActionResult> ListarUltima()
    {
        try
        {
            Compra? ultimaCompra = await _service.ObterUltimaAsync();

            if (ultimaCompra == null)
            {
                return NotFound(new ErrorResponse("Nenhuma compra encontrada"));
            }

            return Ok(ultimaCompra);
        }
        catch (Exception ex)
        { 
            return StatusCode(500, new ErrorResponse(
                "Erro ao listar a última compra",
                ex.Message
            ));
        }
    }

    [HttpGet("relatorio")]
    public async Task<IActionResult> ExportarRelatorio()
    {
        try
        {
            string csv = await _service.GerarRelatorioCsvAsync(); 
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv);

            return File(bytes, "text/csv", "relatorio_compras.csv");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse(
                "Erro ao exportar relatório",
                ex.Message
            ));
        }
    }

}
