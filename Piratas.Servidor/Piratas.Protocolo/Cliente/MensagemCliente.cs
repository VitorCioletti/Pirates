namespace Piratas.Protocolo.Cliente
{
    using System;

    public class MensagemCliente : Mensagem
    {
        public EscolhaCliente EscolhaCliente { get; private set; }

        public string IdAcaoExecutada { get; private set; }

        public MensagemCliente(
            Guid idJogador,
            Guid idMesa,
            string idAcaoExecutada,
            EscolhaCliente escolhaCliente)
            : base(idJogador, idMesa)
        {
            IdAcaoExecutada = idAcaoExecutada;
            EscolhaCliente = escolhaCliente;
        }
    }
}
