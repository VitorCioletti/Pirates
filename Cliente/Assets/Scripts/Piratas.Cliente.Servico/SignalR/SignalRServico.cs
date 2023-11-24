namespace Piratas.Cliente.Servico
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using Protocolo.Partida.Servidor;
    using Protocolo.Sala.Servidor;

    public static class SignalRServico
    {
        private static HubConnection _hubConnection;

        private static List<ISalaOuvinte> _salaOuvintes;

        static SignalRServico()
        {
            _salaOuvintes = new List<ISalaOuvinte>();
        }

        public static void Inicializar()
        {
            string endereco = "http://localhost:5000/sala";

            var hubConnectionBuilder = new HubConnectionBuilder();

            hubConnectionBuilder.WithUrl(endereco);
            hubConnectionBuilder.WithAutomaticReconnect();

            _hubConnection = hubConnectionBuilder.Build();

            _registrarEventosConexao();
            _registrarHubs();
        }

        public static async Task ConectarAsync() => await _hubConnection.StartAsync();

        public static async Task DesconectarAsync() => await _hubConnection.StopAsync();

        public static void RegistrarSalaOuvinte(ISalaOuvinte salaOuvinte)
        {
            _salaOuvintes.Add(salaOuvinte);
        }

        public static void RemoverSalaOuvinte(ISalaOuvinte salaOuvinte)
        {
            _salaOuvintes.Remove(salaOuvinte);
        }

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
            _hubConnection.On<MensagemPartidaServidor>(nameof(AoProcessarMensagem), AoProcessarMensagem);
        }

        private static void _registrarSalaHub()
        {
            _hubConnection.On<MensagemSalaServidor>("AoCriar", _aoCriar);
            _hubConnection.On<MensagemSalaServidor>("AoSair", _aoSair);
            _hubConnection.On<MensagemSalaServidor>("AoEntrar", _aoEntrar);
            _hubConnection.On<MensagemSalaServidor>("AoIniciarPartida", _aoIniciarPartida);
        }

        private static void _aoIniciarPartida(MensagemSalaServidor mensagemSalaServidor)
        {
            foreach (ISalaOuvinte ouvinte in _salaOuvintes)
                ouvinte.AoIniciarPartida(mensagemSalaServidor);
        }

        private static void _aoEntrar(MensagemSalaServidor mensagemSalaServidor)
        {
            foreach (ISalaOuvinte ouvinte in _salaOuvintes)
                ouvinte.AoEntrar(mensagemSalaServidor);
        }

        private static void _aoSair(MensagemSalaServidor mensagemSalaServidor)
        {
            foreach (ISalaOuvinte ouvinte in _salaOuvintes)
                ouvinte.AoSair(mensagemSalaServidor);
        }

        private static void _aoCriar(MensagemSalaServidor mensagemSalaServidor)
        {
            foreach (ISalaOuvinte ouvinte in _salaOuvintes)
                ouvinte.AoCriar(mensagemSalaServidor);
        }

        private static void AoProcessarMensagem(MensagemPartidaServidor mensagemPartidaServidor)
        {
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
