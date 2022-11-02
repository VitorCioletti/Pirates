namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using System;
    using Dominio.Acoes;

    public class NaoPossuiTripulacaoExcecao : BaseAcoesExcecao
    {
        public NaoPossuiTripulacaoExcecao(BaseAcao baseAcao, Guid idJogador) :
            base(baseAcao, "nao-possui-tripulacao", $"Jogador \"{idJogador}\" não possui tripulação.")
        {
        }
    }
}
