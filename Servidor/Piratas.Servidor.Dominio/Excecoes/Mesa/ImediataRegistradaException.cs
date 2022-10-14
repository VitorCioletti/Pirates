namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class ImediataRegistradaException : BaseMesaException
    {
        public ImediataRegistradaException() : base("existe-imediata-registrada", "Já existe uma imediata registrada.")
        {
        }
    }
}
