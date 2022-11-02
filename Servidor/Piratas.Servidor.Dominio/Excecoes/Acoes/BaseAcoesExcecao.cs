namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public abstract class BaseAcoesExcecao : BaseDominioExcecao
    {
        public BaseAcao BaseAcao { get; private set; }

        protected BaseAcoesExcecao(BaseAcao baseAcao, string id, string mensagem) : base(id, mensagem)
        {
            BaseAcao = baseAcao;
        }
    }
}
