namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ImpossivelDescerException : BaseCartaException
    {
        public ImpossivelDescerException(Carta cartaJogada)
            : base(cartaJogada, "impossivel-descer-carta", $"Não é possível descer \"{cartaJogada.Id}\".")
        {
        }
    }
}
