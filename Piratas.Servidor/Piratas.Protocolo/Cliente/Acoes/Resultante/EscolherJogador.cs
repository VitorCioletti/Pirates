namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;

    public class EscolherJogador : Pacote
    {
        public Guid JogadorEscolhido { get; private set; }

        public EscolherJogador(Guid idJogador, Guid jogadorEscolhido) : base(idJogador)
        {
            JogadorEscolhido = jogadorEscolhido;
        }
    }
}
