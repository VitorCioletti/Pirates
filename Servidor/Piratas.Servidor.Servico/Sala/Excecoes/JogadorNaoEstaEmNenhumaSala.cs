namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    using System;
    using Servico.Excecoes;

    public class JogadorNaoEstaEmNenhumaSala : BaseSalaException
    {
        public JogadorNaoEstaEmNenhumaSala(Guid idJogador) :
            base("jogador-nao-esta-na-sala", $"Jogador de id \"{idJogador}\" não está em nenhuma sala.")
        {
        }
    }
}
