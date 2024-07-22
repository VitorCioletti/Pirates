namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Partida;
using Protocolo.Partida.Cliente;
using Protocolo.Partida.Servidor;

public class MatchHub : Hub
{
    public async Task ProcessMessage(ClientMatchMessage clientMatchMessage)
    {
        List<ServerMatchMessage> serverMessages = MatchServiceManager.ProcessClientMessage(clientMatchMessage);

        List<Task> allSendAsyncTasks = new();

        foreach (ServerMatchMessage serverMessage in serverMessages)
        {
            string idStarterPlayer = serverMessage.IdStarterPlayer;

            Task sendAsync = Clients.Client(idStarterPlayer).SendAsync("OnProcessMessage", serverMessage);

            allSendAsyncTasks.Add(sendAsync);
        }

        await Task.WhenAll(allSendAsyncTasks);
    }
}
