namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    using Dominio.Cartas;

    public class CartaNaoExisteNaMaoExcecao : BaseMaoExcecao
    {
        public CartaNaoExisteNaMaoExcecao(Carta carta)
            : base("carta-nao-existe-na-mao", $"Carta \"{carta.Id}\" não existe na mão.")
        {
        }
    }
}
