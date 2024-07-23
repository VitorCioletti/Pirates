namespace Pirates.Server.Service.SignalR.Hubs;

using System.Collections.Generic;
using System.Threading.Tasks;
using Match;
using Microsoft.AspNetCore.SignalR;
using Protocol.Match.Client;
using Protocol.Match.Server;

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
