namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public abstract class BaseCampoException : BaseRegraException
    {
        public BaseCampoException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
