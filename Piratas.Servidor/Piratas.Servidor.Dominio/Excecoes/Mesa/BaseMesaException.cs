namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public abstract class BaseMesaException : BaseRegraException
    {
        protected BaseMesaException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
