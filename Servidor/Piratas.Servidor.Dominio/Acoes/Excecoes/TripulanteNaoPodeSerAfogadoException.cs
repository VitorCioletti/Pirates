namespace Piratas.Servidor.Dominio.Acoes.Excecoes;

public class TripulanteNaoPodeSerAfogadoException : BaseAcoesException
{
    private const string _idExcecao = "tripulante-nao-pode-ser-afogado";

    public TripulanteNaoPodeSerAfogadoException(string idTripulante)
        : base(_idExcecao, $"O tripulante \"{idTripulante}\" não pode ser afogado.")
    {
    }

    public TripulanteNaoPodeSerAfogadoException(string idTripulante, string idCartaRazaoAfogamento)
        : base(_idExcecao, $"O tripulante \"{idTripulante}\" não pode ser afogado por \"{idCartaRazaoAfogamento}\".")
    {
    }
}
