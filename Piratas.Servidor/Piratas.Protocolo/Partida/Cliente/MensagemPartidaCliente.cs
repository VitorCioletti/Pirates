namespace Piratas.Protocolo.Partida.Cliente
{
    using System;

    public class MensagemPartidaCliente : BaseMensagemPartida
    {
        public EscolhaPartidaCliente EscolhaPartidaCliente { get; private set; }

        public string IdAcaoExecutada { get; private set; }

        public MensagemPartidaCliente(
            Guid idJogador,
            Guid idMesa,
            string idAcaoExecutada,
            EscolhaPartidaCliente escolhaPartidaCliente)
            : base(idJogador, idMesa)
        {
            IdAcaoExecutada = idAcaoExecutada;
            EscolhaPartidaCliente = escolhaPartidaCliente;
        }
    }
}