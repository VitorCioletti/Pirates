namespace Piratas.Protocolo.Partida.Cliente
{
    using System;

    public class MensagemPartidaCliente : MensagemPartida
    {
        public TipoMensagemCliente TipoMensagem { get; private set; }

        public BaseEscolha Escolha { get; private set; }

        public string IdAcaoExecutada { get; private set; }

        public MensagemPartidaCliente(
            string idJogador,
            Guid idMesa,
            string idAcaoExecutada,
            BaseEscolha escolha,
            TipoMensagemCliente tipoMensagem)
            : base(idJogador, idMesa)
        {
            IdAcaoExecutada = idAcaoExecutada;
            Escolha = escolha;
            TipoMensagem = tipoMensagem;
        }
    }
}
