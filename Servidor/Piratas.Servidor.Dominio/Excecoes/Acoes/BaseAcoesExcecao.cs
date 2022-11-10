namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public abstract class BaseAcoesExcecao : BaseDominioExcecao
    {
        public BaseAcao Acao { get; private set; }

        protected BaseAcoesExcecao(BaseAcao acao, string id, string mensagem) : base(id, mensagem)
        {
            Acao = acao;
        }
    }
}
