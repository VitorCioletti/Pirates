namespace Piratas.Protocolo.Cliente
{
    using System;

    public class MensagemCliente : Mensagem
    {
        public Escolha Escolha { get; private set; }

        public MensagemCliente(Guid idJogador, Guid idMesa, Escolha escolha) : base(idJogador, idMesa)
        {
            Escolha = escolha;
        }
    }
}
