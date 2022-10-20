namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemPartidaServidor : BaseMensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public BaseEscolha BaseEscolha { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public MensagemPartidaServidor(
            Guid idJogador,
            Guid idMesa,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
            BaseEscolha baseEscolha,
            string idErro = null,
            string descricaoErro = null
        ) : base(
            idJogador,
            idMesa,
            idErro,
            descricaoErro)
        {
            BaseEscolha = baseEscolha;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
        }

        public MensagemPartidaServidor(string idErro, string descricaoErro) : this(
            Guid.Empty,
            Guid.Empty,
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
