namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class SemTripulacaoPilhaDescarteException : CartaException
    {
        public SemTripulacaoPilhaDescarteException(Carta cartaJogada) 
            : base(cartaJogada, "Sem tripulação na pilha de descarte.")
        {
        }
    }
}