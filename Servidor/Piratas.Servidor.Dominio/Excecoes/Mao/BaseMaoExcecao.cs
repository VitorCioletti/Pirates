namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public abstract class BaseMaoExcecao : BaseDominioExcecao
    {
        protected BaseMaoExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
