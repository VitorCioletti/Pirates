namespace Piratas.Protocolo.Sala.Servidor
{
    using System;
    using System.Collections.Generic;

    public class MensagemSalaServidor : Mensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public string IdJogadorRealizouAcao { get; private set; }

        public List<string> Jogadores { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            string idJogadorRealizouAcao,
            Guid idPartida,
            List<string> jogadores,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            IdPartida = idPartida;
            Jogadores = jogadores;
        }

        public MensagemSalaServidor(
            List<string> jogadores,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            Jogadores = jogadores;
            IdPartida = Guid.Empty;
            IdSala = Guid.Empty;
            IdJogadorRealizouAcao = String.Empty;
        }
    }
}
