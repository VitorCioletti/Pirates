namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ImpossivelDescerException : CartaException
    {
        public ImpossivelDescerException(Carta cartaJogada)
            : base(cartaJogada, $"Não é possível descer \"{cartaJogada.Id}\".")
        {
        }
    }
}
