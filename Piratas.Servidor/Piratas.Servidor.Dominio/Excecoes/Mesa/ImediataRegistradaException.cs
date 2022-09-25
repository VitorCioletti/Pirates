namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class ImediataRegistradaException : MesaException
    {
        public ImediataRegistradaException() : base("existe-imediata-registrada", "Já existe uma imediata registrada.")
        {
        }
    }
}
