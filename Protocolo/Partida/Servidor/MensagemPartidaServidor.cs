namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemPartidaServidor : MensagemPartida
    {
        public int AcoesRestantes { get; private set; }

        public int Tesouros { get; private set; }

        public BaseEscolha Escolha { get; private set; }

        public Dictionary<string, List<Evento>> Eventos { get; private set; }

        public string IdJogadorTurnoCorrente { get; private set; }

        public MensagemPartidaServidor(
            string idJogador,
            Guid idMesa,
            int acoesRestantes,
            int tesouros,
            Dictionary<string, List<Evento>> eventos,
            BaseEscolha escolha,
            string idJogadorTurnoCorrente,
            string idErro = null,
            string descricaoErro = null
        ) : base(
            idJogador,
            idMesa,
            idErro,
            descricaoErro)
        {
            Escolha = escolha;
            IdJogadorTurnoCorrente = idJogadorTurnoCorrente;
            AcoesRestantes = acoesRestantes;
            Eventos = eventos;
            Tesouros = tesouros;
        }

        public MensagemPartidaServidor(string idErro, string descricaoErro) : this(
            string.Empty,
            Guid.Empty,
            0,
            0,
            null,
            null,
            string.Empty,
            idErro,
            descricaoErro)
        {
        }
    }
}
