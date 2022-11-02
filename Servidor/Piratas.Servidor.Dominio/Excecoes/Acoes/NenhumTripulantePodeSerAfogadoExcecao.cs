namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using System;
    using Dominio.Acoes;

    public class NenhumTripulantePodeSerAfogadoExcecao : BaseAcoesExcecao
    {
        public NenhumTripulantePodeSerAfogadoExcecao(BaseAcao baseAcao, Guid idJogador)
            : base(
                baseAcao,
                "nenhum-tripulante-pode-ser-afogado",
                $"O jogador \"{idJogador}\" não possui tripulante afogável.")
        {
        }
    }
}
