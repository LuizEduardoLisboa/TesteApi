using ApiTeste.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTeste.Services.Interfaces
{
    public interface ICompraService
    {
        Task<Compra> CriarAsync(CriarCompraDto dto);
        Task<List<Compra>> ListarAsync();
        Task<Compra?> ObterUltimaAsync();
        Task<string> GerarRelatorioCsvAsync();
    }
}