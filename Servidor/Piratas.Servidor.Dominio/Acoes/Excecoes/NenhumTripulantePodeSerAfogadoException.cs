namespace Piratas.Servidor.Dominio.Acoes.Excecoes;

using System;

public class NenhumTripulantePodeSerAfogadoException : BaseAcoesException
{
    public NenhumTripulantePodeSerAfogadoException(Guid idJogador)
        : base("nenhum-tripulante-pode-ser-afogado", $"O jogador \"{idJogador}\" não possui tripulante afogável.")
    {
    }
}
