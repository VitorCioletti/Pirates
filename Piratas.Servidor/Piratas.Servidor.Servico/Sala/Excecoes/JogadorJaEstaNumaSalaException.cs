namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;
    using Servico.Excecoes;

    public class JogadorJaEstaNumaSalaException : BaseSalaException
    {
        public JogadorJaEstaNumaSalaException(Guid idJogador) :
            base("jogador-ja-esta-numa-sala", $"O jogador \"{idJogador}\" já está numa sala.")
        {
        }
    }
}
