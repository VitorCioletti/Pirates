namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class ApenasCartaRespostaDueloExcecao : BaseCartaExcecao
    {
        public ApenasCartaRespostaDueloExcecao(Carta cartaJogada)
            : base(
                cartaJogada,
                "carta-apenas-resposta-duelo",
                $"Carta \"{cartaJogada.Id}\" só pode ser usada em duelo.")
        {
        }
    }
}
