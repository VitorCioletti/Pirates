namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class EmDueloException : MesaException
    {
        public EmDueloException() : base("mesa-em-duelo", "Mesa já está em duelo.")
        {
        }
    }
}
