namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Protocolo.Sala.Servidor;
using Sala;

public class RoomHub : Hub
{
    public async Task Create()
    {
        string playerId = Context.ConnectionId;

        Guid roomId = RoomService.Create(Context.ConnectionId);

        await Groups.AddToGroupAsync(playerId, roomId.ToString());

        var roomServerMessage = new RoomServerMessage(
            roomId,
            playerId,
            Guid.Empty,
            new List<string>());

        await Clients.Caller.SendAsync("OnCreate", roomServerMessage);
    }

    public async Task Exit()
    {
        string playerId = Context.ConnectionId;

        Guid roomId = RoomService.Exit(playerId);

        await Groups.RemoveFromGroupAsync(playerId, roomId.ToString());

        IClientProxy group = Clients.Group(roomId.ToString());

        List<string> players = RoomService.GetPlayers(roomId);

        var roomServerMessage = new RoomServerMessage(
            roomId,
            playerId,
            Guid.Empty,
            players);

        await group.SendAsync("OnExit", roomServerMessage);
    }

    public async Task Enter(Guid roomId)
    {
        string playerId = Context.ConnectionId;

        RoomService.Enter(playerId, roomId);

        await Groups.AddToGroupAsync(playerId, roomId.ToString());

        IClientProxy group = Clients.Group(roomId.ToString());

        List<string> players = RoomService.GetPlayers(roomId);

        var roomServerMessage = new RoomServerMessage(
            roomId,
            playerId,
            Guid.Empty,
            players);

        await group.SendAsync("OnEnter", roomServerMessage);
    }

    public async Task StartMatch(Guid roomId)
    {
        string playerId = Context.ConnectionId;

        Guid matchId = RoomService.StartMatch(playerId, roomId);

        IClientProxy group = Clients.Group(roomId.ToString());

        List<string> players = RoomService.GetPlayers(roomId);

        var roomServerMessage = new RoomServerMessage(
            roomId,
            playerId,
            matchId,
            players);

        await group.SendAsync("OnStartMatch", roomServerMessage);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Exit();
    }
}
