namespace Pirates.Server.Service.SignalR.Filters;

using System;
using System.Threading.Tasks;
using Log;
using Microsoft.AspNetCore.SignalR;
using Protocol;

public class ExceptionFilter : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch (BaseServiceException serviceException)
        {
            LogService.Logger.Debug(serviceException, serviceException.Message);

            var errorMessage = new Message(serviceException.Id, serviceException.Message);

            await invocationContext.Hub.Clients.Caller.SendAsync($"On{invocationContext.HubMethodName}", errorMessage);
        }
        catch (Exception e)
        {
            LogService.Logger.Error(e, "Unknown error.");

            var errorMessage = new Message("unknown-error", "An unknown error has happened.");

            await invocationContext.Hub.Clients.Caller.SendAsync($"On{invocationContext.HubMethodName}", errorMessage);
        }

        return null;
    }
}
