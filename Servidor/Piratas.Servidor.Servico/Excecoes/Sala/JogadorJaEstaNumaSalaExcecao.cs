namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    using System;

    public class JogadorJaEstaNumaSalaExcecao : BaseSalaExcecao
    {
        public JogadorJaEstaNumaSalaExcecao(string idJogador) :
            base("jogador-ja-esta-numa-sala", $"O jogador \"{idJogador}\" já está numa sala.")
        {
        }
    }
}
