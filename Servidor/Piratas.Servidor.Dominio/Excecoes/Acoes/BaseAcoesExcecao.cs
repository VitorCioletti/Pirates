namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public abstract class BaseAcoesExcecao : BaseRegraExcecao
    {
        public Acao Acao { get; private set; }

        protected BaseAcoesExcecao(Acao acao, string id, string mensagem) : base(id, mensagem)
        {
            Acao = acao;
        }
    }
}
