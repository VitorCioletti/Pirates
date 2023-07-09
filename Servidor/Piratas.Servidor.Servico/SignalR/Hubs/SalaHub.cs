namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sala;

public class SalaHub : Hub
{
    public async Task Criar()
    {
        string idJogador = Context.ConnectionId;

        Guid idNovaSala = SalaServico.Criar(Context.ConnectionId);

        await Groups.AddToGroupAsync(idJogador, idNovaSala.ToString());

        await Clients.Caller.SendAsync("AoCriar", idNovaSala);
    }

    public async Task Sair()
    {
        string idJogador = Context.ConnectionId;

        Guid idSala = SalaServico.Sair(idJogador);

        await Groups.RemoveFromGroupAsync(idJogador, idSala.ToString());

        await Clients.Others.SendAsync("AoSair", idJogador);
    }

    public async Task Entrar(Guid idSala)
    {
        string idJogador = Context.ConnectionId;

        SalaServico.Entrar(idJogador, idSala);

        await Groups.AddToGroupAsync(idJogador, idSala.ToString());

        await Clients.All.SendAsync("AoEntrar", idJogador);
    }

    public async Task IniciarPartida()
    {
        Guid idPartida = SalaServico.IniciarPartida(Context.ConnectionId);

        await Clients.All.SendAsync("AoIniciarPartida", idPartida);
    }
}
