namespace Piratas.Servidor.Dominio.Acoes.Excecoes
{
    using Dominio.Excecoes;

    public abstract class BaseAcoesException : BaseRegraException
    {
        protected BaseAcoesException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
