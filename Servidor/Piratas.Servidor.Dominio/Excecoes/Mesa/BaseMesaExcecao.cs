namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public abstract class BaseMesaExcecao : BaseRegraExcecao
    {
        protected BaseMesaExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
