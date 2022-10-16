namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;

    public class JogadorNaoEstaEmNenhumaSala : BaseSalaExcecao
    {
        public JogadorNaoEstaEmNenhumaSala(Guid idJogador) :
            base("jogador-nao-esta-na-sala", $"Jogador de id \"{idJogador}\" não está em nenhuma sala.")
        {
        }
    }
}
