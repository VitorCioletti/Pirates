namespace Piratas.Protocolo.Partida.Cliente
{
    using System;

    public class MensagemPartidaCliente : BaseMensagemPartida
    {
        public BaseEscolha Escolha { get; private set; }

        public string IdAcaoExecutada { get; private set; }

        public MensagemPartidaCliente(
            Guid idJogador,
            Guid idMesa,
            string idAcaoExecutada,
            BaseEscolha escolha)
            : base(idJogador, idMesa)
        {
            IdAcaoExecutada = idAcaoExecutada;
            Escolha = escolha;
        }
    }
}
