namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using Excecoes;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Servidor;

    public static class GerenciadorPartidaServico
    {
        private static Dictionary<Guid, PartidaServico> _partidasEmAndamento { get; }

        static GerenciadorPartidaServico()
        {
            _partidasEmAndamento = new Dictionary<Guid, PartidaServico>();
        }

        public static List<MensagemPartidaServidor> ProcessarMensagemCliente(
            MensagemPartidaCliente mensagemPartidaCliente)
        {
            Guid idPartida = mensagemPartidaCliente.IdMesa;

            _verificarPartidaExistente(idPartida);

            PartidaServico partida = _partidasEmAndamento[idPartida];

            return partida.ProcessarMensagemCliente(mensagemPartidaCliente);
        }

        // TODO: Verificar se a quantidade mínima de jogadores foi atingida
        public static Guid CriarPartida(List<Guid> idsJogadores)
        {
            var novaPartida = new PartidaServico(idsJogadores);

            _partidasEmAndamento[novaPartida.Id] = novaPartida;

            return novaPartida.Id;
        }

        public static void RemoverPartida(Guid idPartida)
        {
            _verificarPartidaExistente(idPartida);

            _partidasEmAndamento.Remove(idPartida);
        }

        private static void _verificarPartidaExistente(Guid idPartida)
        {
            if (!_partidasEmAndamento.ContainsKey(idPartida))
                throw new PartidaNaoEncontradaException(idPartida);
        }
    }
}