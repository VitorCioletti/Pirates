namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class CartaNaoEUmaOpcaoException : BaseAcoesException
    {
        public CartaNaoEUmaOpcaoException(Acao acao, Carta carta)
            : base(acao, "carta-nao-e-uma-opcao", $"Carta \"{carta.Id}\" não é uma opção.")
        {
        }
    }
}
