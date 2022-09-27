namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class SemDueloException : BaseMesaException
    {
        public SemDueloException() : base("mesa-sem-duelo", "Mesa sem duelo.")
        {
        }
    }
}
