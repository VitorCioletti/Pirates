namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using Protocolo.Acoes;

    public class CopiarPrimaria : Pacote
    {
        public Primaria Primaria { get; private set; }

        public CopiarPrimaria(Guid idJogador, Primaria primaria) : base(idJogador)
        {
            Primaria = primaria;
        }
    }
}
