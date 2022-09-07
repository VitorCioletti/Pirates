namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using Cartas;

    public class DanificarEmbarcacao : Pacote
    {
        public Embarcacao Embarcacao { get; private set; }

        public DanificarEmbarcacao(Guid idJogador, Embarcacao embarcacao) : base(idJogador)
        {
            Embarcacao = embarcacao;
        }
    }
}
