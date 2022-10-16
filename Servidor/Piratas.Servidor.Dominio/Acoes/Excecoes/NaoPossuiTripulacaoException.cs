namespace Piratas.Servidor.Dominio.Acoes.Excecoes;

using System;

public class NaoPossuiTripulacaoException : BaseAcoesException
{
    public NaoPossuiTripulacaoException(Guid idJogador) :
        base("nao-possui-tripulacao", $"Jogador \"{idJogador}\" não possui tripulação.")
    {
    }
}
