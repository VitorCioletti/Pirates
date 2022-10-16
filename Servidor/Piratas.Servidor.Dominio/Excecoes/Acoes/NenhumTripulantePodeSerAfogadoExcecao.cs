namespace Piratas.Servidor.Dominio.Excecoes.Acoes;

using System;
using Dominio.Acoes;

public class NenhumTripulantePodeSerAfogadoExcecao : BaseAcoesExcecao
{
    public NenhumTripulantePodeSerAfogadoExcecao(Acao acao, Guid idJogador)
        : base(acao, "nenhum-tripulante-pode-ser-afogado", $"O jogador \"{idJogador}\" não possui tripulante afogável.")
    {
    }
}
