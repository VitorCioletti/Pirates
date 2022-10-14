namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public abstract class BaseCartaException : BaseRegraException
    {
        public Carta Carta { get; private set; }

        protected BaseCartaException(Carta carta, string id, string mensagem) : base(id, mensagem)
        {
            Carta = carta;
        }
    }
}
