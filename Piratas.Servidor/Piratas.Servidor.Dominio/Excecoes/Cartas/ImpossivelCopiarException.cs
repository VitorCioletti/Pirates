namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ImpossivelCopiarException : CartaException
    {
        public ImpossivelCopiarException(Carta cartaJogada, Carta cartaCopiada)
            : base(cartaJogada, $"Não é possível copiar carta \"{cartaCopiada.Nome}\".")
        {
        }
    }
}
