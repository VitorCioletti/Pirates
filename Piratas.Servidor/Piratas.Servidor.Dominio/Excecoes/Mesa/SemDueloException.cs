namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class SemDueloException : MesaException
    {
        public SemDueloException() : base("mesa-sem-duelo", "Mesa sem duelo.")
        {
        }
    }
}
