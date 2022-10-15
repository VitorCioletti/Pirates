namespace Piratas.Servidor.Servico.Partida.Excecoes;

public class BasePartidaException : BaseServicoException
{
    public BasePartidaException(string id, string mensagem) : base(id, mensagem)
    {
    }
}
