namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    using Dominio.Cartas;

    public class CartaNaoExisteNaMaoException : BaseMaoException
    {
        public CartaNaoExisteNaMaoException(Carta carta)
            : base("carta-nao-existe-na-mao", $"Carta \"{carta.Id}\" não existe na mão.")
        {
        }
    }
}
