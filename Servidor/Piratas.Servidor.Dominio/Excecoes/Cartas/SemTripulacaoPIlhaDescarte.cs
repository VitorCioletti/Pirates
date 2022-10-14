namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class SemTripulacaoPilhaDescarteException : BaseCartaException
    {
        public SemTripulacaoPilhaDescarteException(Carta cartaJogada)
            : base(cartaJogada, "sem-tripulacao-pilha-descarte", "Sem tripulação na pilha de descarte.")
        {
        }
    }
}
