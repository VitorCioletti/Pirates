namespace Piratas.Servidor.Servico.SignalR.Filters;

using System;
using System.Threading.Tasks;
using Log;
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
            return new Mensagem(servicoException.Id, servicoException.Message);
        }
        catch (Exception e)
        {
            LogServico.Logger.Error(e, "Erro desconhecido.");

            return new Mensagem("erro-desconhecido", "Ocorreu um erro desconhecido.");
        }
    }
}
