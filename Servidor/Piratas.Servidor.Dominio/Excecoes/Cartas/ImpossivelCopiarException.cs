namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ImpossivelCopiarException : BaseCartaException
    {
        public ImpossivelCopiarException(Carta cartaJogada, Carta cartaCopiada)
            : base(cartaJogada, "impossivel-copiar-carta", $"Não é possível copiar carta \"{cartaCopiada.Id}\".")
        {
        }
    }
}
