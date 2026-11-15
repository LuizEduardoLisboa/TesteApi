using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTeste.Data;
using ApiTeste.Domain;
using ApiTeste.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CompraService : ICompraService
{
    private readonly AppDbContext _context;

    public CompraService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Compra> CriarAsync(CriarCompraDto dto)
    {
        var compra = new Compra
        {
            UsuarioId = dto.UsuarioId,
            ProdutoId = dto.ProdutoId,
            DataCompra = DateTime.UtcNow
        };

        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return compra;
    }

    public async Task<List<Compra>> ListarAsync()
    {
        return await _context.Compras
            .Include(c => c.Usuario)
            .Include(c => c.Produto)
            .AsNoTracking() 
            .ToListAsync();
    }

    public async Task<Compra?> ObterUltimaAsync()
    {
        return await _context.Compras
            .Include(c => c.Usuario)
            .Include(c => c.Produto)
            .AsNoTracking()
            .OrderByDescending(c => c.DataCompra)
            .FirstOrDefaultAsync();
    }

    public async Task<string> GerarRelatorioCsvAsync()
    {
        var compras = await ListarAsync();
        var builder = new StringBuilder();

        builder.AppendLine("ID da Compra;Data;Usuario;Produto");

        foreach (var c in compras)
        {
            var nomeUsuario = c.Usuario?.Nome ?? "N/A";
            var nomeProduto = c.Produto?.Nome ?? "N/A";
            var data = c.DataCompra.ToString("dd/MM/yyyy HH:mm");

            builder.AppendLine($"{c.Id};{data};{nomeUsuario};{nomeProduto}");
        }

        return builder.ToString();
    }
}