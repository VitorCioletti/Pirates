namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public abstract class BaseCartaExcecao : BaseRegraExcecao
    {
        public Carta Carta { get; private set; }

        protected BaseCartaExcecao(Carta carta, string id, string mensagem) : base(id, mensagem)
        {
            Carta = carta;
        }
    }
}
