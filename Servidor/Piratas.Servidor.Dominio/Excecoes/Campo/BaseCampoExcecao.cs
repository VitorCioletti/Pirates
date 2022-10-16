namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public abstract class BaseCampoExcecao : BaseRegraExcecao
    {
        protected BaseCampoExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
