namespace Piratas.Cliente.Servicos
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hubs;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Protocolo;
    using Protocolo.Partida.Servidor;

    public static class SignalRServico
    {
        public static List<Mensagem> MensagensRecebidas { get; private set; }

        private static HubConnection _hubConnection;

        static SignalRServico()
        {
            MensagensRecebidas = new List<Mensagem>();
        }

        public static void Inicializar()
        {
            string endereco = "ws://localhost:8182/signalr/sala";

            var hubConnectionBuilder = new HubConnectionBuilder();

            hubConnectionBuilder.WithUrl(endereco);
            hubConnectionBuilder.WithAutomaticReconnect();
            hubConnectionBuilder.AddMessagePackProtocol();

            _hubConnection = hubConnectionBuilder.Build();

            _registrarEventosConexao();
            _registrarHubs();
        }

        public static async Task ConectarAsync() => await _hubConnection.StartAsync();

        public static void EntrarSala(Guid idSala)
        {
            _hubConnection.SendAsync("Entrar", idSala);
        }

        public static void SairSala()
        {
            _hubConnection.SendAsync("Sair");
        }

        public static void CriarSala()
        {
            _hubConnection.SendAsync("Criar");
        }

        public static void IniciarPartida(Guid idSala)
        {
            _hubConnection.SendAsync("IniciarPartida", idSala);
        }

        private static void _registrarEventosConexao()
        {
            _hubConnection.Closed += _aoFechar;
            _hubConnection.Reconnected += _aoReconectar;
            _hubConnection.Reconnecting += _aoIniciarReconexao;
        }

        private static void _registrarHubs()
        {
            _registrarPartidaHub();
            _registrarSalaHub();
        }

        private static void _registrarPartidaHub()
        {
            var salaHub = new PartidaHub();

            _hubConnection.On<MensagemPartidaServidor>(
                nameof(salaHub.AoProcessarMensagem),
                salaHub.AoProcessarMensagem);
        }

        private static void _registrarSalaHub()
        {
            var salaHub = new SalaHub();

            _hubConnection.On<Guid>(nameof(salaHub.AoCriar), salaHub.AoCriar);
            _hubConnection.On<string>(nameof(salaHub.AoSair), salaHub.AoSair);
            _hubConnection.On<string>(nameof(salaHub.AoEntrar), salaHub.AoEntrar);
            _hubConnection.On<Guid>(nameof(salaHub.AoIniciarPartida), salaHub.AoIniciarPartida);
        }

        private static Task _aoIniciarReconexao(Exception exception)
        {
            return Task.CompletedTask;
        }

        private static Task _aoReconectar(string idNovaConexao)
        {
            return Task.CompletedTask;
        }

        private static Task _aoFechar(Exception exception)
        {
            return Task.CompletedTask;
        }
    }
}
