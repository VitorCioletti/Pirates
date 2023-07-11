namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public abstract class BasePartidaExcecao : BaseServicoExcecao
    {
        protected BasePartidaExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
