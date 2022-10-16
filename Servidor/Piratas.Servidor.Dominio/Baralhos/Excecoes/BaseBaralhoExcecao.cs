namespace Piratas.Servidor.Dominio.Baralhos.Excecoes;

using Dominio.Excecoes;

public class BaseBaralhoExcecao : BaseRegraException
{
    public BaseBaralhoExcecao(string id, string mensagem) : base(id, mensagem)
    {
    }
}
