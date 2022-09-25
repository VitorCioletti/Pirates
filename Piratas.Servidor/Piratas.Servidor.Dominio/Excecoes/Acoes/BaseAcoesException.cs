namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public abstract class BaseAcoesException : BaseRegraException
    {
        public Acao Acao { get; private set; }

        protected BaseAcoesException(Acao acao, string id, string mensagem) : base(id, mensagem)
        {
            Acao = acao;
        }
    }
}
