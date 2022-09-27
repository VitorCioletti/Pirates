namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public class MaoVaziaException : BaseMaoException
    {
        public MaoVaziaException() : base("mao-vazia", "Mão está vazia.")
        {
        }
    }
}
