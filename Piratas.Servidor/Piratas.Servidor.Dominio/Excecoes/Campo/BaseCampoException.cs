namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class BaseCampoException : BaseRegraException
    {
        public BaseCampoException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
