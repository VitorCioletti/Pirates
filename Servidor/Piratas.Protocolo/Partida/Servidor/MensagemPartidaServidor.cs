namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemPartidaServidor : BaseMensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public BaseEscolha Escolha { get; private set; }

        public Dictionary<string, List<Evento>> Eventos { get; private set; }

        public MensagemPartidaServidor(
            string idJogador,
            Guid idMesa,
            Guid idMensagemSolicitante,
            int acoesRestantes,
            int tesouros,
            Dictionary<string, List<Evento>> eventos,
            BaseEscolha escolha,
            string idErro = null,
            string descricaoErro = null
        ) : base(
            idJogador,
            idMesa,
            idMensagemSolicitante,
            idErro,
            descricaoErro)
        {
            Escolha = escolha;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
        }

        public MensagemPartidaServidor(Guid idMensagemSolicitante, string idErro, string descricaoErro) : this(
            string.Empty,
            Guid.Empty,
            idMensagemSolicitante,
            0,
            0,
            null,
            null,
            idErro,
            descricaoErro)
        {
        }
    }
}
