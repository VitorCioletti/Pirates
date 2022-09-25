namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class TripulacaoCheiaException : CartaException
    {
        public TripulacaoCheiaException(Carta cartaJogada, Jogador jogador)
            : base(cartaJogada, "tripulacao-cheia", $"Tripulação do jogador \"{jogador.Id}\" cheia.")
        {
        }
    }
}
