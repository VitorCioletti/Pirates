namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Protocolo.Sala.Servidor;
using Sala;

public class SalaHub : Hub
{
    public async Task Criar()
    {
        string idJogador = Context.ConnectionId;

        Guid idNovaSala = SalaServico.Criar(Context.ConnectionId);

        await Groups.AddToGroupAsync(idJogador, idNovaSala.ToString());

        var mensagemSala = new MensagemSalaServidor(
            idNovaSala,
            idJogador,
            Guid.Empty,
            new List<string>());

        await Clients.Caller.SendAsync("AoCriar", mensagemSala);
    }

    public async Task Sair()
    {
        string idJogador = Context.ConnectionId;

        Guid idSala = SalaServico.Sair(idJogador);

        await Groups.RemoveFromGroupAsync(idJogador, idSala.ToString());

        IClientProxy group = Clients.Group(idSala.ToString());

        List<string> jogadoresSala = SalaServico.ObterJogadoresSala(idSala);

        var mensagemSala = new MensagemSalaServidor(
            idSala,
            idJogador,
            Guid.Empty,
            jogadoresSala);

        await group.SendAsync("AoSair", mensagemSala);
    }

    public async Task Entrar(Guid idSala)
    {
        string idJogador = Context.ConnectionId;

        SalaServico.Entrar(idJogador, idSala);

        await Groups.AddToGroupAsync(idJogador, idSala.ToString());

        IClientProxy group = Clients.Group(idSala.ToString());

        List<string> jogadoresSala = SalaServico.ObterJogadoresSala(idSala);

        var mensagemSala = new MensagemSalaServidor(
            idSala,
            idJogador,
            Guid.Empty,
            jogadoresSala);

        await group.SendAsync("AoEntrar", mensagemSala);
    }

    public async Task IniciarPartida(Guid idSala)
    {
        string idJogador = Context.ConnectionId;

        Guid idPartida = SalaServico.IniciarPartida(idJogador, idSala);

        IClientProxy group = Clients.Group(idSala.ToString());

        List<string> jogadoresSala = SalaServico.ObterJogadoresSala(idSala);

        var mensagemSala = new MensagemSalaServidor(
            idSala,
            idJogador,
            idPartida,
            jogadoresSala);

        await group.SendAsync("AoIniciarPartida", mensagemSala);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Sair();
    }
}
