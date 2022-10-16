namespace Piratas.Servidor.Servico.Sala.Excecoes
{
    public class BaseSalaExcecao : BaseServicoException
    {
        public BaseSalaExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
