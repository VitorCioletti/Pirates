namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    public abstract class BaseSalaExcecao : BaseServicoException
    {
        protected BaseSalaExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
