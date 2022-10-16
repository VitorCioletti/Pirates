namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class CartaNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public CartaNaoEUmaOpcaoExcecao(Acao acao, Carta carta)
            : base(acao, "carta-nao-e-uma-opcao", $"Carta \"{carta.Id}\" não é uma opção.")
        {
        }
    }
}
