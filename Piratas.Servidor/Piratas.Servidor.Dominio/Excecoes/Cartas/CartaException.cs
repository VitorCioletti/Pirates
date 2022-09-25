namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public abstract class CartaException : BaseRegraException
    {
        public Carta Carta { get; private set; }

        protected CartaException(Carta carta, string id, string mensagem) : base(id, mensagem)
        {
            Carta = carta;
        }
    }
}
