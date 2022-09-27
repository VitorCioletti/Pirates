namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public class LimiteCartasMaoAtingidoException : BaseMaoException
    {
        public LimiteCartasMaoAtingidoException() : base("limite-cartas-mao-atingido", "Limite de cartas na mão atingido")
        {
        }
    }
}
