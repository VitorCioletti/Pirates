namespace Piratas.Servidor.Dominio.Baralhos.Excecoes
{
    using Dominio.Excecoes;

    public class BaseBaralhoExcecao : BaseDominioExcecao
    {
        public BaseBaralhoExcecao(string id, string mensagem) : base(id, mensagem)
        {
        }
    }
}
