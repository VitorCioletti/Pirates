namespace Piratas.Protocolo.Servidor.Partida
{
    using System;
    using System.Collections.Generic;

    public class MensagemPartidaServidor : BaseMensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public EscolhaServidor EscolhaServidor { get; private set; }

        public Dictionary<Guid, List<Evento>> Eventos { get; private set; }

        public string IdErro { get; private set; }

        public bool PossuiErro => !string.IsNullOrWhiteSpace(IdErro);

        public MensagemPartidaServidor(
            Guid idJogador,
            Guid idMesa,
            int acoesRestantes,
            int tesouros,
            Dictionary<Guid, List<Evento>> eventos,
            EscolhaServidor escolhaServidor,
            string idErro
        ) : base(idJogador, idMesa)
        {
            EscolhaServidor = escolhaServidor;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
            IdErro = idErro;
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
