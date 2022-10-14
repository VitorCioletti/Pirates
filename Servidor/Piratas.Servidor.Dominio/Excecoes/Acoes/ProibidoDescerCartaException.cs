namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class ProibidoDescerCartaException : BaseAcoesException
    {
        public Carta Carta { get; private set; }

        public ProibidoDescerCartaException(Acao acao, Carta carta)
            : base(acao, "proibido-descer-carta", $"Probido jogar cartas do tipo \"{carta.GetType()}\".")
        {
            Carta = carta;
        }
    }
}
