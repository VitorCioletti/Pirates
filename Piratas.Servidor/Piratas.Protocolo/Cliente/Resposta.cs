namespace Piratas.Protocolo.Cliente
{
    using System;

    public class Resposta : Pacote
    {
        public Escolha Escolha { get; private set; }

        public Resposta(Guid idJogador, Escolha escolha) : base(idJogador)
        {
            Escolha = escolha;
        }
    }
}
