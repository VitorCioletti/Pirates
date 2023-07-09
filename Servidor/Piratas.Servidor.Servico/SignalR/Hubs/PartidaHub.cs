namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Partida;
using Protocolo.Partida.Cliente;
using Protocolo.Partida.Servidor;

public class PartidaHub : Hub
{
    public void ProcessarMensagem(MensagemPartidaCliente mensagemPartidaCliente)
    {
        List<MensagemPartidaServidor> mensagensServidor =
            GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemPartidaCliente);

        foreach (MensagemPartidaServidor mensagemPartidaServidor in mensagensServidor)
        {
            string idJogadorRealizador = mensagemPartidaServidor.IdJogadorRealizador;

            Clients.Client(idJogadorRealizador).SendAsync("AoProcessarMensagem", mensagensServidor);
        }
    }
}
