namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemPartidaServidor : BaseMensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public BaseEscolha Escolha { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public MensagemPartidaServidor(
            Guid idJogador,
            Guid idMesa,
            Guid idMensagemSolicitante,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
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
            Guid.Empty,
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
