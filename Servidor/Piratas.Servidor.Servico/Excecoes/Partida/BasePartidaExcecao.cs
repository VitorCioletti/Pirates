namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public abstract class BasePartidaExcecao : BaseServicoException
    {
        protected BasePartidaExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
