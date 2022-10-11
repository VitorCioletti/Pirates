namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;
    using BaseInternal;

    public class MensagemPartidaServidor : BaseMensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public EscolhaServidor EscolhaServidor { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public MensagemPartidaServidor(
            Guid idJogador,
            Guid idMesa,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
            EscolhaServidor escolhaServidor,
            string idErro = null
        ) : base(idJogador, idMesa, idErro)
        {
            EscolhaServidor = escolhaServidor;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
        }

        public MensagemPartidaServidor(string idErro) : this(
            Guid.Empty,
            Guid.Empty,
            0,
            0,
            null,
            null,
            idErro)
        {
        }
    }
}
