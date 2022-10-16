namespace Piratas.Servidor.Dominio.Excecoes.Acoes

{
    using System;
    using Dominio.Acoes;

    public class NaoPossuiTripulacaoExcecao : BaseAcoesExcecao
    {
        public NaoPossuiTripulacaoExcecao(Acao acao, Guid idJogador) :
            base(acao, "nao-possui-tripulacao", $"Jogador \"{idJogador}\" não possui tripulação.")
        {
        }
    }
}
