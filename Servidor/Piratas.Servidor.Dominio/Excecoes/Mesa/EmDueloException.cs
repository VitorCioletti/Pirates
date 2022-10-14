namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class EmDueloException : BaseMesaException
    {
        public EmDueloException() : base("mesa-em-duelo", "Mesa já está em duelo.")
        {
        }
    }
}
