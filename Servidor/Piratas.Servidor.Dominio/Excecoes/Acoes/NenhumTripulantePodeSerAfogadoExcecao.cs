namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using System;
    using Dominio.Acoes;

    public class NenhumTripulantePodeSerAfogadoExcecao : BaseAcoesExcecao
    {
        public NenhumTripulantePodeSerAfogadoExcecao(BaseAcao acao, Guid idJogador)
            : base(
                acao,
                "nenhum-tripulante-pode-ser-afogado",
                $"O jogador \"{idJogador}\" não possui tripulante afogável.")
        {
        }
    }
}
