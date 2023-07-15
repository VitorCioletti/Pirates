namespace Piratas.Servidor.Servico.SignalR.Filters;

using System;
using System.Threading.Tasks;
using Log;
using Microsoft.AspNetCore.SignalR;
using Protocolo;

public class ExcecaoFiltro : IHubFilter
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
            LogServico.Logger.Debug(servicoException, servicoException.Message);

            var mensagemErro = new Mensagem(servicoException.Id, servicoException.Message);

            await invocationContext.Hub.Clients.Caller.SendAsync($"Ao{invocationContext.HubMethodName}", mensagemErro);
        }
        catch (Exception e)
        {
            LogServico.Logger.Error(e, "Erro desconhecido.");

            var mensagemErro = new Mensagem("erro-desconhecido", "Ocorreu um erro desconhecido.");

            await invocationContext.Hub.Clients.Caller.SendAsync($"Ao{invocationContext.HubMethodName}", mensagemErro);
        }

        return null;
    }
}
