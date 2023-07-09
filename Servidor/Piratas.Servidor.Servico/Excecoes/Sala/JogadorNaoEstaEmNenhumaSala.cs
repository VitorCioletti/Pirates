namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    using System;

    public class JogadorNaoEstaEmNenhumaSala : BaseSalaExcecao
    {
        public JogadorNaoEstaEmNenhumaSala(string idJogador) :
            base("jogador-nao-esta-na-sala", $"Jogador de id \"{idJogador}\" não está em nenhuma sala.")
        {
        }
    }
}
