namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using Protocolo.Cliente.Partida;
    using Protocolo.Servidor.Partida;

    // TODO: Injeção de dependência.
    public static class GerenciadorPartidaServico
    {
        private static Dictionary<Guid, PartidaServico> _partidasEmAndamento { get; set; }

        static GerenciadorPartidaServico()
        {
            _partidasEmAndamento = new Dictionary<Guid, PartidaServico>();
        }

        public static List<MensagemPartidaServidor> ProcessarMensagemCliente(
            MensagemPartidaCliente mensagemPartidaCliente)
        {
            // TODO: Tratar erro caso partida não exista.
            PartidaServico partida = _partidasEmAndamento[mensagemPartidaCliente.IdMesa];

            return partida.ProcessarMensagemCliente(mensagemPartidaCliente);
        }

        public static void CriarPartida(List<Guid> idsJogadores)
        {
            var novaPartida = new PartidaServico(idsJogadores);

            _partidasEmAndamento[novaPartida.IdMesa] = novaPartida;
        }

        public static void RemoverPartida(Guid idPartida)
        {
            // TODO: Tratar erro caso partida não exista.
            _partidasEmAndamento.Remove(idPartida);
        }
    }
}
