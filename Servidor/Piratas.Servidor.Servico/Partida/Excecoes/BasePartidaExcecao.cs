namespace Piratas.Servidor.Servico.Partida.Excecoes;

public class BasePartidaExcecao : BaseServicoException
{
    public BasePartidaExcecao(string id, string mensagem) : base(id, mensagem)
    {
    }
}
