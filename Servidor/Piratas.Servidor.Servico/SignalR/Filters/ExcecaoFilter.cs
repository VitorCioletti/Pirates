namespace Piratas.Servidor.Servico.SignalR.Filters;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Protocolo;

public class ExcecaoFilter : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch (BaseServicoExcecao servicoException)
        {
            return new Mensagem(Guid.Empty, servicoException.Id, servicoException.Message);
        }
        catch (Exception)
        {
            return new Mensagem(Guid.Empty, "erro-desconhecido", "Ocorreu um erro desconhecido.");
        }
    }
}
