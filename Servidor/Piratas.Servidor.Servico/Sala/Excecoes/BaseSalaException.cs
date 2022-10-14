namespace Piratas.Servidor.Servico.Excecoes
{
    public class BaseSalaException : BaseServicoException
    {
        public BaseSalaException(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
