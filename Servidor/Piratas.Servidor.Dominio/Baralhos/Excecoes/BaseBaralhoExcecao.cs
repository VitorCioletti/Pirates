namespace Piratas.Servidor.Dominio.Baralhos.Excecoes
{
    using Dominio.Excecoes;

    public class BaseBaralhoExcecao : BaseRegraExcecao
    {
        public BaseBaralhoExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
