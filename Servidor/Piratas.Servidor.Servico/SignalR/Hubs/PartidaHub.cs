namespace Piratas.Servidor.Servico.SignalR.Hubs;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Partida;
using Protocolo.Partida.Cliente;
using Protocolo.Partida.Servidor;

public class PartidaHub : Hub
{
    public async Task ProcessarMensagem(MensagemPartidaCliente mensagemPartidaCliente)
    {
        List<MensagemPartidaServidor> mensagensServidor =
            GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemPartidaCliente);

        List<Task> allSendAsyncTasks = new();

        foreach (MensagemPartidaServidor mensagemPartidaServidor in mensagensServidor)
        {
            string idJogadorRealizador = mensagemPartidaServidor.IdJogadorRealizador;

            Task sendAsync = Clients.Client(idJogadorRealizador).SendAsync("AoProcessarMensagem", mensagensServidor);

            allSendAsyncTasks.Add(sendAsync);
        }

        await Task.WhenAll(allSendAsyncTasks);
    }
}
