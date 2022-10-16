namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;

    public class JogadorJaEstaNumaSalaExcecao : BaseSalaExcecao
    {
        public JogadorJaEstaNumaSalaExcecao(Guid idJogador) :
            base("jogador-ja-esta-numa-sala", $"O jogador \"{idJogador}\" já está numa sala.")
        {
        }
    }
}
