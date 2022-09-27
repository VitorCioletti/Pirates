namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public abstract class BaseMaoException : BaseRegraException
    {
        protected BaseMaoException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
