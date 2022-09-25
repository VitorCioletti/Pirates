namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public abstract class MesaException : BaseRegraException
    {
        protected MesaException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
