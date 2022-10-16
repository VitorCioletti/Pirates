namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ImpossivelDescerExcecao : BaseCartaExcecao
    {
        public ImpossivelDescerExcecao(Carta cartaJogada)
            : base(cartaJogada, "impossivel-descer-carta", $"Não é possível descer \"{cartaJogada.Id}\".")
        {
        }
    }
}
