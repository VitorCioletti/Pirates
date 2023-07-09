namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using System;
    using Dominio.Acoes;

    public class NaoPossuiTripulacaoExcecao : BaseAcoesExcecao
    {
        public NaoPossuiTripulacaoExcecao(BaseAcao acao, string idJogador) :
            base(acao, "nao-possui-tripulacao", $"Jogador \"{idJogador}\" não possui tripulação.")
        {
        }
    }
}
