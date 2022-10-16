namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class ImediataRegistradaExcecao : BaseMesaExcecao
    {
        public ImediataRegistradaExcecao() : base("existe-imediata-registrada", "Já existe uma imediata registrada.")
        {
        }
    }
}
